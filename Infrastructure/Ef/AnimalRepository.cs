using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

namespace Infrastructure.Ef;

public class AnimalRepository : IAnimalRepository
{
    private readonly BackSmallBrotherContextProvider _contextProvider;

    public AnimalRepository(BackSmallBrotherContextProvider contextProvider)
    {
        _contextProvider = contextProvider;
    }

    // FetchAll of Animals
    public IEnumerable<DbAnimal> FetchAll()
    {
        using var context = _contextProvider.NewContext();
        return context.Animals.ToList();
    }

    // FetchById of Animal
    public DbAnimal FetchById(int id)
    {
        using var context = _contextProvider.NewContext();
        var animal = context.Animals.FirstOrDefault(a => a.IdAnimal == id);

        if (animal == null)
            throw new KeyNotFoundException($"Animal with id {id} has not been found");

        return animal;
    }

    // Fetch Animal by id client
    public IEnumerable<DbAnimal> FetchByIdClient(int idClient)
    {
        using var context = _contextProvider.NewContext();
        var animals = context.Animals.Where(a => a.IdClient == idClient && a.StatutAnimal != "R").ToList();

        if (animals.Count == 0)
            throw new KeyNotFoundException($"Client with id {idClient} has not been found or has no animals");

        return animals;
    }

    // Create Animal of a Client
    public DbAnimal Create(string nameAnimal, string breed, bool? tag, string? birthDate, string descriptionAnimal, string? height,
        string gender, string typeAnimal, string statutAnimal, string? urlImage, int idClient)
    {
        using var context = _contextProvider.NewContext();
        var animal = new DbAnimal
        {
            NameAnimal = nameAnimal, Breed = breed, Tag = tag,
            Birthdate = birthDate, DescriptionAnimal = descriptionAnimal, Height = height,
            Gender = gender, TypeAnimal = typeAnimal, StatutAnimal = statutAnimal,
            UrlImage = urlImage, IdClient = idClient
        };
        context.Animals.Add(animal);
        context.SaveChanges();
        return animal;
    }

    // Delete Animal
    public DbAnimal Delete(int id)
    {
        using var context = _contextProvider.NewContext();
        var animal = context.Animals.FirstOrDefault(a => a.IdAnimal == id);

        if (animal == null)
            throw new KeyNotFoundException($"Animal with id {id} has not been found");

        context.Animals.Remove(animal);
        context.SaveChanges();
        return animal;
    }

    // Change statut Animal to default
    public DbAnimal ChangeStatutDefault(int id)
    {
        using var context = _contextProvider.NewContext();
        var animal = context.Animals.FirstOrDefault(a => a.IdAnimal == id);
        
        if (animal == null)
            throw new KeyNotFoundException($"Animal with id {id} has not been found");
        
        animal.StatutAnimal = "N";
        
        context.Animals.Update(animal);
        context.SaveChanges();
        return animal;
    }
    
    // Change statut Animal to lost
    public DbAnimal ChangeStatutLost(int id, string townDisparition, string descriptionPost)
    {
        using var context = _contextProvider.NewContext();
        var animal = context.Animals.FirstOrDefault(a => a.IdAnimal == id);
        
        if (animal == null)
            throw new KeyNotFoundException($"Animal with id {id} has not been found");

        animal.StatutAnimal = "P";
        
        context.Animals.Update(animal);
        context.SaveChanges();
        return animal;
    }

    // Fetch Animals found of a Client
    public IEnumerable<DbAnimal> FetchByIdClientFound(int idClient)
    {
        using var context = _contextProvider.NewContext();
        var animals = context.Animals.Where(a => a.IdClient == idClient && a.StatutAnimal == "R").ToList();

        if (animals.Count == 0)
            throw new KeyNotFoundException($"Client with id {idClient} has not been found or has no animals found");

        return animals;
    }
}