using System;
using System.Collections.Generic;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Validators;

// TODO: Determine whether or not this entity should be inherenting from a more formal User class that can track things like authentication. See Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser
// TODO: Add a preferred language property so use know which localization of stuff we should display for the user.
namespace FactorioProductionCells.Domain.Entities
{
    public class User
    {
        public const Int32 UserNameLength = 100;
        
        private User() {}
        
        public User (String UserName)
        {
            StringValidator.ValidateRequiredStringWithMaxLength(UserName, nameof(UserName), User.UserNameLength);

            this.Id = Guid.NewGuid();
            this.UserName = UserName;
        }

        public Guid Id { get; private set; }
        public String UserName { get; private set; }

        //public DateTime AddedDate { get; private set; }
        //public DateTime LastModified { get; private set; }

        // Navigation Properties
        
    }
}
