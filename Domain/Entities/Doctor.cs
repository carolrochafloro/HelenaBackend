using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.Entities;
public class Doctor : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public string Contact {  get; set; } = string.Empty;

    [Required]
    [ForeignKey("Id")]
    public Guid UserId { get; set; }

    [JsonIgnore]
    public AppUser User { get; set; }

}
