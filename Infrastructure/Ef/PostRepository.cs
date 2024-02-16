using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

namespace Infrastructure.Ef;

public class PostRepository : IPostRepository
{
    private readonly BackSmallBrotherContextProvider _contextProvider;
    private readonly IAnimalRepository _animalRepository;

    public PostRepository(BackSmallBrotherContextProvider contextProvider, IAnimalRepository animalRepository)
    {
        _contextProvider = contextProvider;
        _animalRepository = animalRepository;
    }

    // FetchAll of Posts
    public IEnumerable<DbPost> FetchAll()
    {
        using var context = _contextProvider.NewContext();
        return context.Posts.ToList();
    }

    // Fetch Posts of Animals found
    public IEnumerable<DbPost> FetchAllFound()
    {
        using var context = _contextProvider.NewContext();
        var posts = context.Posts.ToList();
        var postsAnimalsFound = new List<DbPost>();
        foreach (var post in posts)
        {
            var dbAnimal = _animalRepository.FetchById(post.IdAnimal);
            if (dbAnimal.StatutAnimal == "R")
            {
                postsAnimalsFound.Add(post);
            }
        }
        return postsAnimalsFound;
    }

    // Fetch Posts of Animals not found
    public IEnumerable<DbPost> FetchAllNotFound()
    {
        using var context = _contextProvider.NewContext();
        var posts = context.Posts.ToList();
        var postsAnimalsFound = new List<DbPost>();
        foreach (var post in posts)
        {
            var dbAnimal = _animalRepository.FetchById(post.IdAnimal);
            if (dbAnimal.StatutAnimal != "R")
            {
                postsAnimalsFound.Add(post);
            }
        }
        return postsAnimalsFound;
    }

    // Fetch post by id
    public DbPost FetchById(int id)
    {
        using var context = _contextProvider.NewContext();
        var post = context.Posts.FirstOrDefault(p => p.IdPost == id);

        if (post == null)
            throw new KeyNotFoundException($"Post with id {id} has not been found");

        return post;
    }

    // Fetch latests posts to the main menu
    public IEnumerable<DbPost> FetchLatestsPosts(int nbLast)
    {
        using var context = _contextProvider.NewContext();
        var listPosts = context.Posts.ToList();

        if (listPosts.Count < nbLast)
            throw new KeyNotFoundException($"There are not {nbLast} existing posts");
        
        return listPosts.GetRange(listPosts.Count - nbLast, nbLast);
    }

    // Fetch Posts by id Animal of Post
    public IEnumerable<DbPost> FetchByIdAnimal(int idAnimal)
    {
        using var context = _contextProvider.NewContext();
        var posts = context.Posts.Where(p => p.IdAnimal == idAnimal).ToList();

        if (posts.Count == 0)
            throw new KeyNotFoundException($"Animal with id {idAnimal} has not been found or has no posts");

        return posts;
    }

    // Fetch Posts by id of author 
    public IEnumerable<DbPost> FetchByIdClient(int idClient)
    {
        using var context = _contextProvider.NewContext();
        var posts = context.Posts.ToList();
        var postsIdClient = new List<DbPost>();
        foreach (var post in posts)
        {
            var dbAnimal = _animalRepository.FetchById(post.IdAnimal);
            if (dbAnimal.IdClient == idClient)
            {
                postsIdClient.Add(post);
            }
        }
        
        if (postsIdClient.Count == 0)
            throw new KeyNotFoundException($"Client with id {idClient} has not been found or has no posts");
        
        return postsIdClient;
    }


    // Create Post
    public DbPost Create(string datePost, int nbAlert, string townDisparition, string descriptionPost, string? urlImage,
        int idAnimal)
    {
        using var context = _contextProvider.NewContext();
        var post = new DbPost
        {
            DatePost = datePost, NbAlert = nbAlert, TownDisparition = townDisparition,
            DescriptionPost = descriptionPost, UrlImage = urlImage, IdAnimal = idAnimal
        };
        context.Posts.Add(post);
        context.SaveChanges();
        return post;
    }

    // Delete Post
    public DbPost Delete(int id)
    {
        using var context = _contextProvider.NewContext();
        var post = context.Posts.FirstOrDefault(p => p.IdPost == id);

        if (post == null)
            throw new KeyNotFoundException($"Post with id {id} has not been found");

        context.Posts.Remove(post);
        context.SaveChanges();
        return post;
    }
}