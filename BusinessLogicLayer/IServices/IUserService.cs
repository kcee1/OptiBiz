using Domain.DTO;

namespace BusinessLogicLayer.IServices
{
    public interface IUserService
    {
        IList<GetUserDto> Users();

        Task<GetUserDto> User(string id);

        Task<(CreateUserDto?, string message)> CreateUser(CreateUserDto createUserDto);
       
        Task<(bool, string message)> UpdateUser(string id);
       
        Task<bool> AssignUserToRole(string id, string roleName);
       
        Task<bool> RemoveUserFromRole(string id, string roleName);






    }
}
