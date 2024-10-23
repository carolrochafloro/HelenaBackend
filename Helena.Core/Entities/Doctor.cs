using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace Helena.Core.Entities;
public class Doctor : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    [JsonIgnore]
    public Guid UserId { get; set; }

}
