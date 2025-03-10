
#nullable disable
namespace Domain.Models
{
    public class TransactionBeneficiary
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public decimal Amount { get; set; }

        public Transaction Transaction { get; set; }
    }
}
