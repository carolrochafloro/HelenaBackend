﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; }
        public Guid LastUpdatedBy { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
