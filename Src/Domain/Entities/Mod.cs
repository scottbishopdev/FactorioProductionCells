using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Validators;

namespace FactorioProductionCells.Domain.Entities
{
    public class Mod : AuditableEntity, IEquatable<Mod>
    {
        public const String ValidModNameCharacters = "-ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_ ";
        public const Int32 NameLength = 200;
        public static String ModNameCapturePattern = @"^([" + Regex.Escape(ValidModNameCharacters) + @"]{1," + Mod.NameLength.ToString() + @"})$";

        private Mod() {}

        public Mod(Mod original)
        {
            ObjectValidator.ValidateRequiredObject(original, nameof(original));

            this.Id = original.Id;
            this.Name = original.Name;
            this.Titles = original.Titles != null ? original.Titles.ConvertAll(title => new ModTitle(title)) : null;
            this.Releases = original.Releases != null ? original.Releases.ConvertAll(release => new Release(release)) : null;
        }

        public Mod(String Name, List<ModTitle> Titles, List<Release> Releases)
        {
            StringValidator.ValidateRequiredStringWithMaxLength(Name, nameof(Name), Mod.NameLength);
            ListValidator.ValidateRequiredListNotEmpty<ModTitle>(Titles, nameof(Titles));
            ListValidator.ValidateRequiredListNotEmpty<Release>(Releases, nameof(Releases));
            
            Regex modNameCaptureRegex = new Regex(Mod.ModNameCapturePattern);
            Match match = modNameCaptureRegex.Match(Name);            
            if(!match.Success) throw new ArgumentException($"Unable to parse \"{Name}\" to a valid mod name due to formatting.", "Name");

            // TODO: Should we validate that the modName matches the various modName properties (e.g. ReleaseFileName.ModName, ReleaseDownloadURL.ModName, etc.)
            foreach (Release release in Releases)
            {
                if (release.ReleaseFileName.ModName != Name) throw new ArgumentException($"The mod name in the release filename \"{release.ReleaseFileName}\" does not match the specified mod name \"{Name}\".", nameof(Releases));
                if (release.ReleaseDownloadUrl.ModName != Name.Replace(" ", "%20")) throw new ArgumentException($"The mod name in the release download URL \"{release.ReleaseDownloadUrl}\" does not match the specified mod name \"{Name}\".", nameof(Releases));
            }

            this.Name = match.Groups[1].Value;
            this.Releases = Releases.ConvertAll(release => new Release(release));
            this.Titles = Titles.ConvertAll(title => new ModTitle(title));
        }

        public Guid Id { get; private set; }
        public String Name { get; private set; }

        // TODO: The Application layer needs direct access to the collection properties in order to properly query things. Is there a way to protect the integrity of the lists AND give people the necessary access?
        public List<ModTitle> Titles { get; private set; } = new List<ModTitle>();
        public List<Release> Releases { get; private set; } = new List<Release>();

        public void TryAddRelease(Release newRelease)
        {
            ObjectValidator.ValidateRequiredObject(newRelease, nameof(newRelease));
            
            if(this.Releases.Exists(r => r.ModVersion == newRelease.ModVersion)) throw new InvalidOperationException("A release with the specified version already exists for this mod.");
            if(this.Name.Replace(" ", "%20") != newRelease.ReleaseDownloadUrl.ModName) throw new InvalidOperationException("The specified download URL does not properly reference this mod.");
            if(this.Name != newRelease.ReleaseFileName.ModName) throw new InvalidOperationException("The specified release file name does not properly reference this mod.");
            
            this.Releases.Add(new Release(newRelease));
        }

        public Boolean TryRemoveRelease(Release releaseToRemove)
        {
            if(this.Releases.Count <= 1) throw new InvalidOperationException("A mod must always have at least one release. You may not remove the last release from a mod's list of releases.");
            return this.Releases.Remove(releaseToRemove);
        }

        public Release GetLatestRelease()
        {
            return this.Releases.Max();
        }

        public void TryAddTitle(ModTitle newModTitle)
        {
            ObjectValidator.ValidateRequiredObject(newModTitle, nameof(newModTitle));
            
            if(this.Titles.Exists(t => t.Language.Id == newModTitle.Language.Id && t.Language.Id == newModTitle.LanguageId)) throw new InvalidOperationException("A title with the specified language already exists for this mod.");
            
            this.Titles.Add(new ModTitle(newModTitle));
        }

        public Boolean TryRemoveTitle(ModTitle titleToRemove)
        {
            if(this.Titles.Count <= 1) throw new InvalidOperationException("A mod must always have at least one title. You may not remove the last title from a mod's list of titles.");
            return this.Titles.Remove(titleToRemove);
        }

        public Boolean Equals(Mod right)
        {
            return right != null
                && ((this.Id == null && right.Id == null) || this.Id == right.Id)
                && ((this.Name == null && right.Name == null) || this.Name == right.Name)
                && ((this.Titles == null && right.Titles == null) || Enumerable.SequenceEqual(this.Titles, right.Titles))
                && ((this.Releases == null && right.Releases == null) || Enumerable.SequenceEqual(this.Releases, right.Releases));
        }
    }
}
