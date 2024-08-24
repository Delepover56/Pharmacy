using Microsoft.AspNetCore.Http; // Add this namespace
using System.ComponentModel.DataAnnotations.Schema; // Add this namespace

namespace Pharmacy.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string? ProdName { get; set; }
        public string? Description { get; set; }
        public float? Price { get; set; }
        public int? Stock { get; set; }
        public string? ImagePath { get; set; } // Stores the image URL or file path

        [NotMapped]
        public IFormFile? ImageFile { get; set; } // For file upload
    }
}
