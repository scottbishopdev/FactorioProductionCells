using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Application.Common.Models;

namespace FactorioProductionCells.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;

        public IdentityService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<String> GetUserNameFromIdAsync(Guid userId)
        {
            
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return null;
            }
            else
            {
                return user.UserName;
            }
        }

        public async Task<Guid?> GetIdFromUserNameAsync(String userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            return user != null ? user.Id : (Guid?)null;
        }

        public async Task<Object> GetUserFromUserNameAsync(String userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            return user;
        }

        public async Task<Object> GetUserFromIdAsync(Guid id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<(Result Result, Object User)> CreateUserAsync(String userName, String emailAddress, String password, Language preferredLanguage)
        {
            var newUser = new User
            {
                UserName = userName,
                Email = emailAddress,
                PreferredLanguageId = preferredLanguage.Id
            };

            var result = await _userManager.CreateAsync(newUser, password);
            return (result.ToApplicationResult(), newUser);
        }

        public async Task<(Result Result, Object User)> CreateUserAsync(Guid id, String userName, String emailAddress, String password, Language preferredLanguage)
        {
            var newUser = new User
            {
                Id = id,
                UserName = userName,
                Email = emailAddress,
                PreferredLanguageId = preferredLanguage.Id
            };

            var result = await _userManager.CreateAsync(newUser, password);
            return (result.ToApplicationResult(), newUser);
        }

        public async Task<Result> DeleteUserByIdAsync(Guid userId)
        {
            var userToDelete = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            if (userToDelete != null)
            {
                return await DeleteUserAsync(userToDelete);
            }

            return Result.Success();
        }

        public async Task<Result> DeleteUserAsync(User userToDelete)
        {
            var result = await _userManager.DeleteAsync(userToDelete);
            return result.ToApplicationResult();
        }
    }
}
