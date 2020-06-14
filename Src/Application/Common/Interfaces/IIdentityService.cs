using System;
using System.Threading.Tasks;
using FactorioProductionCells.Application.Common.Models;

namespace FactorioProductionCells.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<String> GetUserNameFromIdAsync(Guid userId);

        Task<Guid> GetIdFromUserNameAsync(String userName);

        Task<(Result Result, Guid UserId)> CreateUserAsync(String userName, String emailAddress, String password);

        Task<Result> DeleteUserByIdAsync(Guid userId);
    }
}
