using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helena.Core.Interfaces;
public interface ITimes
{
    Guid Id { get; }
    DateTime DateTime { get; set; }
    Guid MedicationId { get; set; }
    bool IsTaken { get; set; }
}
