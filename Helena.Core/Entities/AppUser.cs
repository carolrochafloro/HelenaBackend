using Microsoft.AspNetCore.Identity;

namespace Helena.Core.Entities;
public class AppUser : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

}
