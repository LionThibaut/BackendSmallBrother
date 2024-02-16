using Application.UseCases.Client.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Client;

public class UseCaseCreateClient : IUseCaseWriter<DtoOutputClient, DtoInputCreateClient>
{
    private readonly IClientRepository _clientRepository;

    public UseCaseCreateClient(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    
    // Create Client
    public DtoOutputClient Execute(DtoInputCreateClient input)
    {
        var dbClient = _clientRepository.Create(input.FirstName, input.LastName,
            input.Gender, input.Mail, input.HashedPassword, input.PhoneNumber,
            input.Town, input.RoleClient, input.UrlImage);
        return Mapper.GetInstance().Map<DtoOutputClient>(dbClient);
    }
}