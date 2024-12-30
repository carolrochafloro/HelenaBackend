using Domain.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Medication : BaseEntity
{
    public string Name { get; set; }
    public string Lab { get; set; }
    public string Type { get; set; }
    public string Dosage { get; set; }
    public string Notes { get; set; }
    public string Img { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public FrequencyTypeEnum FrequencyType { get; set; }
    public int Recurrency { get; set; }
    public string IndicatedFor { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid DoctorId { get; set; }

    [JsonIgnore]
    [ForeignKey("DoctorId")]
    public Doctor Doctor { get; set; }

    [JsonIgnore]
    [ForeignKey("UserId")]
    public AppUser User { get; set; }
    [JsonIgnore]
    public List<Times> Times { get; set; } = new List<Times>();

}
