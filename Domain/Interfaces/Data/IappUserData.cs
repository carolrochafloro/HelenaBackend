using Domain.Contracts.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Data;
public interface IAppUserData
{
    Task<ResponseDTO> CreateUserAsync(AppUser user);
    AppUser? GetUserById(Guid id);
    AppUser? GetUser(string email);
    Task<ResponseDTO> DeleteUserAsync(Guid id);

}
