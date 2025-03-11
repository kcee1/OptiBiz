using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext<User,Role, string>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionBeneficiary> TransactionBeneficiaries { get; set; }
        public DbSet<UserVerification> UserVerifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // Ensure Identity schema is applied

            SeedRoles(builder);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                new Role { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" },
                new Role { Id = Guid.NewGuid().ToString(), Name = "Initiator", NormalizedName = "INITIATOR" },
                new Role { Id = Guid.NewGuid().ToString(), Name = "Approver", NormalizedName = "APPROVER" },
                new Role { Id = Guid.NewGuid().ToString(), Name = "Viewer", NormalizedName = "VIEWER" }
            );
        }
    }
}
