using System;
using System.Collections.Generic;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Validators;
using FactorioProductionCells.Domain.ValueObjects;

namespace FactorioProductionCells.Domain.Entities
{
    public class Release : AuditableEntity
    {
        private Release() {}

        public Release(Guid ModId, DateTime ReleasedAt, String DownloadUrl, String FileName, String Sha1, ModVersion Version, FactorioVersion FactorioVersion, IList<Dependency> Dependencies)
        {
            ObjectValidator.ValidateRequiredObject(ModId, nameof(ModId));
            DateTimeValidator.ValidateRequiredDateTimeBeforePresent(ReleasedAt, nameof(ReleasedAt));
            StringValidator.ValidateRequiredStringWithMaxLength(DownloadUrl, nameof(DownloadUrl), Release.DownloadUrlLength);
            StringValidator.ValidateRequiredStringWithMaxLength(FileName, nameof(FileName), Release.FileNameLength);
            StringValidator.ValidateRequiredStringWithMaxLength(Sha1, nameof(Sha1), Release.Sha1Length);
            ObjectValidator.ValidateRequiredObject(Version, nameof(Version));
            ObjectValidator.ValidateRequiredObject(FactorioVersion, nameof(FactorioVersion));
            ListValidator.ValidateRequiredListNotEmpty<Dependency>(Dependencies, nameof(Dependencies));

            this.ModId = ModId;
            this.ReleasedAt = ReleasedAt;
            this.DownloadUrl = DownloadUrl;
            this.FileName = FileName;
            this.Sha1 = Sha1;
            this.Version = Version;
            this.FactorioVersion = FactorioVersion;
            this.Dependencies = Dependencies;
        }

        public Release(Mod Mod, DateTime ReleasedAt, String DownloadUrl, String FileName, String Sha1, ModVersion Version, FactorioVersion FactorioVersion, IList<Dependency> Dependencies)
            : this(Mod.Id, ReleasedAt, DownloadUrl, FileName, Sha1, Version, FactorioVersion, Dependencies) {}

        public const Int32 DownloadUrlLength = 400;
        public const Int32 FileNameLength = 200;
        public const Int32 Sha1Length = 50;
        
        public Guid Id { get; private set; }
        public Guid ModId { get; private set; }
        public DateTime ReleasedAt { get; private set; }
        public String DownloadUrl { get; private set; }
        public String FileName { get; private set; }
        public String Sha1 { get; private set; }
        public ModVersion Version { get; private set; }
        public FactorioVersion FactorioVersion { get; private set; }
        public IList<Dependency> Dependencies { get; private set; } = new List<Dependency>();

        // Navigation properties
        public Mod Mod { get; private set; }
    }
}
