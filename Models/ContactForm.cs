using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models
{
    public class ContactForm
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(2000)] // Increase the length here
        public string? Message { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
