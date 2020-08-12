using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Validators;
using FactorioProductionCells.Domain.ValueObjects;

namespace FactorioProductionCells.Domain.Entities
{
    public class Release : AuditableEntity, IComparable, IEquatable<Release>
    {
        public const String ValidSha1Characters = "abcdef0123456789";
        public const Int32 Sha1Length = 40;
        public static String Sha1CapturePattern = @"^([" + Regex.Escape(ValidSha1Characters) + @"]+)$";
        
        private Release() {}

        public Release(Release original)
        {
            ObjectValidator.ValidateRequiredObject(original, nameof(original));
            
            if (original.Id != null) this.Id = original.Id;
            if (original.ModId != null) this.ModId = original.ModId;
            this.Mod = original.Mod != null ?  new Mod(original.Mod) : null;
            if (original.ReleasedAt != null) this.ReleasedAt = original.ReleasedAt;
            this.ReleaseDownloadUrl = original.ReleaseDownloadUrl != null ? new ReleaseDownloadUrl(original.ReleaseDownloadUrl) : null;
            this.ReleaseFileName = original.ReleaseFileName != null ? new ReleaseFileName(original.ReleaseFileName) : null;
            if (original.Sha1 != null) this.Sha1 = original.Sha1;
            this.ModVersion = original.ModVersion != null ? new ModVersion(original.ModVersion) : null;
            this.FactorioVersion = original.FactorioVersion != null ? new FactorioVersion(original.FactorioVersion) : null;
            this.Dependencies = original.Dependencies != null ? original.Dependencies.ConvertAll(dependency => new Dependency(dependency)) : null;
        }

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
            Match match = sha1CaptureRegex.Match(Sha1String);
            if(!match.Success) throw new ArgumentException($"Unable to parse \"{Sha1String}\" to a valid SHA-1 hash due to formatting.", "Sha1String");

            String sha1 = match.Groups[1].Value;

            if (sha1.Length != Sha1Length) throw new ArgumentException($"Unable to parse \"{Sha1String}\" to a valid SHA-1 hash due to length. The SHA-1 hash must have a length of exactly {Release.Sha1Length} characters.", "Sha1String");
            if (ModVersion != ReleaseFileName.ModVersion) throw new ArgumentException("The specified release file name version does not match the specified release version.", "ReleaseFileName");

            this.ReleasedAt = ReleasedAt;
            this.ReleaseDownloadUrl = new ReleaseDownloadUrl(ReleaseDownloadUrl);
            this.ReleaseFileName = new ReleaseFileName(ReleaseFileName);
            this.Sha1 = sha1;
            this.ModVersion = new ModVersion(ModVersion);
            this.FactorioVersion = new FactorioVersion(FactorioVersion);
            // TODO: We may want to write an .AddDependency() method that validates that the dependency we're adding isn't for the release/mod itself. That would generate a circular dependency.
            this.Dependencies = Dependencies.ConvertAll(dependency => new Dependency(dependency));
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

        public Boolean Equals(Release right)
        {
            return right != null
                && ((this.Id == null && right.Id == null) || this.Id == right.Id)
                && ((this.ModId == null && right.ModId == null) || this.ModId == right.ModId)
                && ((this.ReleasedAt == null && right.ReleasedAt == null) || this.ReleasedAt == right.ReleasedAt)
                && ((this.Sha1 == null && right.Sha1 == null) || this.Sha1 == right.Sha1)
                && ((this.ModVersion == null && right.ModVersion == null) || this.ModVersion == right.ModVersion)
                && ((this.FactorioVersion == null && right.FactorioVersion == null) || this.FactorioVersion == right.FactorioVersion)
                && ((this.ReleaseDownloadUrl == null && right.ReleaseDownloadUrl == null) || this.ReleaseDownloadUrl.Equals(right.ReleaseDownloadUrl))
                && ((this.ReleaseFileName == null && right.ReleaseFileName == null) || this.ReleaseFileName.Equals(right.ReleaseFileName))
                && ((this.Dependencies == null && right.Dependencies == null) || Enumerable.SequenceEqual(this.Dependencies, right.Dependencies))
                && ((this.Mod == null && right.Mod == null) || this.Mod.Equals(right.Mod));
        }
    }
}
