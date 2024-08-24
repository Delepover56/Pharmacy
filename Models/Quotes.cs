using System;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models
{
    // Enum definition
    public enum UrgencyLevel
    {
        Low,
        Medium,
        High
    }

    public class Quotes
    {
        public int Id { get; set; } // Primary key

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty; // Initialized to avoid warnings

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty; // Initialized to avoid warnings

        [Phone]
        public string? PhoneNumber { get; set; } // Optional and nullable

        [Required]
        [StringLength(200)]
        public string Subject { get; set; } = string.Empty; // Initialized to avoid warnings

        [Required]
        [StringLength(1000)]
        public string Message { get; set; } = string.Empty; // Initialized to avoid warnings

        public string? PreferredContactMethod { get; set; } // Nullable

        [Required]
        public UrgencyLevel Urgency { get; set; }

        public DateTime DateSubmitted { get; set; } = DateTime.UtcNow; // Initialized to avoid warnings
    }
}
