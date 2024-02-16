using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IClientRepository
{
    IEnumerable<DbClient> FetchAll();
    
    DbClient FetchById(int id);

    DbClient FetchByLogin(string mail, string password);

    DbClient Create(string firstName,string lastName,
        string gender, string mail, string hashedPassword,
        string phoneNumber, string town, string roleClient, string? urlImage);

    DbClient Delete(int id);
}