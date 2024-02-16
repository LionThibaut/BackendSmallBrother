using Application.Services.Client;
using Application.UseCases.Animal.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Animal;

public class UseCaseChangeStatutLostAnimal : IUseCaseWriter<DtoOutputAnimal, DtoInputChangeStatutLostAnimal>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IPostRepository _postRepository;
    private readonly IClientService _clientService;

    public UseCaseChangeStatutLostAnimal(IAnimalRepository animalRepository, IPostRepository postRepository, IClientService clientService)
    {
        _animalRepository = animalRepository;
        _postRepository = postRepository;
        _clientService = clientService;
    }

    // Change statut Animal to lost with creation of a post
    public DtoOutputAnimal Execute(DtoInputChangeStatutLostAnimal input)
    {
        var dbAnimal = _animalRepository.ChangeStatutLost(input.Id, input.TownDisparition, input.DescriptionPost);
        _postRepository.Create(DateTime.Now.ToString("yyyy-MM-dd"), 0, input.TownDisparition, input.DescriptionPost, 
            dbAnimal.UrlImage, dbAnimal.IdAnimal);
        var dtoOutputAnimal = Mapper.GetInstance().Map<DtoOutputAnimal>(dbAnimal);
        dtoOutputAnimal.ClientAnimal = Mapper.GetInstance()
            .Map<DtoOutputAnimal.DtoClientAnimal>(_clientService.FetchById(dbAnimal.IdClient));
        return dtoOutputAnimal;
    }
}