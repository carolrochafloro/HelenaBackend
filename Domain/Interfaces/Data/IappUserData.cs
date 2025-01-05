using Domain.Contracts.DTO;
using Domain.Contracts.DTO.AppUser;
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
    UserDTO? GetUserById(Guid id);
    AppUser? GetUser(string email);
    Task<ResponseDTO> DeleteUserAsync(Guid id);
    Task<ResponseDTO> UpdateUserAsync(UpdateUserDTO user, Guid id);

}
