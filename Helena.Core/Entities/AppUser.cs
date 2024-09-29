using Helena.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helena.Core.Entities;
internal class AppUser : IdentityUser<Guid>, IAppUser
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

}
