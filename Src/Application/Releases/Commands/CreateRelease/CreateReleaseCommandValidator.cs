/*
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.ValueObjects;

namespace FactorioProductionCells.Application.Releases.Commands
{
    public class CreateReleaseCommandValidator : AbstractValidator<CreateReleaseCommand>
    {
        private readonly IFactorioProductionCellsDbContext _dbContext;

        public CreateReleaseCommandValidator(IFactorioProductionCellsDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(r => r.ModId)
                .NotEmpty().WithMessage("A mod id for this release is required.")
                .MustAsync(BeValidMod).WithMessage("A mod corresponding to the specified mod id could not be found.");

            // TODO: Since we've replaced the string with a value object (ModVersion), all this validation needs to be revisited.
            RuleFor(r => r.Version)
                .NotEmpty().WithMessage("A version for this release is required.")
                .Matches(ModVersion.ModVersionStringRegex).WithMessage("A release's version must be in the format \"<major>.<minor>.<patch>\".")
                .MaximumLength(Releases.VersionStringLength).WithMessage($"A release's version must not exceed {Releases.VersionStringLength} characters.")
                .MustAsync(BeUniqueVersionString).WithMessage("A release already exists with the specified version.");

            RuleFor(r => r.ReleasedAt)
                .NotEmpty().WithMessage("A release date for this release is required.")
                .LessThan(DateTime.Now).WithMessage("A release's release date must be in the past.");

            RuleFor(r => r.DownloadUrl)
                .NotEmpty().WithMessage("A download url for this release is required.")
                .MaximumLength(Release.DownloadUrlLength).WithMessage($"A release's download URL must not exceed {Release.DownloadUrlLength} characters.")
                .Matches(@"^\/download\/[\S]+\/[0-9A-Fa-f]{24}$").WithMessage("A release's download URL must be in the format \"/download/<mod name>/*.\"");
                
            RuleFor(r => r)
                .MustAsync(BeValidDownloadUrlForMod).WithMessage("A release's download URL must reference the mod represented by the specified mod id.");
                
            RuleFor(r => r.FileName)
                .NotEmpty().WithMessage("A file name for this release is required.")
                .MaximumLength(Release.FileNameLength).WithMessage($"A release's file name must not exceed {Release.FileNameLength} characters.")
                .Matches(@"^[\S]+_[0-9]+\.[0-9]+\.[0-9]+\.zip$").WithMessage("A release's file name must be in the format \"<mod name>_<release version>.zip\"");
            RuleFor(r => r)
                .MustAsync(BeValidFilenameForMod).WithMessage("A release's file name must reference the mod represented by the specified mod id.")
                .Must(BeValidFilenameForVersion).WithMessage("A release's file name must reference the specified version.");

            RuleFor(r => r.Sha1)
                .NotEmpty().WithMessage("A SHA1 checksum for this release is required.")
                .MaximumLength(Release.Sha1Length).WithMessage($"A release's SHA1 checksum must not exceed {Release.Sha1Length} characters.")
                .Matches("^[0-9a-fA-F]{40}$").WithMessage("A release's SHA1 checksum must be in the format of a string of 40 hexadecimal characters.");

            // TODO: Since we've replaced the string with a value object (FactorioVersion), all this validation needs to be revisited.
            RuleFor(r => r.FactorioVersion)
                .NotEmpty().WithMessage("A Factorio version for this release is required.")
                .Matches(FactorioVersion.FactorioVersionStringRegex).WithMessage("A release's Factorio version must be in the format \"<major>.<minor>\".")
                .MaximumLength(Release.FactorioVersionStringLength).WithMessage($"A release's Factorio version must not exceed {Release.FactorioVersionStringLength} characters.");

            RuleFor(r => r.Dependencies)
                .NotEmpty().WithMessage("A release must have at least one dependency, as all mods are dependent on the base mod.");

            // TODO: Determine what sort of validation needs to be performed on each individual dependency.
            //RuleForEach(r => r.Dependencies)
            //    .SetValidator();
        }

        public async Task<Boolean> BeValidMod(Guid modId, CancellationToken cancellationToken)
        {
            return await _dbContext.Exists<Mod>(m => m.Id = modId);
        }

        public async Task<Boolean> BeUniqueVersionString(String versionString, CancellationToken cancellationToken)
        {
            return await _dbContext.Releases.AllAsync(m => m.versionString != versionString);
        }

        // TODO: Test the ever-loving crap our of all these regex validations
        public async Task<Boolean> BeValidDownloadUrlForMod(Release release, CancellationToken cancellationToken)
        {
            var dbMod = await _dbContext.Mods.SingleOrDefault(m => m.Id == release.modId);
            var downloadUrlRegex = new Regex($"^\\/download\\/{dbMod.Name}\\/.[0-9A-Fa-f]{23}$");
            
            return downloadUrlRegex.IsMatch(release.DownloadUrl);
        }

        public async Task<Boolean> BeValidFilenameForMod(Release release, CancellationToken cancellationToken)
        {
            var dbMod = await _dbContext.Mods.SingleOrDefault(m => m.Id == release.modId);
            var fileNameForModRegex = new Regex($"^{dbMod.Name}_[0-9]+\\.[0-9]+\\.[0-9]+\\.zip$");
            
            return fileNameForModRegex.IsMatch(release.FileName);
        }

        public Boolean BeValidFilenameForVersion(Release release, CancellationToken cancellationToken)
        {
            var fileNameforVersionRegex = new Regex($"^[\\S]{+}_{release.VersionString}.zip$");
            
            return fileNameforVersionRegex.IsMatch(release.FileName);
        }
    }
}
*/