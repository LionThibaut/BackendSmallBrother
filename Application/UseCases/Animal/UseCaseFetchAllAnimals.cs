using Application.Services.Client;
using Application.UseCases.Animal.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Animal;

public class UseCaseFetchAllAnimals : IUseCaseQuery<IEnumerable<DtoOutputAnimal>>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IClientService _clientService;

    public UseCaseFetchAllAnimals(IAnimalRepository animalRepository, IClientService clientService)
    {
        _animalRepository = animalRepository;
        _clientService = clientService;
    }
    
    // FetchAll Animals with their Clients
    public IEnumerable<DtoOutputAnimal> Execute()
    {
        var dbAnimal = _animalRepository.FetchAll();
        var listDbAnimals = dbAnimal.ToList();
        var dtoOutputAnimals =  Mapper.GetInstance().Map<List<DtoOutputAnimal>>(dbAnimal);
        for (var i = 0; i < dtoOutputAnimals.Count; i++)
        {
            dtoOutputAnimals[i].ClientAnimal = Mapper.GetInstance().Map<DtoOutputAnimal.DtoClientAnimal>(_clientService.FetchById(listDbAnimals[i].IdClient));
        }
        return dtoOutputAnimals;
    }
}