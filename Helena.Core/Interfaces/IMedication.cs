using Helena.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helena.Core.Interfaces;
public interface IMedication : IEntity
{
    string Name { get; set; }
    string Lab {  get; set; }
    string Type { get; set; }
    string Dosage { get; set; }
    string Notes { get; set; }
    string Img { get; set; }
    DateOnly StartDate { get; set; }
    DateTime EndDate { get; set; }
    FrequencyType FrequencyType { get; set; }
    int Recurrency { get; set; }
    Guid DoctorId { get; set; }
    Guid UserId { get; set; }

}
