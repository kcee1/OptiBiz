namespace Domain.DTO
{
    public class GetTransactionDto
    {
        public int Id { get; set; }
        public string ReferenceNumber { get; set; }
        public string InitiatorId { get; set; }
        public string? ApproverId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public List<GetTransactionBeneficiaryDto> Beneficiaries { get; set; }
        public int TenantId { get; set; }
    }
}
