using Domain.Contracts.DTO;
using Domain.Contracts.Enum;
using Domain.Entities;
using Domain.Interfaces.Data;
using Helena.Web.Data.Context;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data;
public class AppUserData : IAppUserData
{
    private readonly Context _context;
    private readonly ILogger<AppUserData> _logger;

    public AppUserData(Context context,
                       ILogger<AppUserData> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ResponseDTO> CreateUserAsync(AppUser user)
    {
        _logger.LogInformation($"Criando usuário {user.Id}");

        try
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return new ResponseDTO { Status = StatusResponseEnum.Success, Message = $"Usuário {user.Id} - {user.Email} criado com sucesso." };
        }
        catch (Exception ex)
        {

            _logger.LogError(ex.Message);
            return new ResponseDTO { Status = StatusResponseEnum.Error, Message = ex.Message };

        }

    }


    public async Task<AppUser?> GetUserAsync(string email)
    {

        _logger.LogInformation($"Buscando usuário pelo e-mail {email}");

        try
        {

            var user = await _context.Set<AppUser>(email).FirstOrDefaultAsync();

            if (user is null)
            {
                return null;
            }

            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }

    }

    public async Task<ResponseDTO> DeleteUserAsync(string email)
    {

        _logger.LogInformation($"Deletando usuário {email}");

        try
        {
            var user = await GetUserAsync(email);

            if (user is null)
            {
                return new ResponseDTO
                {
                    Status = StatusResponseEnum.Error,
                    Message = $"Usuário com e-mail {email} não encontrado."
                };
            }

            user.IsActive = false;

            _context.Set<AppUser>().Update(user);
            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                Status = StatusResponseEnum.Success,
                Message = $"Usuário com e-mail {email} deletado."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return new ResponseDTO
            {
                Status = StatusResponseEnum.Error,
                Message = ex.Message
            };
        }


    }
}
