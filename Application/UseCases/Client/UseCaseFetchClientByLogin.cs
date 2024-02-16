using Application.UseCases.Client.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Client;

public class UseCaseFetchClientByLogin : IUseCaseWriter<DtoOutputClient, DtoInputLoginClient>
{
    private readonly IClientRepository _clientRepository;

    public UseCaseFetchClientByLogin(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    // Fetch Client with its identifiers
    public DtoOutputClient Execute(DtoInputLoginClient input)
    {
        var dbClient = _clientRepository.FetchByLogin(input.Mail, input.HashedPassword);
        return Mapper.GetInstance().Map<DtoOutputClient>(dbClient);
    }
}