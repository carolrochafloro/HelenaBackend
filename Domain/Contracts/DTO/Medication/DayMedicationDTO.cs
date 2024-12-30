using Domain.Contracts.DTO.Times;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.DTO.Medication;
public class DayMedicationDTO
{
    public Guid MedicationId { get; set; }
    public string Name { get; set; }
    public string Notes { get; set; }
    public string Dosage { get; set; }
    public string Type { get; set; }
    public List<TimeDTO> Times { get; set; }
}
