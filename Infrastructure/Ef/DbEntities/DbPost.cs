namespace Infrastructure.Ef.DbEntities;

public class DbPost
{
    public int IdPost { get; set; }
    
    public string DatePost { get; set; }
    
    public int NbAlert { get; set; }
    
    public string TownDisparition { get; set; }
    
    public string DescriptionPost { get; set; }
    
    public string? UrlImage { get; set; }
    
    public int IdAnimal{ get; set; }
}