using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace Domain.Models
{
    public class User : IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string AccountNumber { get; set; } // 10-digit randomly generated number
        public bool IsEmailVerified { get; set; } 
        public DateTime CreatedAt { get; set; }

        public ICollection<Transaction> InitiatedTransactions { get; set; }
        public ICollection<Transaction> ApprovedTransactions { get; set; }

        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }
        public int TenantId { get; set; }

        public decimal AccountBalance { get; set; }


    }
}
