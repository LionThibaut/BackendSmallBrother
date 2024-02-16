using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Client.Dtos;

public class DtoInputCreateClient
{
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public string Gender { get; set; }
    [Required] public string Mail { get; set; }
    [Required] public string HashedPassword { get; set; }
    [Required] public string PhoneNumber { get; set; }
    [Required] public string Town { get; set; }
    [Required] public string RoleClient { get; set; }
    public string? UrlImage { get; set; }
    
}