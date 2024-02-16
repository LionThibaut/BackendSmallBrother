using Application.UseCases;
using Infrastructure.Ef;

namespace Application.Services.Client;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public Domain.Client FetchById(int id)
    {
        var dbClient = _clientRepository.FetchById(id);
        return Mapper.GetInstance().Map<Domain.Client>(dbClient);
    }
}