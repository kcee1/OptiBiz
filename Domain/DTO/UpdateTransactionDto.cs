namespace Domain.DTO
{
    public class UpdateTransactionDto
    {
        public string Status { get; set; } // Pending, Approved, Rejected
        public int? ApproverId { get; set; }
    }
}
