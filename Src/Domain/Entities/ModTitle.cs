using System;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Validators;

namespace FactorioProductionCells.Domain.Entities
{
    public class ModTitle : AuditableEntity, IEquatable<ModTitle>
    {
        public const Int32 TitleLength = 200;
        private ModTitle() {}

        public ModTitle(ModTitle original)
        {
            ObjectValidator.ValidateRequiredObject(original, nameof(original));
            
            if (original.ModId != null) this.ModId = original.ModId;
            if (original.LanguageId != null) this.LanguageId = original.LanguageId;
            if (original.Title != null) this.Title = original.Title;
            this.Mod = original.Mod != null ? new Mod(original.Mod) : null;
            this.Language = original.Language != null ? new Language(original.Language) : null;
        }

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

        public Boolean Equals(ModTitle right)
        {
            return right != null
                && ((this.ModId == null && right.ModId == null) || this.ModId == right.ModId)
                && ((this.LanguageId == null && right.LanguageId == null) || this.LanguageId == right.LanguageId)
                && ((this.Title == null && right.Title == null) || this.Title == right.Title)
                && ((this.Mod == null && right.Mod == null) || this.Mod.Equals(right.Mod))
                && ((this.Language == null && right.Language == null) || this.Language.Equals(right.Language));
        }
    }
}
