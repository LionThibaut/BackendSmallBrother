using Application.UseCases;
using Infrastructure.Ef;

namespace Application.Services.Animal;

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    public Domain.Animal FetchById(int id)
    {
        var dbAnimal = _animalRepository.FetchById(id);
        return Mapper.GetInstance().Map<Domain.Animal>(dbAnimal);
    }
}