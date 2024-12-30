using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.DTO.AppUser;
public class UserDTO
{
    public string Name { get; set; }
    public string LastName { get; set; }

    [EmailAddress]
    public string Email { get; set; }
    public DateOnly BirthDate { get; set; }
}
