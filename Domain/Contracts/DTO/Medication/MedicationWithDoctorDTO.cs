using Domain.Contracts.DTO.Times;
using Domain.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.DTO.Medication;
public class MedicationWithDoctorDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Lab { get; set; }
    public string Type { get; set; } 
    public string Dosage { get; set; } 
    public string Notes { get; set; }
    public DateOnly Start { get; set; }
    public DateOnly End { get; set; }   
    public FrequencyTypeEnum FrequencyType { get; set; }
    public int Recurrency { get; set; }
    public string DoctorName { get; set; }
    public string DoctorSpecialty { get; set; }
    public string? IndicatedFor { get; set; }
    public List<TimeDTO> Times { get; set; }
}
