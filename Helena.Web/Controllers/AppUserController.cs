
using Domain.Contracts.DTO;
using Domain.Contracts.DTO.AppUser;
using Domain.Contracts.Enum;
using Domain.Entities;
using Domain.Interfaces.Business;
using Domain.Interfaces.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Helena.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AppUserController : ControllerBase
{
    private readonly ILogger<AppUserController> _logger;
    private readonly IAppUserBusiness _appUserBusiness;
    private readonly IAppUserData _appUserData;

    public AppUserController(ILogger<AppUserController> logger,
                             IAppUserBusiness appUserBusiness,
                             IAppUserData appUserData)
    {
        _logger = logger;
        _appUserBusiness = appUserBusiness;
        _appUserData = appUserData;
    }

    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDTO register)
    {
        try
        {
            var user = _appUserData.GetUserByEmail(register.Email);

            if (user is not null)
            {
                return Ok(new ResponseDTO
                {
                    Status = false,
                    Message = "Usuário já cadastrado."
                });
            }

            var salt = _appUserBusiness.SaltGenerator();
            var hash = _appUserBusiness.HashPassword(register.Password, salt);

            AppUser newUser = new()
            {

                Email = register.Email,
                Name = register.Name,
                LastName = register.LastName,
                PasswordSalt = salt,
                PasswordHash = hash,
                BirthDate = (DateOnly)register.BirthDate
            };

            var save = await _appUserData.CreateUserAsync(newUser);

            if (save.Status)
            {
                var jwt = _appUserBusiness.GenerateJwt(newUser);

                return Ok(new ResponseDTO
                {
                    Status = true,
                    Message = jwt
                });
            }

            return Ok(new ResponseDTO
            {
                Status = true,
                Message = "Usuário cadastrado."
            });
        }

        catch (Exception ex)
        {
            return BadRequest(new ResponseDTO
            {
                Status = false,
                Message = ex.Message,
            });
        }
    }

    [Route("login")]
    [HttpPost]

    public IActionResult Login([FromBody] LoginDTO login)
    {

        var result = _appUserBusiness.Authenticate(login);

        if (result.Item1.Status == false)
        {
            return Ok(new ResponseDTO
            {
                Status = false,
                Message = result.Item1.Message
            });
        }

        if (result.Item1 is null || result.Item2 is null)
        {
            return BadRequest(new ResponseDTO
            {
                Status = false,
                Message = "Erro no servidor. Tente novamente mais tarde."
            });
        }

        var jwt = _appUserBusiness.GenerateJwt(result.Item2);

        return Ok(new ResponseDTO
        {
            Status = true,
            Message = jwt
        });
    }

    [Route("{userId}")]
    [HttpGet]
    [Authorize]
    public IActionResult GetLoggedUser([FromRoute] Guid userId)
    {

        try
        {
            _logger.LogInformation("Buscando usuário!");
            var user = _appUserData.GetUserById(userId);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);

        }

        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Route("{userId}")]
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteProfile([FromRoute] Guid userId)
    {
        try
        {
            var response = await _appUserData.DeleteUserAsync(userId);
            return Ok(response);
        }

        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("{userId}")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromRoute] Guid userId, [FromBody] UpdateUserDTO updateUser)
    {
        try
        {
            _logger.LogInformation("Atualizando usuário.");
            var response = await _appUserData.UpdateUserAsync(updateUser, userId);
            return Ok(response);
        }

        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
