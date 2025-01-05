
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
            var user = _appUserData.GetUser(register.Email);

            if (user is not null)
            {
                return BadRequest("O e-mail já está registrado.");
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

            if (save.Status == StatusResponseEnum.Success)
            {
                var jwt = _appUserBusiness.GenerateJwt(newUser);
                return Ok(new JwtDTO
                {
                    Message = "Autenticado",
                    Token = jwt
                });
            }



            return Ok(save.Message);
        }

        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }


    }

    [Route("login")]
    [HttpPost]

    public IActionResult Login([FromBody] LoginDTO login)
    {

        var result = _appUserBusiness.Authenticate(login);

        if (result.Item1.Status == StatusResponseEnum.Error)
        {
            return Unauthorized("Usuário não autorizado.");
        }

        if (result.Item1 is null || result.Item2 is null)
        {
            return BadRequest("Erro do servidor");
        }

        var jwt = _appUserBusiness.GenerateJwt(result.Item2);

        return Ok(new JwtDTO
        {
            Token = jwt,
            Message = result.Item1.Message,
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

    [HttpDelete]
    [Authorize]

    public async Task<IActionResult> DeleteProfile()
    {
        var userIdString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        Guid.TryParse(userIdString, out Guid userId);

        try
        {
            var response = await _appUserData.DeleteUserAsync(userId);

            return Ok(response.Message);
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
            return Ok(response.Message);

        }

        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
