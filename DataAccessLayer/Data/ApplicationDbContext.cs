using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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


    }
}
