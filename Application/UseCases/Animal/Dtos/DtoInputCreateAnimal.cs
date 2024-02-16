using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Animal.Dtos;

public class DtoInputCreateAnimal
{
    [Required] public string NameAnimal { get; set; }
    [Required] public string Breed { get; set; }
    public bool? Tag { get; set; }
    public string? Birthdate { get; set; }
    [Required] public string DescriptionAnimal { get; set; }
    public string? Height { get; set; }
    [Required] public string Gender { get; set; }
    [Required] public string TypeAnimal { get; set; }
    [Required] public string StatutAnimal { get; set; }
    public string? UrlImage { get; set; }
    [Required] public int IdClient { get; set; }
}