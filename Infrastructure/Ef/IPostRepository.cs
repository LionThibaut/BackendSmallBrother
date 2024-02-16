using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IPostRepository
{
    IEnumerable<DbPost> FetchAll();

    IEnumerable<DbPost> FetchAllFound();

    IEnumerable<DbPost> FetchAllNotFound();

    DbPost FetchById(int id);

    IEnumerable<DbPost> FetchLatestsPosts(int nbLast);

    IEnumerable<DbPost> FetchByIdAnimal(int idAnimal);

    IEnumerable<DbPost> FetchByIdClient(int idClient);

    DbPost Create(string datePost, int nbAlert, string townDisparition, string descriptionPost, string? urlImage, int idAnimal);

    DbPost Delete(int id);
}