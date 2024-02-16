using Application.Services.Animal;
using Application.Services.Client;
using Application.UseCases.Post.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Post;

public class UseCaseFetchPostsByIdAnimal : IUseCaseParameterizedQuery<IEnumerable<DtoOutputPost>, int>
{
    private readonly IPostRepository _postRepository;
    private readonly IAnimalRepository _animalRepository;
    private readonly IAnimalService _animalService;
    private readonly IClientService _clientService;

    public UseCaseFetchPostsByIdAnimal(IPostRepository postRepository, IAnimalRepository animalRepository,
        IAnimalService animalService, IClientService clientService)
    {
        _postRepository = postRepository;
        _animalRepository = animalRepository;
        _animalService = animalService;
        _clientService = clientService;
    }

    // FetchAll Posts of a Animal with his Animal and Client
    public IEnumerable<DtoOutputPost> Execute(int idAnimal)
    {
        var dbPost = _postRepository.FetchByIdAnimal(idAnimal);
        var dbAnimal = _animalRepository.FetchById(idAnimal);
        var animalPost = _animalService.FetchById(idAnimal);
        var dtoOutputPosts = Mapper.GetInstance().Map<IEnumerable<DtoOutputPost>>(dbPost);
        var listDtoOutputPosts = dtoOutputPosts.ToList();
        foreach (var dtoOutputPost in listDtoOutputPosts)
        {
            dtoOutputPost.AnimalPost = Mapper.GetInstance().Map<DtoOutputPost.DtoAnimalPost>(animalPost);
            dtoOutputPost.AnimalPost.ClientAnimalPost = Mapper.GetInstance()
                .Map<DtoOutputPost.DtoClientAnimalPost>(_clientService.FetchById(dbAnimal.IdClient));
        }
        listDtoOutputPosts.Reverse();
        return listDtoOutputPosts;
    }
}