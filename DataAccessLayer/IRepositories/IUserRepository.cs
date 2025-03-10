﻿using Domain.Models;

namespace DataAccessLayer.IRepositories
{
    public interface IUserRepository
    {
        List<User> users();
        Task<User?> user(string id);
        Task<User?> createUser(User user);
        Task<User?> UpdateUser(string id);
        Task<bool> deleteUser(User user);
        Task<bool> AddUserToRole(User user, string roleName);

        Task<bool> RemoveUserFromRole(User user, string roleName);


    }
}
