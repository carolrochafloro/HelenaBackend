using Helena.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helena.Core.Entities;
internal class Times : ITimes
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime DateTime { get; set; }
    public Guid MedicationId { get; set; }
    public bool IsTaken { get; set; }
}
