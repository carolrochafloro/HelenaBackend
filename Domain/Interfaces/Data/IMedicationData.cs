using Domain.Contracts.DTO;
using Domain.Contracts.DTO.Medication;
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
        Task<ResponseDTO> UpdateMedicationAsync(UpdateMedDTO medication, Guid medId);
        Task<ResponseDTO> DeleteMedicationAsync(Guid mediId);
        MedicationWithDoctorDTO? GetMedicationById(Guid medId);
        List<MedicationWithDoctorDTO> GetAllMedications(Guid userId);
        List<DayMedicationDTO> GetAllMedicationsByDate(DateOnly date, Guid userId);

    }
}
