using Helena.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Helena.Core.Entities;
public class Medication : BaseEntity { 
    public string Name { get ; set ; }
    public string Lab { get ; set ; }
    public string Type { get ; set ; }
    public string Dosage { get ; set ; }
    public string Notes { get ; set ; }
    public string Img { get ; set ; }
    public DateOnly StartDate { get ; set ; }
    public DateTime EndDate { get ; set ; }
    public FrequencyType FrequencyType { get ; set ; }
    public int Recurrency { get ; set ; }

    [JsonIgnore]
    public Guid DoctorId { get ; set ; }

    [JsonIgnore]
    public Guid UserId { get ; set ; }
}
