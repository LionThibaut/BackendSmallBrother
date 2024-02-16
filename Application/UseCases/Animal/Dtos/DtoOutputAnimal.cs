namespace Application.UseCases.Animal.Dtos;

public class DtoOutputAnimal
{
    public int IdAnimal { get; set; }
    
    public string NameAnimal { get; set; }
    
    public string Breed{ get; set;}
    
    public bool? Tag{ get; set;}
    
    public string? Birthdate{ get; set; }
    
    public string DescriptionAnimal { get; set; }
    
    public string? Height{ get; set; }
    
    public string Gender{ get; set; }
    
    public string TypeAnimal { get; set; }

    public string StatutAnimal { get; set; }
    
    public string? UrlImage { get; set; }
    public DtoClientAnimal ClientAnimal { get; set; }

    public class DtoClientAnimal
    {
        public int IdClient { get; set; }
    
        public string FirstName{ get; set;}
    
        public string LastName{ get; set; }
    
        public string Gender{ get; set; }
    
        public string Mail{ get; set; }

        public string PhoneNumber{ get; set; }
    
        public string Town{ get; set; }
    
        public string RoleClient{ get; set; }
        
        public string? UrlImage { get; set; }
    }
}