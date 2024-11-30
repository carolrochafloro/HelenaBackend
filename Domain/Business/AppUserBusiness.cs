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

    public string HashPassword(string password, string salt)
    {

        return "";
    }

    public string SaltGenerator()
    {

        byte[] salt = new byte[16];

        RandomNumberGenerator.Fill(salt);

        return Convert.ToBase64String(salt);
    }
}
