using Domain.Contracts.DTO;
using Domain.Entities;
using Domain.Interfaces.Data;
using Helena.Web.Data.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data;
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
            await _context.AddAsync(doctor);
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
            return new ResponseDTO
            {
                Status = Domain.Contracts.Enum.StatusResponseEnum.Error,
                Message = ex.Message
            };
        }

    }

    public Task<ResponseDTO> DeleteDoctorAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<Doctor>? GetDoctors(Guid id)
    {
        _logger.LogInformation($"Buscando médicos do usuário {id}");

        try
        {
            var doctors = _context.Set<Doctor>().Where(d => d.UserId == id).ToList();
            return doctors;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao buscar médicos: {ex.Message}");
            return null;
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
            return null;
        }
    }

    public Task<ResponseDTO> UpdateDoctorAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
