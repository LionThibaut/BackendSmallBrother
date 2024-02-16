using Application.Services.Client;
using Application.UseCases.Animal.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Animal;

public class UseCaseCreateAnimal : IUseCaseWriter<DtoOutputAnimal, DtoInputCreateAnimal>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IClientService _clientService;

    public UseCaseCreateAnimal(IAnimalRepository animalRepository, IClientService clientService)
    {
        _animalRepository = animalRepository;
        _clientService = clientService;
    }

    // Creation of Animal
    public DtoOutputAnimal Execute(DtoInputCreateAnimal input)
    {
        var dbAnimal = _animalRepository.Create(input.NameAnimal, input.Breed,
            input.Tag, input.Birthdate, input.DescriptionAnimal, input.Height, 
            input.Gender, input.TypeAnimal, input.StatutAnimal, input.UrlImage, 
            input.IdClient);
        var dtoOutputAnimal = Mapper.GetInstance().Map<DtoOutputAnimal>(dbAnimal);
        dtoOutputAnimal.ClientAnimal = Mapper.GetInstance().Map<DtoOutputAnimal.DtoClientAnimal>(_clientService.FetchById(dbAnimal.IdClient));
        return dtoOutputAnimal;
    }
}