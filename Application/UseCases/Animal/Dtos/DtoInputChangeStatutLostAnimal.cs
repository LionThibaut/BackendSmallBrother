using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Animal.Dtos;

public class DtoInputChangeStatutLostAnimal
{
    [Required] public int Id { get; set; }
    
    [Required] public string TownDisparition { get; set; }
    
    [Required] public string DescriptionPost { get; set; }
}