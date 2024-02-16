using Application.Services.Client;
using Application.UseCases.Animal.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Animal;

public class UseCaseFetchAnimalById : IUseCaseParameterizedQuery<DtoOutputAnimal, int>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IClientService _clientService;

    public UseCaseFetchAnimalById(IAnimalRepository animalRepository, IClientService clientService)
    {
        _animalRepository = animalRepository;
        _clientService = clientService;
    }
    
    // Fetch Animal with id with his Client
    public DtoOutputAnimal Execute(int id)
    {
        var dbAnimal = _animalRepository.FetchById(id);
        var dtoOutputAnimal =  Mapper.GetInstance().Map<DtoOutputAnimal>(dbAnimal);
        dtoOutputAnimal.ClientAnimal = Mapper.GetInstance().Map<DtoOutputAnimal.DtoClientAnimal>(_clientService.FetchById(dbAnimal.IdClient));
        return dtoOutputAnimal;
    }
}