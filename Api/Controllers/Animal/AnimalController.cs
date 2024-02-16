using Application.UseCases.Animal;
using Application.UseCases.Animal.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackSmallBrother.Controllers.Animal;

[ApiController]
[Route("api/v1/animal")]
public class AnimalController : ControllerBase
{
    private readonly UseCaseFetchAllAnimals _useCaseFetchAllAnimals;
    private readonly UseCaseFetchAnimalById _useCaseFetchAnimalById;
    private readonly UseCaseFetchAnimalsByIdClient _useCaseFetchAnimalsByIdClient;
    private readonly UseCaseCreateAnimal _useCaseCreateAnimal;
    private readonly UseCaseDeleteAnimal _useCaseDeleteAnimal;
    private readonly UseCaseChangeStatutDefaultAnimal _useCaseChangeStatutDefaultAnimal;
    private readonly UseCaseChangeStatutLostAnimal _useCaseChangeStatutLostAnimal;
    private readonly UseCaseFetchAnimalsByIdClientFound _useCaseFetchAnimalsByIdClientFound;

    public AnimalController(UseCaseFetchAllAnimals useCaseFetchAllAnimals, UseCaseFetchAnimalById useCaseFetchAnimalById, 
        UseCaseFetchAnimalsByIdClient useCaseFetchAnimalsByIdClient, UseCaseCreateAnimal useCaseCreateAnimal, UseCaseDeleteAnimal useCaseDeleteAnimal, 
        UseCaseChangeStatutDefaultAnimal useCaseChangeStatutDefaultAnimal, UseCaseChangeStatutLostAnimal useCaseChangeStatutLostAnimal, UseCaseFetchAnimalsByIdClientFound useCaseFetchAnimalsByIdClientFound)
    {
        _useCaseFetchAllAnimals = useCaseFetchAllAnimals;
        _useCaseFetchAnimalById = useCaseFetchAnimalById;
        _useCaseFetchAnimalsByIdClient = useCaseFetchAnimalsByIdClient;
        _useCaseCreateAnimal = useCaseCreateAnimal;
        _useCaseDeleteAnimal = useCaseDeleteAnimal;
        _useCaseChangeStatutDefaultAnimal = useCaseChangeStatutDefaultAnimal;
        _useCaseChangeStatutLostAnimal = useCaseChangeStatutLostAnimal;
        _useCaseFetchAnimalsByIdClientFound = useCaseFetchAnimalsByIdClientFound;
    }
    
    [HttpGet]
    [Authorize]
    [Route("fetchAll")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputAnimal>> FetchAll()
    {
        return Ok(_useCaseFetchAllAnimals.Execute());
    }

    [HttpGet]
    [Authorize]
    [Route("fetchById/{id:int}")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputAnimal> FetchById(int id)
    {
        try
        {
            return Ok(_useCaseFetchAnimalById.Execute(id));
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
    [Route("fetchByIdClient")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputAnimal>> FetchByIdClient()
    {
        try
        {
           var idClient = User.Claims.First(claim => claim.Type == "IdClient").Value;
           return Ok(_useCaseFetchAnimalsByIdClient.Execute(Convert.ToInt32(idClient)));
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
    public ActionResult<IEnumerable<DtoOutputAnimal>> FetchByIdClient(int idClient)
    {
        try
        {
            return Ok(_useCaseFetchAnimalsByIdClient.Execute(idClient));
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
    [Route("fetchByIdClientFound")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputAnimal>> FetchByIdClientFound()
    {
        try
        {
            var idClient = User.Claims.First(claim => claim.Type == "IdClient").Value;
            return Ok(_useCaseFetchAnimalsByIdClientFound.Execute(Convert.ToInt32(idClient)));
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
    public ActionResult<DtoOutputAnimal> Create(DtoInputCreateAnimal dto)
    {
        var output = _useCaseCreateAnimal.Execute(dto);
        return CreatedAtAction(
            nameof(FetchById),
            new { id = output.IdAnimal },
            output
        );
    }
    
    [HttpDelete]
    [Authorize]
    [Route("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputAnimal> Delete(int id)
    {
        try
        {
            return Ok(_useCaseDeleteAnimal.Execute(id));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }

    [HttpPatch]
    [Authorize]
    [Route("changeStatutDefault/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputAnimal> ChangeStatutDefault(int id)
    {
        try
        {
            return Ok(_useCaseChangeStatutDefaultAnimal.Execute(id));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }

    [HttpPatch]
    [Authorize]
    [Route("changeStatutLost")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputAnimal> ChangeStatutLost(DtoInputChangeStatutLostAnimal dto)
    {
        try
        {
            return Ok(_useCaseChangeStatutLostAnimal.Execute(dto));
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