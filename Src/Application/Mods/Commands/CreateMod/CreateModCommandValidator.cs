/*
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Domain.Entities;
using ValidationException = FactorioProductionCells.Application.Common.Exceptions.ValidationException;

namespace FactorioProductionCells.Application.Mods.Commands
{
    public class CreateModCommandValidator : AbstractValidator<CreateModCommand>
    {
        private readonly IFactorioProductionCellsDbContext _dbContext;

        public CreateModCommandValidator(IFactorioProductionCellsDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("A name for this mod is required.")
                .MaximumLength(Mod.NameLength).WithMessage($"The mod's name must not exceed {Mod.NameLength} characters.")
                .MustAsync(BeUniqueName).WithMessage("A mod already exists with the specificed name.");

            RuleFor(v => v.ModTitles)
                .NotEmpty().WithMessage("A mod must have at least one title.");

            // TODO: Determine what sort of validation needs to be performed on each individual mod title
            //RuleForEach(v => v.ModTitles)
            //    .NotEmpty().WithMessage("A mod must have at least one title.");

            // TODO: Determine what sort of validation needs to be performed on the collection of releases.
            //RuleFor(v => v.Releases)

            // TODO: Determine what sort of validation needs to be performed on each individual release.
            //RuleForEach(v => v.Releases)
        }

        public async Task<Boolean> BeUniqueName(String name, CancellationToken cancellationToken)
        {
            return await _dbContext.Mods.AllAsync(m => m.Name != name);
        }
    }
}
*/