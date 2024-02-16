using Application.Services.Client;
using Application.UseCases.Animal.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Animal;

public class UseCaseFetchAnimalsByIdClientFound : IUseCaseParameterizedQuery<IEnumerable<DtoOutputAnimal>, int>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IClientService _clientService;

    public UseCaseFetchAnimalsByIdClientFound(IAnimalRepository animalRepository, IClientService clientService)
    {
        _animalRepository = animalRepository;
        _clientService = clientService;
    }

    // Fetch Animal found with id of their Clients with their Client
    public IEnumerable<DtoOutputAnimal> Execute(int idClient)
    {
        var dbAnimal = _animalRepository.FetchByIdClientFound(idClient);
        var clientAnimal = _clientService.FetchById(idClient);
        var dtoOutputAnimals =  Mapper.GetInstance().Map<IEnumerable<DtoOutputAnimal>>(dbAnimal);
        var listDtoOutputAnimals = dtoOutputAnimals.ToList();
        foreach (var dtoOutputAnimal in listDtoOutputAnimals)
        {
            dtoOutputAnimal.ClientAnimal = Mapper.GetInstance().Map<DtoOutputAnimal.DtoClientAnimal>(clientAnimal);
        }
        return listDtoOutputAnimals;
    }
}