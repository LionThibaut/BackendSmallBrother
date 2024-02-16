namespace Infrastructure.Ef.DbEntities;

public class DbClient
{
    public int IdClient { get; set; }
    
    public string FirstName{ get; set;}
    
    public string LastName{ get; set; }
    
    public string Gender{ get; set; }
    
    public string Mail{ get; set; }
    
    public string HashedPassword{ get; set; }
    
    public string PhoneNumber{ get; set; }
    
    public string Town{ get; set; }
    
    public string RoleClient{ get; set; }
    
    public string? UrlImage { get; set; }
}