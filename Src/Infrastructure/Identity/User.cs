using System;
using Microsoft.AspNetCore.Identity;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Infrastructure.Identity
{
    // TODO: Since this is basically and entity, I really wish it could be up in the Domain layer, but I don't want the Domain to be dependent on AspnetCore.Identity. I'd
    // create a class up there and inherit from it here, but since IdentityUser<T> is a class and we can't have multiple class inheritance, that's not an option.
    public class User : IdentityUser<Guid>
    {
        // TODO: Figure out how we can populate this from the DefaultLanguageService without creating a circular dependency.
        public Guid PreferredLanguageId { get; set; }

        // Navigation Properties
        public Language PreferredLanguage { get; set; }
    }
}
