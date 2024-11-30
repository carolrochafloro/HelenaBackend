
using Domain.Contracts.DTO;
using Domain.Contracts.Enum;
using Domain.Entities;
using Domain.Interfaces.Business;
using Domain.Interfaces.Data;
using Helena.Web.Core.DTO;
using Microsoft.AspNetCore.Mvc;


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
            BirthDate = register.BirthDate
        };

        var save = await _appUserData.CreateUserAsync(newUser);

        // salvar no db

        return Ok(new ResponseDTO { Status = StatusResponseEnum.Success, Message = "Usuário criado com sucesso." });

    }

    [Route("login")]
    [HttpPost]
    
    public async Task<IActionResult> Login([FromBody] LoginDTO login)
    {
        var user = _appUserData.GetUser(login.Email);

        if (user is null)
        {
            return Ok(new ResponseDTO { Status = StatusResponseEnum.Error, Message = "Usuário não registrado" });
        }

        bool validPassword = _appUserBusiness.IsValidPassword(login.Password, user.PasswordSalt, user.PasswordHash);

        if (!validPassword)
        {
            return Ok(new ResponseDTO { Status = StatusResponseEnum.Error, Message = "Usuário ou senha estão incorretos." });
        }

        // gerar cookie 

        return Ok();
    }

}
