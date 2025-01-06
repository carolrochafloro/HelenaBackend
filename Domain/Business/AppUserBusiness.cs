using Domain.Contracts.DTO;
using Domain.Contracts.DTO.AppUser;
using Domain.Entities;
using Domain.Interfaces.Business;
using Domain.Interfaces.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Business;
public class AppUserBusiness : IAppUserBusiness
{

    private readonly IAppUserData _appUserData;

    public AppUserBusiness(IAppUserData appUserData)
    {
        _appUserData = appUserData;
    }

    public bool IsValidPassword(string password, string salt, string hash)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);
        using (var hmac = new HMACSHA256(saltBytes))
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] computedHash = hmac.ComputeHash(passwordBytes);
            string computedHashString = Convert.ToBase64String(computedHash);

            return computedHashString.Equals(hash);
        }
    }

    public string HashPassword(string password, string salt)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);
        using (var hmac = new HMACSHA256(saltBytes))
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = hmac.ComputeHash(passwordBytes);

            return Convert.ToBase64String(hash);
        }
    }
    public string SaltGenerator()
    {

        byte[] salt = new byte[16];

        RandomNumberGenerator.Fill(salt);

        return Convert.ToBase64String(salt);
    }

    public (ResponseDTO, AppUser?) Authenticate(LoginDTO login)
    {
        var user = _appUserData.GetUserByEmail(login.Email);

        if (user is null)
        {
            return (new ResponseDTO
            {
                Status = false,
                Message = "Usuário não encontrado."
            }, null);
        }

        if (!IsValidPassword(login.Password, user.PasswordSalt, user.PasswordHash))
        {
            return (new ResponseDTO
            {
                Status = false,
                Message = "Usuário ou senha inválidos."
            }, null);
        }

        if (!user.IsActive)
        {
            return (new ResponseDTO
            {
                Status = false,
                Message = "Usuário inativo."
            }, null);
        }

        return (new ResponseDTO
        {
            Status = true,
            Message = "Usuário autenticado."
        }, user);
    }

    public string GenerateJwt(AppUser user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hardCodedKeyForNow1234567890!_testeeeee"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
           issuer: "HelenaApp",
           audience: "HelenaApp",
           claims: claims,
           expires: DateTime.Now.AddDays(2),
           signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
