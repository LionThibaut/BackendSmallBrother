namespace Application.Services.Client;

public interface IClientService
{
    Domain.Client FetchById(int id);
}