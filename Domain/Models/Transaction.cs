using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace Domain.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string ReferenceNumber { get; set; } // Unique transaction reference
        public int InitiatorId { get; set; }
        public int? ApproverId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } // Pending, Approved, Rejected
        public string Type { get; set; } // SingleTransfer, BulkTransfer, BillPayment
        public DateTime CreatedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }

        public string InitiatorsName { get; set; }

        public string ApproverName { get; set; }
        public int TenantId { get; set; }

        public ICollection<TransactionBeneficiary> Beneficiaries { get; set; }
    }
}
