namespace Pharmacy.Models
{
    public class CareerForm
    {
        public int Id { get; set; } // Unique identifier for the application
        public string FullName { get; set; } // Full name of the applicant
        public string Email { get; set; } // Email address of the applicant
        public string PhoneNumber { get; set; } // Phone number of the applicant
        public string Education { get; set; } // Position the applicant is applying for
        public DateTime DateOfApplication { get; set; } // Date the application was submitted
        public bool Status { get; set; }
    }
}
