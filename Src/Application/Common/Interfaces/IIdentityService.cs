using System;
using System.Threading.Tasks;
using FactorioProductionCells.Application.Common.Models;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<String> GetUserNameFromIdAsync(Guid userId);
        Task<Guid?> GetIdFromUserNameAsync(String userName);
        Task<(Result Result, Object User)> CreateUserAsync(String userName, String emailAddress, String password, Language PreferredLanguage);
        Task<(Result Result, Object User)> CreateUserAsync(Guid id, String userName, String emailAddress, String password, Language PreferredLanguage);
        Task<Object> GetUserFromUserNameAsync(String userName);
        Task<Object> GetUserFromIdAsync(Guid id);
        Task<Result> DeleteUserByIdAsync(Guid userId);
    }
}
