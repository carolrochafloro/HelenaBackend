using Domain.Contracts.DTO;
using Domain.Contracts.DTO.Medication;
using Domain.Contracts.DTO.Times;
using Domain.Entities;
using Domain.Interfaces.Data;
using Helena.Web.Data.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Data;
public class MedicationData : IMedicationData
{
    private readonly ILogger<MedicationData> _logger;
    private readonly Context _context;

    public MedicationData(ILogger<MedicationData> logger, Context context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<ResponseDTO> CreateMedicationAsync(Medication medication)
    {
        _logger.LogInformation($"Criando medicamento {medication.Id}");

        try
        {

            await _context.Set<Medication>().AddAsync(medication);
            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                Status = true,
                Message = "Medicamento criado com sucesso"
            };

        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao criar medicamento: {ex.Message}");
            throw;
        }
    }

    public async Task<ResponseDTO> DeleteMedicationAsync(Guid mediId)
    {
        _logger.LogInformation($"Deletando cadastro de medicamento {mediId}");

        try
        {
            var medication = _context.Set<Medication>().Where(m => m.Id == mediId).FirstOrDefault();
            if (medication is null)
            {
                return new ResponseDTO
                {
                    Status = false,
                    Message = "Medicamento não encontrado."
                };
            }

            _context.Set<Medication>().Remove(medication);
            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                Status = true,
                Message = "Medicamento deletado com sucesso."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao deletar medicamento: {ex.Message}");
            throw;
        }
    }

    public List<MedicationWithDoctorDTO> GetAllMedications(Guid userId)
    {
        _logger.LogInformation($"Buscando medicamentos para o usuário {userId}");

        try
        {
            var query = from medication in _context.Medications
                        where medication.UserId == userId
                        join doctor in _context.Doctors on medication.DoctorId equals doctor.Id
                        join time in _context.Times on medication.Id equals time.MedicationId
                        group new { medication, doctor, time } by new
                        {
                            medication.Id,
                            MedicationName = medication.Name,
                            medication.Lab,
                            medication.Type,
                            medication.Dosage,
                            medication.Notes,
                            medication.StartDate,
                            medication.EndDate,
                            medication.FrequencyType,
                            medication.Recurrency,
                            DoctorName = doctor.Name,
                            doctor.Specialty,
                            medication.IndicatedFor
                        } into g
                        select new MedicationWithDoctorDTO
                        {
                            Id = g.Key.Id,
                            Name = g.Key.MedicationName,
                            Lab = g.Key.Lab,
                            Type = g.Key.Type,
                            Dosage = g.Key.Dosage,
                            Notes = g.Key.Notes,
                            Start = g.Key.StartDate,
                            End = g.Key.EndDate,
                            FrequencyType = g.Key.FrequencyType,
                            Recurrency = g.Key.Recurrency,
                            DoctorName = g.Key.DoctorName,
                            DoctorSpecialty = g.Key.Specialty,
                            IndicatedFor = g.Key.IndicatedFor,
                            Times = g.Select(x => new TimeDTO
                            {
                                Id = x.time.Id,
                                DateTime = x.time.DateTime.ToLocalTime(),
                                IsTaken = x.time.IsTaken,
                            }).OrderBy(t => t.DateTime).ToList()
                        };

            var result = query.ToList();

            return result;

        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao buscar os medicamentos: {ex.Message}");
            throw;
        }
    }

