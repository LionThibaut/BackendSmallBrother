using Application.UseCases.Post;
using Application.UseCases.Post.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackSmallBrother.Controllers.Post;

[ApiController]
[Route("api/v1/post")]
public class PostController : ControllerBase
{
    private readonly UseCaseFetchAllPosts _useCaseFetchAllPosts;
    private readonly UseCaseFetchPostById _useCaseFetchPostById;
    private readonly UseCaseFetchLatestsPosts _useCaseFetchLatestsPosts;
    private readonly UseCaseFetchPostsByIdAnimal _useCaseFetchPostsByIdAnimal;
    private readonly UseCaseFetchPostsByIdClient _useCaseFetchPostsByIdClient;
    private readonly UseCaseCreatePost _useCaseCreatePost;
    private readonly UseCaseDeletePost _useCaseDeletePost;
    private readonly UseCaseFetchAllPostsFound _useCaseFetchAllPostsFound;
    private readonly UseCaseFetchAllPostsNotFound _useCaseFetchAllPostsNotFound;

    public PostController(UseCaseFetchAllPosts useCaseFetchAllPosts, UseCaseFetchPostById useCaseFetchPostById, 
        UseCaseFetchLatestsPosts useCaseFetchLatestsPosts, UseCaseFetchPostsByIdAnimal useCaseFetchPostsByIdAnimal,
        UseCaseFetchPostsByIdClient useCaseFetchPostsByIdClient, UseCaseCreatePost useCaseCreatePost, 
        UseCaseDeletePost useCaseDeletePost, UseCaseFetchAllPostsFound useCaseFetchAllPostsFound, 
        UseCaseFetchAllPostsNotFound useCaseFetchAllPostsNotFound)
    {
        _useCaseFetchAllPosts = useCaseFetchAllPosts;
        _useCaseFetchPostById = useCaseFetchPostById;
        _useCaseFetchLatestsPosts = useCaseFetchLatestsPosts;
        _useCaseFetchPostsByIdAnimal = useCaseFetchPostsByIdAnimal;
        _useCaseFetchPostsByIdClient = useCaseFetchPostsByIdClient;
        _useCaseCreatePost = useCaseCreatePost;
        _useCaseDeletePost = useCaseDeletePost;
        _useCaseFetchAllPostsFound = useCaseFetchAllPostsFound;
        _useCaseFetchAllPostsNotFound = useCaseFetchAllPostsNotFound;
    }
    
    [HttpGet]
    [Authorize]
    [Route("fetchAll")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputPost>> FetchAll()
    {
        return Ok(_useCaseFetchAllPosts.Execute());
    }

    [HttpGet]
    [Authorize]
    [Route("fetchAllFound")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputPost>> FetchAllFound()
    {
        return Ok(_useCaseFetchAllPostsFound.Execute());
    }
    
    [HttpGet]
    [Authorize]
    [Route("fetchAllNotFound")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputPost>> FetchAllNotFound()
    {
        return Ok(_useCaseFetchAllPostsNotFound.Execute());
    }

    [HttpGet]
    [Authorize]
    [Route("fetchById/{id:int}")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputPost> FetchById(int id)
    {
        try
        {
            return Ok(_useCaseFetchPostById.Execute(id));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }
    
    [HttpGet]
    [Route("fetchLatestsPosts/{nbLast:int}")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputPost>> FetchLatestsPosts(int nbLast)
    {
        try
        {
            return Ok(_useCaseFetchLatestsPosts.Execute(nbLast));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("fetchByIdAnimal/{idAnimal:int}")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputPost>> FetchByIdAnimal(int idAnimal)
    {
        try
        {
            return Ok(_useCaseFetchPostsByIdAnimal.Execute(idAnimal));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("fetchByIdClient/{idClient:int}")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputPost>> FetchByIdClient(int idClient)
    {
        try
        {
            return Ok(_useCaseFetchPostsByIdClient.Execute(idClient));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<DtoOutputPost> Create(DtoInputCreatePost dto)
    {
        var output = _useCaseCreatePost.Execute(dto);
        return CreatedAtAction(
            nameof(FetchById),
            new { id = output.IdPost },
            output
        );
    }
    
    [HttpDelete]
    [Authorize]
    [Route("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputPost> Delete(int id)
    {
        try
        {
            return Ok(_useCaseDeletePost.Execute(id));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }
}