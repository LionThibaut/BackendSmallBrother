using Application.UseCases.Animal;
using Application.UseCases.Client.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Client;

public class UseCaseDeleteClient : IUseCaseParameterizedQuery<DtoOutputClient, int>
{
    private readonly IClientRepository _clientRepository;
    private readonly IAnimalRepository _animalRepository;
    private readonly UseCaseDeleteAnimal _useCaseDeleteAnimal;

    public UseCaseDeleteClient(IClientRepository clientRepository, IAnimalRepository animalRepository, UseCaseDeleteAnimal useCaseDeleteAnimal)
    {
        _clientRepository = clientRepository;
        _animalRepository = animalRepository;
        _useCaseDeleteAnimal = useCaseDeleteAnimal;
    }

    // Delete Client
    public DtoOutputClient Execute(int id)
    {
        var listDbAnimals = _animalRepository.FetchAll().Where(a => a.IdClient == id).ToList();
        foreach (var dbAnimal in listDbAnimals)
        {
            _useCaseDeleteAnimal.Execute(dbAnimal.IdAnimal);
        }
        var dbClient = _clientRepository.Delete(id);
        return Mapper.GetInstance().Map<DtoOutputClient>(dbClient);
    }
}