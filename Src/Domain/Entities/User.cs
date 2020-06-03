using System;
using System.Collections.Generic;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Validators;

// TODO: Determine whether or not this entity should be inherenting from a more formal User class that can track things like authentication.
// TODO: Add a preferred language property so use know which localization of stuff we should display for the user.
namespace FactorioProductionCells.Domain.Entities
{
    public class User : AuditableEntity
    {
        private User() {}
        
        public User (String UserName)
        {
            StringValidator.ValidateRequiredStringWithMaxLength(UserName, nameof(UserName), User.UserNameLength);

            this.UserName = UserName;
        }

        public const Int32 UserNameLength = 100;

        public Guid Id { get; private set; }
        public String UserName { get; private set; }
    }
}
