using FluentValidation;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Application.Mods.Queries.GetModByName
{
    public class GetModByNameQueryValidator : AbstractValidator<GetModByNameQuery>
    {
        public GetModByNameQueryValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("You must provide a mod name to search for.")
                .MaximumLength(Mod.NameLength).WithMessage($"The mod's name must not exceed {Mod.NameLength} characters.");
        }
    }
}
