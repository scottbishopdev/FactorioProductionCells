using System;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Validators;

namespace FactorioProductionCells.Domain.Entities
{
    public class ModTitle : AuditableEntity
    {
        public const Int32 TitleLength = 200;
        private ModTitle() {}

        public ModTitle(Language Language, String Title)
        {
            ObjectValidator.ValidateRequiredObject(Language, nameof(Language));
            StringValidator.ValidateRequiredStringWithMaxLength(Title, nameof(Title), ModTitle.TitleLength);
            
            this.Language = Language;
            this.LanguageId = Language.Id;
            this.Title = Title;
        }

        public Guid ModId { get; set; }
        public Guid LanguageId { get; set; }
        public String Title { get; private set; }

        // Navigation properties
        public Mod Mod { get; private set; }
        public Language Language { get; private set; }
    }
}
