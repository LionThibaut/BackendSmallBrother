using Application.UseCases.Client.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Client;

public class UseCaseFetchAllClients : IUseCaseQuery<IEnumerable<DtoOutputClient>>
{
    private readonly IClientRepository _clientRepository;

    public UseCaseFetchAllClients(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    // FetchAll Client
    public IEnumerable<DtoOutputClient> Execute()
    {
        var dbClient = _clientRepository.FetchAll();
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputClient>>(dbClient);
    }
}