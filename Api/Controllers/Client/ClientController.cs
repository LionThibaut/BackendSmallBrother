using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.UseCases.Client;
using Application.UseCases.Client.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BackSmallBrother.Controllers.Client;

[ApiController]
[Route("api/v1/client")]
public class ClientController : ControllerBase
{
    private readonly UseCaseFetchAllClients _useCaseFetchAllClients;
    private readonly UseCaseFetchClientById _useCaseFetchClientById;
    private readonly UseCaseFetchClientByLogin _useCaseFetchClientByLogin;
    private readonly UseCaseCreateClient _useCaseCreateClient;
    private readonly UseCaseDeleteClient _useCaseDeleteClient;
    private readonly IConfiguration _configuration;


    public ClientController(UseCaseFetchAllClients useCaseFetchAllClients,
        UseCaseFetchClientById useCaseFetchClientById,
        UseCaseFetchClientByLogin useCaseFetchClientByLogin, UseCaseCreateClient useCaseCreateClient,
        UseCaseDeleteClient useCaseDeleteClient, IConfiguration configuration)
    {
        _useCaseFetchAllClients = useCaseFetchAllClients;
        _useCaseFetchClientById = useCaseFetchClientById;
        _useCaseFetchClientByLogin = useCaseFetchClientByLogin;
        _useCaseCreateClient = useCaseCreateClient;
        _useCaseDeleteClient = useCaseDeleteClient;
        _configuration = configuration;
    }

    [HttpGet]
    [Authorize]
    [Route("fetchAll")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputClient>> FetchAll()
    {
        return Ok(_useCaseFetchAllClients.Execute());
    }

    [HttpGet]
    [Authorize]
    [Route("fetchById")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputClient> FetchById()
    {
        try
        {
            var idClient = User.Claims.First(claim => claim.Type == "IdClient").Value; 
            //var idClient = 2;
            
            
            return Ok(_useCaseFetchClientById.Execute(Convert.ToInt32(idClient)));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }

    // Connection of a Client and creation of authentications cookies
    [HttpPost]
    [Route("fetchByLogin")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputClient> FetchByLogin(DtoInputLoginClient dto)
    {
        try
        {
            var user = _useCaseFetchClientByLogin.Execute(dto);
            string token = CreateToken(user);

            Response.Cookies.Append("CookieSuper", token, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
            });
            
            Response.Cookies.Append("CookieRole", user.RoleClient, new CookieOptions()
            {
                HttpOnly = false,
                Secure = true,
            });
            
            Response.Cookies.Append("CookieId", user.IdClient.ToString(), new CookieOptions()
            {
                HttpOnly = false,
                Secure = true,
            });
            
            return Ok(user);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }

    // Creation of JWT
    private String CreateToken(DtoOutputClient user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim("IdClient", user.IdClient.ToString()),
            new Claim(ClaimTypes.Email, user.Mail),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisismySecretKey"));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt: Issuer"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(300),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    // Disconnection of a Client and deletion of authentications cookies
    [HttpPost]
    [Route("disconnect")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Disconnect()
    {
        Response.Cookies.Delete("CookieSuper");
        Response.Cookies.Delete("CookieRole");
        Response.Cookies.Delete("CookieId");
        return Ok();
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<DtoOutputClient> Create(DtoInputCreateClient dto)
    {
        var output = _useCaseCreateClient.Execute(dto);
        return CreatedAtAction(
            nameof(FetchById),
            new { id = output.IdClient },
            output
        );
    }

    [HttpDelete]
    [Authorize]
    [Route("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputClient> Delete(int id)
    {
        try
        {
            return Ok(_useCaseDeleteClient.Execute(id));
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