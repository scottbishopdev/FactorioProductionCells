using System;
using System.Collections.Generic;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Validators;
using FactorioProductionCells.Domain.ValueObjects;

namespace FactorioProductionCells.Domain.Entities
{
    public class Release : AuditableEntity, IComparable
    {
        public const Int32 Sha1Length = 50;
        
        private Release() {}

        public Release(Mod Mod, DateTime ReleasedAt, String Sha1, ReleaseDownloadUrl DownloadUrl, ReleaseFileName ReleaseFileName, ModVersion Version, FactorioVersion FactorioVersion, List<Dependency> Dependencies)
        {
            ObjectValidator.ValidateRequiredObject(Mod, nameof(Mod));
            DateTimeValidator.ValidateRequiredDateTimeBeforePresent(ReleasedAt, nameof(ReleasedAt));
            StringValidator.ValidateRequiredStringWithMaxLength(Sha1, nameof(Sha1), Release.Sha1Length);
            ObjectValidator.ValidateRequiredObject(DownloadUrl, nameof(DownloadUrl));
            ObjectValidator.ValidateRequiredObject(ReleaseFileName, nameof(ReleaseFileName));
            ObjectValidator.ValidateRequiredObject(Version, nameof(Version));
            ObjectValidator.ValidateRequiredObject(FactorioVersion, nameof(FactorioVersion));
            ListValidator.ValidateRequiredList<Dependency>(Dependencies, nameof(Dependencies));

            if (Version != ReleaseFileName.Version) throw new ArgumentException("The specified release file name version down not match the specified release version.", "ReleaseFileName");

            this.Mod = Mod;
            this.ReleasedAt = ReleasedAt;
            this.DownloadUrl = DownloadUrl;
            this.ReleaseFileName = ReleaseFileName;
            this.Sha1 = Sha1;
            this.Version = Version;
            this.FactorioVersion = FactorioVersion;
            this.Dependencies = Dependencies;
        }

        public Guid Id { get; private set; }
        public Guid ModId { get; private set; }
        public DateTime ReleasedAt { get; private set; }
        public String Sha1 { get; private set; }
        public ModVersion Version { get; private set; }
        public FactorioVersion FactorioVersion { get; private set; }
        public ReleaseDownloadUrl DownloadUrl { get; private set; }
        public ReleaseFileName ReleaseFileName { get; private set; }
        // TODO: Similar to the lists of objects on Mod, we need to make this public so entity framework can query with it properly.
        public List<Dependency> Dependencies { get; private set; } = new List<Dependency>();

        // Navigation properties
        public Mod Mod { get; private set; }

        public int CompareTo(Object obj)
        {
            if(obj.GetType() != this.GetType()) throw new ArgumentException("Unable to compare the specified object to a Release.", "obj");
            Release right = (Release)obj;

            if(this.ReleasedAt < right.ReleasedAt) return -1;
            else if(this.ReleasedAt > right.ReleasedAt) return 1;
            else return 0;
        }

        public List<Dependency> GetDependencies()
        {
            return this.Dependencies;
        }
    }
}
