using Domain.Contracts.DTO.Times;
using Domain.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.DTO.Medication;
public class NewMedicationDTO
{
    [Required]
    public string Name { get; set; }
    public string Lab { get; set; }
    public string Type { get; set; }
    [Required]
    public string Dosage { get; set; }
    public string Notes { get; set; }
    public string IndicatedFor { get; set; } = string.Empty;
    [Required]
    public string Start { get; set; }
    [Required]
    public string End { get; set; }
    [Required]
    public FrequencyTypeEnum FrequencyType { get; set; }
    [Required]
    public int Recurrency { get; set; }
    [Required]
    public string DoctorId { get; set; }
    public string UserId { get; set; }
    public List<NewTimeDTO> Times { get; set; }
}
