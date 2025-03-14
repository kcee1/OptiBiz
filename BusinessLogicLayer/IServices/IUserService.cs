﻿using Domain.DTO;

namespace BusinessLogicLayer.IServices
{
    public interface IUserService
    {
        List<GetUserDto> Users();

        Task<GetUserDto> User(string id);

        Task<(GetUserDto?, string message)> CreateUser(CreateUserDto createUserDto);
       
        Task<(bool, string message)> UpdateUser(string id);
       
        Task<bool> AssignUserToRole(string id, string roleName);
       
        Task<bool> RemoveUserFromRole(string id, string roleName);
        Task<IList<GetUserDto>> GetTenantUser(int tenantId);

        Task<IList<GetTenantDto>> GetAllTenants();
        Task<GetUserDto> GetUserbyEmail(string email);




    }
}
