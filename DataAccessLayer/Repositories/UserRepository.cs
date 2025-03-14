﻿using DataAccessLayer.IRepositories;
using Domain.Models;
using Microsoft.AspNetCore.Identity;


namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        public List<User> users()
        {
            return _userManager.Users.ToList();
        }

        public async Task<User?> user(string id)
        {
           return await _userManager.FindByIdAsync(id);
        }

        public async Task<User?> getUserbyUserName(string userName)
        {
           return await _userManager.FindByNameAsync(userName);
        }


        public async Task<User?> createUser(User user)
        {
            IdentityResult result = await _userManager
                .CreateAsync(user, user.PasswordHash);

            if(!result.Succeeded)
            {
                return null;
            }

            return await _userManager.FindByIdAsync(user.Id);
        }

         public async Task<User?> UpdateUser(string id)
        {
            User? theUser = await _userManager.FindByIdAsync(id);

            if(theUser == null)
            {
                return null;
            }

            theUser.IsEmailVerified = true;


            IdentityResult result = await _userManager
                .UpdateAsync(theUser);

            if(!result.Succeeded)
            {
                return null;
            }

            return theUser;
        }

        public async Task<(bool, string message)> DebitUser(string userId, decimal amount)
        {
            User? theUser = await _userManager.FindByIdAsync(userId);

            if (theUser == null)
            {
                return (false, "Invalid user");
            }

            if(theUser.AccountBalance < amount)
            {
                return (false, "Insufficient Balance");
            }

            theUser.AccountBalance -= amount;

            IdentityResult result = await _userManager
                .UpdateAsync(theUser);

            if (!result.Succeeded)
            {
                return (true, "Debited Successfully");
            }

            return (false, "Could not process this transaction");
        }


        public async Task<bool> deleteUser(User user)
        {
           IdentityResult result = await _userManager
                .DeleteAsync(user);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        
        }

        public async Task<bool> AddUserToRole(User user, string roleName)
        {
           IdentityResult result = await _userManager.AddToRoleAsync(user, roleName);

            if(result.Succeeded)
            {
                return true;
            }

            return false;

        }

        public async Task<bool> RemoveUserFromRole(User user, string roleName)
        {
           IdentityResult result = await _userManager.RemoveFromRoleAsync(user, roleName);

            if(result.Succeeded)
            {
                return true;
            }

            return false;

        }


    }
}
