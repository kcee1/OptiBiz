#nullable disable
namespace Domain.DTO
{
    public class GetTransactionBeneficiaryDto
    {
        public int Id { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
