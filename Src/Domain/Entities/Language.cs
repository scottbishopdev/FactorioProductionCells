using System;
using System.Collections.Generic;
using FactorioProductionCells.Domain.Validators;

namespace FactorioProductionCells.Domain.Entities
{
    public class Language
    {
        public const int EnglishNameLength = 50;
        public const int LanguageCodeLength = 20;
        
        private Language() {}

        public Language(String EnglishName, String LanguageCode, Boolean IsDefault = false)
        {
            StringValidator.ValidateRequiredStringWithMaxLength(EnglishName, nameof(EnglishName), Language.EnglishNameLength);
            StringValidator.ValidateRequiredStringWithMaxLength(LanguageCode, nameof(LanguageCode), Language.LanguageCodeLength);

            this.EnglishName = EnglishName;
            this.LanguageCode = LanguageCode;
            this.IsDefault = IsDefault;
        }
        
        // TODO: Add validation that LanguageCode looks like an actual language code?

        public Guid Id { get; private set; }
        public String EnglishName { get; private set; }
        
        // Note: This field is intended to store a IETF BCP 47 language tag. See https://en.wikipedia.org/wiki/IETF_language_tag for information.
        public String LanguageCode { get; private set; }
        public Boolean IsDefault { get; private set; }

        // Navigation properties
        public IList<ModTitle> ModTitles { get; private set; } = new List<ModTitle>();
    }
}
