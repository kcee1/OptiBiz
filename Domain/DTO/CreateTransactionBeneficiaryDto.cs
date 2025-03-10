#nullable disable
namespace Domain.DTO
{
    public class CreateTransactionBeneficiaryDto
    {
        public string BeneficiaryName { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
