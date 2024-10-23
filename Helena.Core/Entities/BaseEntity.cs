using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helena.Core.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime LastUpdateAt { get; set; }
        public Guid LastUpdatetBy { get; set; } 
        public bool IsActive { get; set; } = true;
    }
}
