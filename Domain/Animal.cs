namespace Domain;

public class Animal
{
    public int IdAnimal { get; set; }
    public string NameAnimal { get; set; }
    public string Breed { get; set; }
    public bool? Tag { get; set; }
    public string? Birthdate { get; set; }
    public string DescriptionAnimal { get; set; }
    public string? Height { get; set; }
    public string Gender { get; set; }
    public string TypeAnimal { get; set; }
    public string StatutAnimal { get; set; }
    public string? UrlImage { get; set; }
    
    public Client ClientAnimal { get; set; }
    
    public bool Equals(Animal animal)
    {
        return IdAnimal == animal.IdAnimal;
    }

    public Animal Clone()
    {
        return new Animal
        {
            IdAnimal = IdAnimal,
            NameAnimal = NameAnimal,
            Breed = Breed,
            Tag = Tag,
            Birthdate = Birthdate,
            DescriptionAnimal = DescriptionAnimal,
            Height = Height,
            Gender = Gender,
            TypeAnimal = TypeAnimal,
            StatutAnimal = StatutAnimal,
            UrlImage = UrlImage,
            ClientAnimal = ClientAnimal.Clone()
        };
    }
}