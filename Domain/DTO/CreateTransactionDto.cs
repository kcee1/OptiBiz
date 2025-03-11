namespace Domain.DTO
{
    public class CreateTransactionDto
    {
        public int InitiatorId { get; set; }
        public string Type { get; set; } // SingleTransfer, BulkTransfer, BillPayment
        public decimal Amount { get; set; }
        public int TenantId { get; set; }
        public List<CreateTransactionBeneficiaryDto> Beneficiaries { get; set; }
    }
}
