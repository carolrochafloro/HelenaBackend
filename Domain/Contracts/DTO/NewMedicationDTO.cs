using Domain.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.DTO;
public class NewMedicationDTO
{
    [Required]
    public string Name { get; set; }
    public string Lab { get; set; }
    public string Type { get; set; }
    [Required]
    public string Dosage { get; set; }
    public string Notes { get; set; }
    public string IndicatedFor { get; set; }
    [Required]
    public DateOnly StartDate { get; set; }
    [Required]
    public DateOnly EndDate { get; set; }
    [Required]
    public FrequencyTypeEnum FrequencyType { get; set; }
    [Required]
    public int Recurrency { get; set; }
    [Required]
    public Guid DoctorId { get; set; }
    public List<NewTimeDTO> Times { get; set; }
}
