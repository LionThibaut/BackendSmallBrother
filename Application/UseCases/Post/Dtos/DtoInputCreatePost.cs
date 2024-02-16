using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Post.Dtos;

public class DtoInputCreatePost
{
    [Required] public string DatePost { get; set; }
    
    [Required] public int NbAlert { get; set; }
    
    [Required] public string TownDisparition { get; set; }
    
    [Required] public string DescriptionPost { get; set; }
    
    public string? UrlImage { get; set; }
    
    [Required] public int IdAnimal { get; set; }
}