using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helena.Core.Interfaces;

public interface IEntity
{
    Guid Id { get;}
    DateOnly CreatedAt { get; set; }
    string UpdatedBy { get; set; }
    DateOnly LastUpdatedAt { get; set; }
    bool IsActive { get; set; }
}