    public MedicationWithDoctorDTO? GetMedicationById(Guid medId)
    {
        _logger.LogInformation($"Buscando medicamento: {medId}");

        try
        {
            var query = from medication in _context.Medications
                        where medication.Id == medId
                        join doctor in _context.Doctors on medication.DoctorId equals doctor.Id
                        join time in _context.Times on medication.Id equals time.MedicationId
                        group new { medication, doctor, time } by new
                        {
                            medication.Id,
                            MedicationName = medication.Name,
                            medication.Lab,
                            medication.Type,
                            medication.Dosage,
                            medication.Notes,
                            medication.StartDate,
                            medication.EndDate,
                            medication.FrequencyType,
                            medication.Recurrency,
                            DoctorName = doctor.Name,
                            doctor.Specialty,
                            medication.IndicatedFor
                        } into g
                        select new MedicationWithDoctorDTO
                        {
                            Id = g.Key.Id,
                            Name = g.Key.MedicationName,
                            Lab = g.Key.Lab,
                            Type = g.Key.Type,
                            Dosage = g.Key.Dosage,
                            Notes = g.Key.Notes,
                            Start = g.Key.StartDate,
                            End = g.Key.EndDate,
                            FrequencyType = g.Key.FrequencyType,
                            Recurrency = g.Key.Recurrency,
                            DoctorName = g.Key.DoctorName,
                            DoctorSpecialty = g.Key.Specialty,
                            IndicatedFor = g.Key.IndicatedFor,
                            Times = g.Select(x => new TimeDTO
                            {
                                Id = x.time.Id,
                                DateTime = x.time.DateTime,
                                IsTaken = x.time.IsTaken,
                            }).OrderBy(t => t.DateTime).ToList()
                        };

         
            return query.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao buscar o medicamento: {ex.Message}");
            throw;
        }
    }

    public async Task<ResponseDTO> UpdateMedicationAsync(UpdateMedDTO medication, Guid medId)
    {

        _logger.LogInformation($"Atualizando medicamento {medication.Name}");

        try
        {

            var med = _context.Set<Medication>().Where(m => m.Id == medId).FirstOrDefault();

            if (med is null)
            {
                return new ResponseDTO
                {
                    Status = false,
                    Message = "Medicamento não encontrado"
                };
            }

            var result = Guid.TryParse(medication.DoctorId, out Guid doctorId);

            med.Name = !string.IsNullOrEmpty(medication.Name) ? medication.Name : med.Name;
            med.Dosage = !string.IsNullOrEmpty(medication.Dosage) ? medication.Dosage : med.Dosage;
            med.IndicatedFor = !string.IsNullOrEmpty(medication.IndicatedFor) ? medication.IndicatedFor : med.IndicatedFor;
            med.DoctorId = medication.DoctorId != default ?doctorId : med.DoctorId;
            med.Type = !string.IsNullOrEmpty(medication.Type) ? medication.Type : med.Type;
            med.Lab = !string.IsNullOrEmpty(medication.Lab) ? medication.Lab : med.Lab;
            med.Notes = !string.IsNullOrEmpty(medication.Notes) ? medication.Notes : med.Notes;

            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                Status = true,
                Message = "Medicamento atualizado com sucesso."
            };

        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao atualizar o medicamento: {ex.Message}");
            throw;
        }

    }

    public List<DayMedicationDTO> GetAllMedicationsByDate(DateOnly date, Guid userId)
    {
        _logger.LogInformation("Buscando medicamentos para o dashboard");


        var medicationList = _context.Medications
        .Where(m => m.UserId == userId && _context.Times.Any(t => t.MedicationId == m.Id && t.DateTime.Date == date.ToDateTime(TimeOnly.MinValue).ToUniversalTime().Date))
        .Select(medication => new DayMedicationDTO
        {
            MedicationId = medication.Id,
            Name = medication.Name,
            Notes = medication.Notes,
            Dosage = medication.Dosage,
            Type = medication.Type,
            Times = _context.Times
                        .Where(t => t.MedicationId == medication.Id && t.DateTime.Date == date.ToDateTime(TimeOnly.MinValue).ToUniversalTime().Date).OrderBy(t => t.DateTime)
                        .Select(t => new TimeDTO
                        {
                            Id = t.Id,
                            DateTime = t.DateTime.ToLocalTime(),
                            IsTaken = t.IsTaken
                        }).ToList()
        });

        return medicationList.ToList();
    }
}
