using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.DTO.Doctor;
public class ResponseDoctorDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Specialty { get; set; }
    public string Contact { get; set; }
    public Guid UserId { get; set; }
}
