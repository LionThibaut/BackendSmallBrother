using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Client.Dtos;

public class DtoInputLoginClient
{
    [Required] public string Mail { get; set; }
    [Required] public string HashedPassword { get; set; }
}