using Domain.Contracts.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Data
{
    public interface IMedicationData
    {
        Task<ResponseDTO> CreateMedicationAsync(Medication medication);
        Task<ResponseDTO> UpdateMedicationAsync(NewMedicationDTO medication, Guid medId);
        Task<ResponseDTO> DeleteMedicationAsync(Guid mediId);
        Medication? GetMedicationById(Guid medId);
        List<Medication> GetAllMedications(Guid userId);

    }
}
