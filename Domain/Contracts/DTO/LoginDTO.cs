using System.ComponentModel.DataAnnotations;

namespace Helena.Web.Core.DTO;

public class LoginDTO
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
