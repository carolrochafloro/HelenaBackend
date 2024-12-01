using Domain.Contracts.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Data;
public interface IDoctorData
{
    Task<ResponseDTO> CreateDoctorAsync(Doctor doctor);
    List<Doctor>? GetDoctors(Guid id);
    Task<ResponseDTO> UpdateDoctorAsync(Guid id);
    Task<ResponseDTO> DeleteDoctorAsync(Guid id);
}
