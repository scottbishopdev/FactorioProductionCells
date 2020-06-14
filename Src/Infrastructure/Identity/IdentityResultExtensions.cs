using System.Linq;
using Microsoft.AspNetCore.Identity;
using FactorioProductionCells.Application.Common.Models;

namespace FactorioProductionCells.Infrastructure.Identity
{
    // TODO: This seems a tad superfluous. Maybe I should do this manually until I understand why it's implemented this way.
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }
    }
}
