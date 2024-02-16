using System.Collections;
using Application.Services.Client;
using Application.UseCases.Animal.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;
using Microsoft.IdentityModel.Tokens;

namespace Application.UseCases.Animal;

public class UseCaseDeleteAnimal : IUseCaseParameterizedQuery<DtoOutputAnimal, int>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IPostRepository _postRepository;
    private readonly IClientService _clientService;

    public UseCaseDeleteAnimal(IAnimalRepository animalRepository, IPostRepository postRepository, IClientService clientService)
    {
        _animalRepository = animalRepository;
        _postRepository = postRepository;
        _clientService = clientService;
    }

    // Delete of Animal
    public DtoOutputAnimal Execute(int id)
    {
        var listDbPosts = _postRepository.FetchAll().Where(p => p.IdAnimal == id).ToList();
        foreach (var dbPost in listDbPosts)
        {
            _postRepository.Delete(dbPost.IdPost);
        }
        var dbAnimal = _animalRepository.Delete(id);
        var dtoOutputAnimal = Mapper.GetInstance().Map<DtoOutputAnimal>(dbAnimal);
        dtoOutputAnimal.ClientAnimal = Mapper.GetInstance().Map<DtoOutputAnimal.DtoClientAnimal>(_clientService.FetchById(dbAnimal.IdClient));
        return dtoOutputAnimal;
    }
}