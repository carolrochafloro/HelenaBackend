using Domain.Contracts.DTO;
using Domain.Contracts.DTO.Doctor;
using Domain.Entities;
using Domain.Interfaces.Data;
using Helena.Web.Data.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Data;
public class DoctorData : IDoctorData
{
    private readonly Context _context;
    private ILogger<DoctorData> _logger;

    public DoctorData(Context context,
                      ILogger<DoctorData> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ResponseDTO> CreateDoctorAsync(Doctor doctor)
    {
        _logger.LogInformation($"Cadastrando médico {doctor.Name}");

        try
        {
            await _context.Set<Doctor>().AddAsync(doctor);
            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                Status = Domain.Contracts.Enum.StatusResponseEnum.Success,
                Message = "Médico cadastrado com sucesso."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao cadastrar médico: {ex.Message}");
            throw;
        }

    }

    public async Task<ResponseDTO> DeleteDoctorAsync(Guid id)
    {
        try
        {
            var doctor = _context.Set<Doctor>().Where(d => d.Id == id).FirstOrDefault();

            if (doctor is null)
            {
                return new ResponseDTO
                {
                    Status = Domain.Contracts.Enum.StatusResponseEnum.Error,
                    Message = "Médico não encontrado"
                };
            }

            _context.Set<Doctor>().Remove(doctor);
            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                Status = Domain.Contracts.Enum.StatusResponseEnum.Success,
                Message = "Médico removido com sucesso."
            };
        }
        catch (Exception ex)
        {

            _logger.LogError($"Erro ao remover o cadastro do médico: {ex.Message}");
            throw;
        }
    }


    public List<ResponseDoctorDTO>? GetDoctors(Guid id)
    {
        _logger.LogInformation($"Buscando médicos do usuário {id}");

        try
        {
            var doctors = _context.Set<Doctor>().Where(d => d.UserId == id).ToList();
            var doctorsDTO = new List<ResponseDoctorDTO>();
            foreach ( var doctor in doctors)
            {
                var doctorDto = new ResponseDoctorDTO
                {
                    Id = doctor.Id,
                    Name = doctor.Name,
                    Contact = doctor.Contact,
                    Specialty = doctor.Specialty,
                    UserId = doctor.UserId,
                };

                doctorsDTO.Add(doctorDto);
            }

            return doctorsDTO;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao buscar médicos: {ex.Message}");
            throw;
        }
    }

    public Doctor? GetDoctorById(Guid id)
    {
        _logger.LogInformation($"Buscando médico com id {id}");

        try
        {
            var doctor = _context.Set<Doctor>().Where(d => d.Id == id).FirstOrDefault();

            return doctor;

        }
        catch (Exception ex)
        {

            _logger.LogError($"Erro ao buscar médico: {ex.Message}");
            throw;
        }
    }

    public async Task<ResponseDTO> UpdateDoctorAsync(Guid id, NewDoctorDTO newdoctor)
    {
        try
        {
            var doctor = _context.Set<Doctor>().Where(d => d.Id == id).FirstOrDefault();

            if (doctor is null)
            {
                return new ResponseDTO
                {
                    Status = Domain.Contracts.Enum.StatusResponseEnum.Error,
                    Message = "Médico não encontrado"
                };
            }

            doctor.Name = !string.IsNullOrEmpty(newdoctor.Name) ? newdoctor.Name : doctor.Name;
            doctor.Specialty = !string.IsNullOrEmpty(newdoctor.Specialty) ? newdoctor.Specialty : doctor.Specialty;
            doctor.Contact = !string.IsNullOrEmpty(newdoctor.Contact) ? newdoctor.Contact : doctor.Contact;

            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                Status = Domain.Contracts.Enum.StatusResponseEnum.Success,
                Message = "Cadastro do médico atualizado com sucesso."
            };

        }

        catch (Exception ex)
        {
            _logger.LogError($"Erro ao buscar médico: {ex.Message}");
            throw;
        }

    }
}
