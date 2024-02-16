using Application.Services.Animal;
using Application.Services.Client;
using Application.UseCases.Post.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Post;

public class UseCaseFetchPostsByIdClient : IUseCaseParameterizedQuery<IEnumerable<DtoOutputPost>, int>
{
    private readonly IPostRepository _postRepository;
    private readonly IAnimalRepository _animalRepository;
    private readonly IAnimalService _animalService;
    private readonly IClientService _clientService;

    public UseCaseFetchPostsByIdClient(IPostRepository postRepository, IAnimalRepository animalRepository, IAnimalService animalService, IClientService clientService)
    {
        _postRepository = postRepository;
        _animalRepository = animalRepository;
        _animalService = animalService;
        _clientService = clientService;
    }

    // FetchAll Posts of a Client with their Animals and Client
    public IEnumerable<DtoOutputPost> Execute(int idClient)
    {
        var dbPost = _postRepository.FetchByIdClient(idClient);
        var listDbPosts = dbPost.ToList();
        var dtoOutputPosts = Mapper.GetInstance().Map<List<DtoOutputPost>>(dbPost);
        for (var i = 0; i < dtoOutputPosts.Count; i++)
        {
            var dbAnimal = _animalRepository.FetchById(listDbPosts[i].IdAnimal);
            dtoOutputPosts[i].AnimalPost = Mapper.GetInstance()
                .Map<DtoOutputPost.DtoAnimalPost>(_animalService.FetchById(listDbPosts[i].IdAnimal));
            dtoOutputPosts[i].AnimalPost.ClientAnimalPost = Mapper.GetInstance()
                .Map<DtoOutputPost.DtoClientAnimalPost>(
                    _clientService.FetchById(idClient));
        }
        dtoOutputPosts.Reverse();
        return dtoOutputPosts;
    }
}