#nullable disable
namespace Domain.DTO
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // Admin, Initiator, Approver, Viewer
        public string Password { get; set; }
        public TenantDTO TenantDTO { get; set; }


    }
}
