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
    public string Img { get; set; }
    public DateOnly StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public FrequencyTypeEnum FrequencyType { get; set; }
    public int Recurrency { get; set; }

    [Required]
    [ForeignKey("Id")]
    public Guid UserId { get; set; }

    [Required]
    [ForeignKey("Id")]
    public Guid DoctorId { get; set; }

    [JsonIgnore]
    public Doctor Doctor { get; set; }

    [JsonIgnore]
    public AppUser User { get; set; }
}
