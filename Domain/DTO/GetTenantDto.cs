#nullable disable
namespace Domain.DTO
{
    public class GetTenantDto
    {
        public int Id { get; set; }
        public string BusinessNumber { get; set; }

        public string TaxIdentificationNumber { get; set; }

        public string BusinessType { get; set; }

        public string BankVerificationCode { get; set; }
    }
}
