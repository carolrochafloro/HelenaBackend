using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Times
{
    [Key]
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime DateTime { get; set; }
    public bool IsTaken { get; set; }

    [Required]
    [ForeignKey("Id")]
    public Guid MedicationId { get; set; }

    [JsonIgnore]
    public Medication Medication { get; set; }

}
