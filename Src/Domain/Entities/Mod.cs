using System;
using System.Collections.Generic;
using System.Linq;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Validators;

namespace FactorioProductionCells.Domain.Entities
{
    public class Mod : AuditableEntity
    {
        private Mod() {}

        public Mod(string Name, IList<ModTitle> Titles, IList<Release> Releases)
        {
            StringValidator.ValidateRequiredStringWithMaxLength(Name, nameof(Name), Mod.NameLength);
            ListValidator.ValidateRequiredListNotEmpty<ModTitle>(Titles, nameof(Titles));
            ListValidator.ValidateRequiredListNotEmpty<Release>(Releases, nameof(Releases));
            
            this.Name = Name;
            this.Releases = Releases;
            this.Titles = Titles;
        }

        public const Int32 NameLength = 200;

        public Guid Id { get; set; }
        public String Name { get; private set; }
        public IList<ModTitle> Titles { get; private set; } = new List<ModTitle>();
        public IList<Release> Releases { get; private set; } = new List<Release>();
    }
}
