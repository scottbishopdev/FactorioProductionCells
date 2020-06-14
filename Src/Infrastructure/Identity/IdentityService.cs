using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Application.Common.Models;

namespace FactorioProductionCells.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        // TODO: I'd like to understand more about the UserManager here. For example, where is it storing it's data? I feel like I ought to be defining some kind of persistence for user information.
        private readonly UserManager<NetCoreUser> _userManager;

        public IdentityService(UserManager<NetCoreUser> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<String> GetUserNameFromIdAsync(Guid userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
            return user.UserName;
        }

        public async Task<Guid> GetIdFromUserNameAsync(String userName)
        {
            var user = await _userManager.Users.FirstAsync(u => u.UserName == userName);
            return user.Id;
        }

        public async Task<(Result Result, Guid UserId)> CreateUserAsync(String userName, String emailAddress, String password)
        {
            var newUser = new NetCoreUser
            {
                UserName = userName,
                Email = emailAddress
            };

            var result = await _userManager.CreateAsync(newUser, password);
            return (result.ToApplicationResult(), newUser.Id);
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

        public async Task<Result> DeleteUserAsync(NetCoreUser userToDelete)
        {
            var result = await _userManager.DeleteAsync(userToDelete);
            return result.ToApplicationResult();
        }
    }
}
