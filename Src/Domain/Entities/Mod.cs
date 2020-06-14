using System;
using System.Collections.Generic;
using System.Linq;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Validators;
using FactorioProductionCells.Domain.ValueObjects;

namespace FactorioProductionCells.Domain.Entities
{
    public class Mod : AuditableEntity
    {
        public const Int32 NameLength = 200;
        
        public const String BaseModName = "base";

        private Mod() {}

        public Mod(String Name, List<ModTitle> Titles, List<Release> Releases)
        {
            StringValidator.ValidateRequiredStringWithMaxLength(Name, nameof(Name), Mod.NameLength);
            //ListValidator.ValidateRequiredListNotEmpty<ModTitle>(Titles, nameof(Titles));
            ListValidator.ValidateRequiredList<ModTitle>(Titles, nameof(Titles));
            //ListValidator.ValidateRequiredListNotEmpty<Release>(Releases, nameof(Releases));
            ListValidator.ValidateRequiredList<Release>(Releases, nameof(Releases));
            
            this.Name = Name;
            this.Releases = Releases;
            this.Titles = Titles;
        }

        public Guid Id { get; private set; }
        public String Name { get; private set; }
        public Boolean IsBaseMod => false;
        // TODO: The Application layer needs direct access to the collection properties in order to properly query things. Is there a way to protect the integrity of the lists AND give people the necessary access?
        public List<ModTitle> Titles { get; private set; } = new List<ModTitle>();
        public List<Release> Releases { get; private set; } = new List<Release>();

        public void TryAddRelease(DateTime releasedAt, String sha1, ReleaseDownloadUrl downloadUrl, ReleaseFileName releaseFileName, ModVersion version, FactorioVersion factorioVersion, List<Dependency> dependencies)
        {
            if(this.Releases.Exists(r => r.ModId == this.Id && r.Version == version)) throw new InvalidOperationException("A release with the specified version already exists for this mod.");
            if(this.Name != downloadUrl.ModName) throw new InvalidOperationException("The specified download URL does not properly reference this mod.");
            if(this.Name != releaseFileName.ModName) throw new InvalidOperationException("The specified release file name does not properly reference this mod.");

            this.Releases.Add(new Release
            (
                Mod: this,
                ReleasedAt: releasedAt,
                Sha1: sha1,
                DownloadUrl: downloadUrl,
                ReleaseFileName: releaseFileName,
                Version: version,
                FactorioVersion: factorioVersion,
                Dependencies: dependencies
            ));
        }

        public Boolean TryRemoveRelease(Release releaseToRemove)
        {
            //if(this.Releases.Count <= 1) throw new InvalidOperationException("A mod must always have at least one release. You may not remove the last release from a mod's list of releases.");
            return this.Releases.Remove(releaseToRemove);
        }

        public List<Release> GetReleases()
        {
            return this.Releases;
        }

        public Release GetLatestRelease()
        {
            return this.Releases.Max();
        }

        public void TryAddTitle(Guid languageId, String title)
        {
            if(this.Titles.Exists(t => t.ModId == this.Id && t.LanguageId == languageId)) throw new InvalidOperationException("A title with the specified language already exists for this mod.");
            
            this.Titles.Add(new ModTitle
            (
                Mod: this,
                LanguageId: languageId,
                Title: title
            ));
        }

        public Boolean TryRemoveTitle(ModTitle titleToRemove)
        {
            if(this.Titles.Count <= 1) throw new InvalidOperationException("A mod must always have at least one title. You may not remove the last title from a mod's list of titles.");
            return this.Titles.Remove(titleToRemove);
        }

        public List<ModTitle> GetTitles()
        {
            return this.Titles;
        }
    }
}
