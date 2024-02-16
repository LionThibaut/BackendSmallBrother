using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IAnimalRepository
{
    IEnumerable<DbAnimal> FetchAll();
    
    DbAnimal FetchById(int id);

    IEnumerable<DbAnimal> FetchByIdClient(int idClient);

    DbAnimal Create(string nameAnimal, string breed, bool? tag,
        string? birthDate, string descriptionAnimal, string? height, string gender,
        string typeAnimal, string statutAnimal, string? urlImage, int idClient);

    DbAnimal Delete(int id);

    DbAnimal ChangeStatutDefault(int id);
    
    DbAnimal ChangeStatutLost(int id, string townDisparition, string descriptionPost);

    IEnumerable<DbAnimal> FetchByIdClientFound(int idClient);
}