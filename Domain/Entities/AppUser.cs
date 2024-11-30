using Domain.Contracts.Enum;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;


namespace Domain.Entities;
public class AppUser : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDate { get; set; }
    public RoleEnum Role { get; set; } = RoleEnum.User;
}
