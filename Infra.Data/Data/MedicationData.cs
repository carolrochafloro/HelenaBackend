using Domain.Contracts.DTO;
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
                Status = Domain.Contracts.Enum.StatusResponseEnum.Success,
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
            var medication = await _context.Set<Medication>().Where(m => m.Id == mediId).FirstOrDefaultAsync();
            if (medication is null)
            {
                return new ResponseDTO
                {
                    Status = Domain.Contracts.Enum.StatusResponseEnum.Error,
                    Message = "Medicamento não encontrado."
                };
            }

            _context.Set<Medication>().Remove(medication);
            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                Status = Domain.Contracts.Enum.StatusResponseEnum.Success,
                Message = "Medicamento deletado com sucesso."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao deletar medicamento: {ex.Message}");
            throw;
        }
    }

    public List<Medication> GetAllMedications(Guid userId)
    {
        _logger.LogInformation($"Buscando medicamentos para o usuário {userId}");
        try
        {
            var list = _context.Set<Medication>().Where(m => m.UserId == userId).ToList();
            return list;

        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao buscar os medicamentos: {ex.Message}");
            throw;
        }
    }

    public Medication? GetMedicationById(Guid medId)
    {
        _logger.LogInformation($"Buscando medicamento: {medId}");

        try
        {
            var medication = _context.Set<Medication>().Where(m => m.Id == medId).FirstOrDefault();
            return medication;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao buscar o medicamento: {ex.Message}");
            throw;
        }
    }

    public async Task<ResponseDTO> UpdateMedicationAsync(NewMedicationDTO medication, Guid medId)
    {

        _logger.LogInformation($"Atualizando medicamento {medication.Name}");

        try
        {

            var med = _context.Set<Medication>().Where(m => m.Id == medId).FirstOrDefault();

            if (med is null)
            {
                return new ResponseDTO
                {
                    Status = Domain.Contracts.Enum.StatusResponseEnum.Error,
                    Message = "Medicamento não encontrado"
                };
            }

            med.Name = !string.IsNullOrEmpty(medication.Name) ? medication.Name : med.Name;
            med.StartDate = medication.StartDate != default ? medication.StartDate : med.StartDate;
            med.Dosage = !string.IsNullOrEmpty(medication.Dosage) ? medication.Dosage : med.Dosage;
            med.EndDate = medication.EndDate != default ? medication.EndDate : med.EndDate;
            med.Recurrency = med.Recurrency;
            med.IndicatedFor = !string.IsNullOrEmpty(medication.IndicatedFor) ? medication.IndicatedFor : med.IndicatedFor;
            med.DoctorId = medication.DoctorId != default ? medication.DoctorId : med.DoctorId;
            med.Type = !string.IsNullOrEmpty(medication.Type) ? medication.Type : med.Type;
            med.Lab = !string.IsNullOrEmpty(medication.Lab) ? medication.Lab : med.Lab;
            med.Notes = !string.IsNullOrEmpty(medication.Notes) ? medication.Notes : med.Notes;

            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                Status = Domain.Contracts.Enum.StatusResponseEnum.Success,
                Message = "Medicamento atualizado com sucesso."
            };

        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao atualizar o medicamento: {ex.Message}");
            throw;
        }

    }
}
