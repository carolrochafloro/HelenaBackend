using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Helena.Core.Entities;
public class Times
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime DateTime { get; set; }
    public bool IsTaken { get; set; }

    [JsonIgnore]
    public Guid MedicationId { get; set; }

}
