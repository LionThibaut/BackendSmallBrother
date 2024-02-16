using Application.UseCases.Client.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Client;

public class UseCaseFetchClientById : IUseCaseParameterizedQuery<DtoOutputClient, int>
{
    private readonly IClientRepository _clientRepository;

    public UseCaseFetchClientById(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    // Fetch Client with his id
    public DtoOutputClient Execute(int id)
    {
        var dbClient = _clientRepository.FetchById(id);
        return Mapper.GetInstance().Map<DtoOutputClient>(dbClient);
    }
}