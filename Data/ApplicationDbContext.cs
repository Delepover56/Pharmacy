using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Models;

namespace Pharmacy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<Quotes> Quotes { get; set; }
        public DbSet<CareerForm> CareerForms { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var user = new IdentityRole("user");
            user.NormalizedName = "user";

            var candidate = new IdentityRole("candidate");
            candidate.NormalizedName = "candidate";

            builder.Entity<IdentityRole>().HasData(admin, user, candidate);
        }
    }
}
