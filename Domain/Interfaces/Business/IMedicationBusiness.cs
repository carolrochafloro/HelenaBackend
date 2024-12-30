using Domain.Contracts.DTO.Medication;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Business;
public interface IMedicationBusiness
{
    Task<Medication> CreateMedicationWithTimes(NewMedicationDTO newMedication, Guid userId);

}
