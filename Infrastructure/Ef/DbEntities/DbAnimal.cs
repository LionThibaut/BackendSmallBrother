namespace Infrastructure.Ef.DbEntities;

public class DbAnimal
{
    public int IdAnimal { get; set; }
    
    public string NameAnimal { get; set; }
    
    public string Breed{ get; set; }
    
    public bool? Tag{ get; set; }
    
    public string? Birthdate{ get; set; }
    
    public string DescriptionAnimal { get; set; }
    
    public string? Height{ get; set; }
    
    public string Gender{ get; set; }
    
    public string TypeAnimal { get; set; }
    
    public string StatutAnimal { get; set; }
    
    public string? UrlImage { get; set; }
    
    public int IdClient { get; set; }
}