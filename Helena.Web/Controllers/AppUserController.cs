using Domain.Contracts.DTO;
using Domain.Contracts.Enum;
using Domain.Entities;
using Helena.Web.Core.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Helena.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AppUserController : ControllerBase
{
    private readonly ILogger<AppUserController> _logger;

    public AppUserController(ILogger<AppUserController> logger)
    {
        _logger = logger;
    }

    [Route("register")]
    [HttpPost]

    public async Task<IActionResult> Register([FromBody] RegisterDTO register)
    {
        // validar se usuário já existe

        AppUser user = new()
        {

            Email = register.Email,
            Name = register.Name,
            LastName = register.LastName,
        };

        // fazer hash de senha
        // salvar no db

        return Ok(new ResponseDTO { Status = StatusResponseEnum.Success, Message = "Usuário criado com sucesso." });

    }

}
