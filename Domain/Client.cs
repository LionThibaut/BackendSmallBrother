namespace Domain;

public class Client
{
    public int IdClient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public string Mail { get; set; }
    public string HashedPassword { get; set; }
    public string PhoneNumber { get; set; }
    public string Town { get; set; }
    public string RoleClient { get; set; }
    public string? UrlImage { get; set; }

    public bool Equals(Client client)
    {
        return IdClient == client.IdClient;
    }

    public Client Clone()
    {
        return new Client
        {
            IdClient = IdClient,
            FirstName = FirstName,
            LastName = LastName,
            Gender = Gender,
            Mail = Mail,
            HashedPassword = HashedPassword,
            PhoneNumber = PhoneNumber,
            Town = Town,
            RoleClient = RoleClient,
            UrlImage = UrlImage
        };
    }
}
