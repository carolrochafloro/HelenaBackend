using Helena.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helena.Core.Entities;
internal class Doctor : IDoctor
{
    public string Name { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid Id { get; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public DateOnly CreatedAt { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public DateOnly LastUpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;

    public Doctor()
    {
      CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
