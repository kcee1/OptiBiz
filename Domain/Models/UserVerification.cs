#nullable disable
namespace Domain.Models
{
    public class UserVerification
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string OTP { get; set; }
        public DateTime ExpiryTime { get; set; }
        public bool IsUsed { get; set; }

        public User User { get; set; }
    }
}
