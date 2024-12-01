using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.DTO.AppUser;
public class JwtDTO
{
    public string Token { get; set; }
    public string Message { get; set; }
}
