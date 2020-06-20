using System;
using System.Collections.Generic;
using FactorioProductionCells.Domain.Validators;

namespace FactorioProductionCells.Domain.Entities
{
    public class Language
    {
        public const int EnglishNameLength = 100;
        public const int LanguageTagLength = 20;
        
        private Language() {}

        public Language(String EnglishName, String LanguageTag, Boolean IsDefault = false)
        {
            StringValidator.ValidateRequiredStringWithMaxLength(EnglishName, nameof(EnglishName), Language.EnglishNameLength);
            // TODO: Add validation to ensure that LanguageTag looks like an actual language tag.
            StringValidator.ValidateRequiredStringWithMaxLength(LanguageTag, nameof(LanguageTag), Language.LanguageTagLength);

            this.EnglishName = EnglishName;
            this.LanguageTag = LanguageTag;
            this.IsDefault = IsDefault;
        }
        
        public Guid Id { get; private set; }
        public String EnglishName { get; private set; }
        
        // Note: This field is intended to store a IETF BCP 47 language tag. See https://en.wikipedia.org/wiki/IETF_language_tag for information.
        public String LanguageTag { get; private set; }
        public Boolean IsDefault { get; private set; }

        // Navigation properties
        public IList<ModTitle> ModTitles { get; private set; } = new List<ModTitle>();
    }
}
