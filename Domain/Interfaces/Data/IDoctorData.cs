﻿using Domain.Contracts.DTO;
using Domain.Contracts.DTO.Doctor;
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
    List<ResponseDoctorDTO>? GetDoctors(Guid id);
    Doctor? GetDoctorById(Guid id);
    Task<ResponseDTO> UpdateDoctorAsync(Guid id, NewDoctorDTO newDoctor);
    Task<ResponseDTO> DeleteDoctorAsync(Guid id);
}
