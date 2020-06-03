using System;
using System.Collections.Generic;
using FactorioProductionCells.Domain.Validators;

namespace FactorioProductionCells.Domain.Entities
{
    public class Language
    {
        private Language() {}

        public Language(string EnglishName, String LanguageCode, bool IsDefault = false)
        {
            StringValidator.ValidateRequiredStringWithMaxLength(EnglishName, nameof(EnglishName), Language.EnglishNameLength);
            StringValidator.ValidateRequiredStringWithMaxLength(LanguageCode, nameof(LanguageCode), Language.LanguageCodeLength);
            
            this.EnglishName = EnglishName;
            this.LanguageCode = LanguageCode;
            this.IsDefault = IsDefault;
        }
        
        public const int EnglishNameLength = 50;
        public const int LanguageCodeLength = 20;

        public Guid Id { get; private set; }
        public String EnglishName { get; private set; }
        public String LanguageCode { get; private set; }
        public Boolean IsDefault { get; private set; }

        // Navigation properties
        public IList<ModTitle> ModTitles { get; private set; } = new List<ModTitle>();
    }
}
