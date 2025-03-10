#nullable disable
namespace Domain.DTO
{
    public class UpdateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; } // Admin, Initiator, Approver, Viewer
    
    }
}
