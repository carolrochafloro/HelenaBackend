using Helena.Core.Enum;
using Helena.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helena.Core.Entities;
internal class Medication : IMedication
{
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
    public Guid DoctorId { get ; set ; }
    public Guid UserId { get ; set ; }
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateOnly CreatedAt { get ; set ; }
    public string UpdatedBy { get ; set ; }
    public DateOnly LastUpdatedAt { get ; set ; }
    public bool IsActive { get ; set ; }

}
