using Microsoft.EntityFrameworkCore;

namespace Pharmacy.Models
{
    public class PharmacyContext : DbContext
    {
        public PharmacyContext(DbContextOptions<PharmacyContext> options)
            : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }
    }
}
