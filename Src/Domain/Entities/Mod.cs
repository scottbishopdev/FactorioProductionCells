using System;
using System.Collections.Generic;
using System.Linq;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Validators;

namespace FactorioProductionCells.Domain.Entities
{
    public class Mod : AuditableEntity
    {
        public const Int32 NameLength = 200;

        private Mod() {}

        public Mod(String Name, List<ModTitle> Titles, List<Release> Releases)
        {
            StringValidator.ValidateRequiredStringWithMaxLength(Name, nameof(Name), Mod.NameLength);
            ListValidator.ValidateRequiredListNotEmpty<ModTitle>(Titles, nameof(Titles));
            ListValidator.ValidateRequiredListNotEmpty<Release>(Releases, nameof(Releases));
            
            this.Name = Name;
            this.Releases = new List<Release>(Releases);
            this.Titles = new List<ModTitle>(Titles);
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
            
            this.Releases.Add(newRelease);
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
            
            this.Titles.Add(newModTitle);
        }

        public Boolean TryRemoveTitle(ModTitle titleToRemove)
        {
            if(this.Titles.Count <= 1) throw new InvalidOperationException("A mod must always have at least one title. You may not remove the last title from a mod's list of titles.");
            return this.Titles.Remove(titleToRemove);
        }
    }
}
