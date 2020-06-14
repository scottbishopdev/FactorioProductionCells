using System;
using System.Threading.Tasks;
using FactorioProductionCells.Application.Common.Models;

namespace FactorioProductionCells.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<String> GetUserNameFromIdAsync(Guid userId);

        Task<Guid> GetUserIdFromNameAsync(String userName);

        // TODO: This definitely doesnt seem right. In what world would we expect a password to be sent as a string? Should this be a hash or something?
        //Task<(Result Result, Guid UserId)> CreateUserAsync(String userName, String password);

        //Task<Result> DeleteUserAsync(Guid userId);

        // TODO: Should this also be able to perform some sort of authentication?
    }
}
