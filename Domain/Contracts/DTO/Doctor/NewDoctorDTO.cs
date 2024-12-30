using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.DTO.Doctor;
public class NewDoctorDTO
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Specialty { get; set; } = string.Empty;
    public string Contact { get; set; }
    public string UserId { get; set; }
}
