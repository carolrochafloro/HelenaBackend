using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Business;
public interface IAppUserBusiness
{
    bool IsValidPassword(string password, string salt, string hash);
    string HashPassword(string password, string salt);
    string SaltGenerator();
}
