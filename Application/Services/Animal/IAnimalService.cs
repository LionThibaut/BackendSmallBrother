namespace Application.Services.Animal;

public interface IAnimalService
{
    Domain.Animal FetchById(int id);
}