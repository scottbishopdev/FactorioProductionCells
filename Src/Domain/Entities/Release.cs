using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Validators;
using FactorioProductionCells.Domain.ValueObjects;

namespace FactorioProductionCells.Domain.Entities
{
    public class Release : AuditableEntity, IComparable
    {
        public const String ValidSha1Characters = "abcdef0123456789";
        public const Int32 Sha1Length = 40;
        public static String Sha1CapturePattern = @"^([" + ValidSha1Characters + @"]{1,})$";
        
        private Release() {}

        public Release(DateTime ReleasedAt, String Sha1String, ReleaseDownloadUrl ReleaseDownloadUrl, ReleaseFileName ReleaseFileName, ModVersion ModVersion, FactorioVersion FactorioVersion, List<Dependency> Dependencies)
        {
            DateTimeValidator.ValidateRequiredDateTimeBeforePresent(ReleasedAt, nameof(ReleasedAt));
            ObjectValidator.ValidateRequiredObject(Sha1String, nameof(Sha1String));
            ObjectValidator.ValidateRequiredObject(ReleaseDownloadUrl, nameof(ReleaseDownloadUrl));
            ObjectValidator.ValidateRequiredObject(ReleaseFileName, nameof(ReleaseFileName));
            ObjectValidator.ValidateRequiredObject(ModVersion, nameof(ModVersion));
            ObjectValidator.ValidateRequiredObject(FactorioVersion, nameof(FactorioVersion));
            ListValidator.ValidateRequiredListNotEmpty(Dependencies, nameof(Dependencies));

            Regex sha1CaptureRegex = new Regex(Release.Sha1CapturePattern);
            Match match = sha1CaptureRegex.Match(Sha1String.Trim());
            if(!match.Success) throw new ArgumentException($"Unable to parse \"{Sha1String}\" to a valid SHA-1 hash due to formatting.", "Sha1String");

            String sha1 = match.Groups[1].Value;

            if (sha1.Length != Sha1Length) throw new ArgumentException($"Unable to parse \"{Sha1String}\" to a valid SHA-1 hash due to length. The SHA-1 hash must have a length of exactly {Release.Sha1Length} characters.", "Sha1String");
            if (ModVersion != ReleaseFileName.ModVersion) throw new ArgumentException("The specified release file name version down not match the specified release version.", "ReleaseFileName");

            this.ReleasedAt = ReleasedAt;
            this.ReleaseDownloadUrl = ReleaseDownloadUrl;
            this.ReleaseFileName = ReleaseFileName;
            this.Sha1 = sha1;
            this.ModVersion = ModVersion;
            this.FactorioVersion = FactorioVersion;
            this.Dependencies = new List<Dependency>(Dependencies);
        }

        public Guid Id { get; private set; }
        public Guid ModId { get; private set; }
        public DateTime ReleasedAt { get; private set; }
        public String Sha1 { get; private set; }
        public ModVersion ModVersion { get; private set; }
        public FactorioVersion FactorioVersion { get; private set; }
        public ReleaseDownloadUrl ReleaseDownloadUrl { get; private set; }
        public ReleaseFileName ReleaseFileName { get; private set; }
        // TODO: Similar to the lists of objects on Mod, we need to make this public so entity framework can query with it properly.
        public List<Dependency> Dependencies { get; private set; }

        // Navigation properties
        public Mod Mod { get; private set; }

        public int CompareTo(Object obj)
        {
            if(obj.GetType() != this.GetType()) throw new ArgumentException("The specified object to compare is not a Release.", "obj");
            Release right = (Release)obj;

            if(this.ReleasedAt < right.ReleasedAt) return -1;
            else if(this.ReleasedAt > right.ReleasedAt) return 1;
            else return 0;
        }
    }
}
