using Domain.Interfaces.Business;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Business;
public class AppUserBusiness : IAppUserBusiness
{

    public bool IsValidPassword(string password, string salt, string hash)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);
        var hmac = new HMACSHA256(saltBytes);
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] computedHash = hmac.ComputeHash(passwordBytes);
        string computedHashString = Convert.ToBase64String(computedHash);

        return computedHashString.Equals(hash);
    }
    public string HashPassword(string password, string salt)
    {

        var hmac = new HMACSHA256();
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] hash = hmac.ComputeHash(passwordBytes);
        

        return Convert.ToBase64String(hash);
    }

    public string SaltGenerator()
    {

        byte[] salt = new byte[16];

        RandomNumberGenerator.Fill(salt);

        return Convert.ToBase64String(salt);
    }
}
