using Domain.Contracts.DTO;
using Domain.Contracts.DTO.AppUser;
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

namespace Infra.Data.Data;
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
            await _context.Set<AppUser>().AddAsync(user);
            await _context.SaveChangesAsync();

            return new ResponseDTO { Status = true, Message = $"Usuário {user.Id} - {user.Email} criado com sucesso." };
        }
        catch (Exception ex)
        {

            _logger.LogError(ex.Message);
            throw;

        }

    }
    public AppUser? GetUserById(Guid id)
    {

        _logger.LogInformation($"Buscando usuário pelo id {id}");

        try
        {

            var user = _context.Set<AppUser>().FirstOrDefault(u => u.Id == id);

            if (user is null)
            {
                return null;
            }

            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }

    }
    public AppUser? GetUserByEmail(string email)
    {

        try
        {
            var user = _context.Set<AppUser>().FirstOrDefault(u => u.Email == email);

            if (user is null)
            {
                return null;
            }

            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }

    }
    public async Task<ResponseDTO> DeleteUserAsync(Guid id)
    {

        _logger.LogInformation($"Deletando usuário {id}");

        try
        {
            _logger.LogInformation("Deletando usuário.");

            var user = GetUserById(id);

            _logger.LogInformation($"Usuário encontrado: {user.Email}");

            if (user is null)
            {
                return new ResponseDTO
                {
                    Status = false,
                    Message = $"Usuário com id {id} não encontrado."
                };
            }
            
            _context.Set<AppUser>().Remove(user);
            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                Status = true,
                Message = $"Usuário com id {id} deletado."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }


    }

    public async Task<ResponseDTO> UpdateUserAsync(UpdateUserDTO user, Guid id)
    {
        try
        {
            var currentUser = _context.Set<AppUser>().Where(u => u.Id == id).FirstOrDefault();

            currentUser.Name = !string.IsNullOrEmpty(user.Name) ? user.Name : currentUser.Name;
            currentUser.LastName = !string.IsNullOrEmpty(user.LastName) ? user.LastName : currentUser.LastName;
            currentUser.Email = !string.IsNullOrEmpty(user.Email) ? user.Email : currentUser.Email;
            currentUser.BirthDate = user.BirthDate != default ? user.BirthDate : currentUser.BirthDate;

            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                Status = true,
                Message = "Usuário atualizado com sucesso."
            };

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
