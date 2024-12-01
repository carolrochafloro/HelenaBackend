using Domain.Contracts.DTO;
using Domain.Contracts.DTO.AppUser;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Business;
public interface IAppUserBusiness
{
    bool IsValidPassword(string password, string salt, string hash);
    string HashPassword(string password, string salt);
    string SaltGenerator();
    (ResponseDTO, AppUser?) Authenticate(LoginDTO login);
    string GenerateJwt(AppUser user);
}
