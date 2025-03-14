﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.DTO.AppUser;

public class RegisterDTO
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Name { get; set; }

    public string LastName { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
    public DateOnly BirthDate { get; set; }

}
