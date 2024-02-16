using Application.Services.Animal;
using Application.Services.Client;
using Application.UseCases.Post.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Post;

public class UseCaseCreatePost : IUseCaseWriter<DtoOutputPost, DtoInputCreatePost>
{
    private readonly IPostRepository _postRepository;
    private readonly IAnimalRepository _animalRepository;
    private readonly IAnimalService _animalService;
    private readonly IClientService _clientService;

    public UseCaseCreatePost(IPostRepository postRepository, IAnimalRepository animalRepository, IAnimalService animalService, IClientService clientService)
    {
        _postRepository = postRepository;
        _animalRepository = animalRepository;
        _animalService = animalService;
        _clientService = clientService;
    }

    // Creation of Post
    public DtoOutputPost Execute(DtoInputCreatePost input)
    {
        var dbPost = _postRepository.Create(input.DatePost, input.NbAlert,
            input.TownDisparition, input.DescriptionPost, input.UrlImage,
            input.IdAnimal);
        var dbAnimal = _animalRepository.FetchById(dbPost.IdAnimal);
        var dtoOutputPost = Mapper.GetInstance().Map<DtoOutputPost>(dbPost);
        dtoOutputPost.AnimalPost = Mapper.GetInstance()
            .Map<DtoOutputPost.DtoAnimalPost>(_animalService.FetchById(dbPost.IdAnimal));
        dtoOutputPost.AnimalPost.ClientAnimalPost = Mapper.GetInstance()
            .Map<DtoOutputPost.DtoClientAnimalPost>(_clientService.FetchById(dbAnimal.IdClient));
        return dtoOutputPost;
    }
}