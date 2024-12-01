using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.DTO.AppUser;

public class LoginDTO
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
