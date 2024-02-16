using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

namespace Infrastructure.Ef;

public class ClientRepository : IClientRepository
{   
    private readonly BackSmallBrotherContextProvider _contextProvider;

    public ClientRepository(BackSmallBrotherContextProvider contextProvider)
    {
        _contextProvider = contextProvider;
    }

    // FetchAll of Clients
    public IEnumerable<DbClient> FetchAll()
    {
        using var context = _contextProvider.NewContext();
        return context.Clients.ToList();
    }

    // FetchById of Clients
    public DbClient FetchById(int id)
    {
        using var context = _contextProvider.NewContext();
        var client = context.Clients.FirstOrDefault(c => c.IdClient == id);

        if (client == null)
            throw new KeyNotFoundException($"Client with id {id} has not been found");

        return client;
    }

    // Connection of the customer with recovery of his information
    public DbClient FetchByLogin(string mail, string password)
    {
        using var context = _contextProvider.NewContext();
        var client = context.Clients.FirstOrDefault(c => c.Mail == mail);

        if (client == null || !VerifyHash(password, client.HashedPassword))
            throw new KeyNotFoundException($"Client with this mail and password has not been found");

        return client;
    }
    
    // Create of Client
    public DbClient Create(string firstName, string lastName, string gender, string mail, string hashedPassword,
        string phoneNumber, string town, string roleClient, string? urlImage)
    {
        using var context = _contextProvider.NewContext();
        var client = new DbClient
        {
            FirstName = firstName, LastName = lastName, Gender = gender,
            Mail = mail, HashedPassword = HashPassword(hashedPassword), PhoneNumber = phoneNumber,
            Town = town, RoleClient = roleClient, UrlImage = urlImage
        };
        context.Clients.Add(client);
        context.SaveChanges();
        return client;
    }

    // Delete of Client with id
    public DbClient Delete(int id)
    {
        using var context = _contextProvider.NewContext();
        var client = context.Clients.FirstOrDefault(c => c.IdClient == id);

        if (client == null)
            throw new KeyNotFoundException($"Client with id {id} has not been found");
        
        context.Clients.Remove(client);
        context.SaveChanges();
        return client;
    }
    
    // Hash password
    public String HashPassword(String password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    // Verify password
    public bool VerifyHash(String password, String passWordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passWordHash);
    }
}