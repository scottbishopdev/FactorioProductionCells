/*
using FluentValidation;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Application.Users.Queries.GetUserByUserName
{
    public class GetUserByUserNameQueryValidator : AbstractValidator<GetUserByUserNameQuery>
    {
        public GetUserByUserNameQueryValidator()
        {
            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage("You must provide a user name to search for.")
                .MaximumLength(User.UserNameLength).WithMessage($"The user name must not exceed {User.UserNameLength} characters.");
        }
    }
}
*/