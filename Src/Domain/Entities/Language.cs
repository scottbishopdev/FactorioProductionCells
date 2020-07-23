using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
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
            StringValidator.ValidateRequiredStringWithMaxLength(LanguageTag, nameof(LanguageTag), Language.LanguageTagLength);

            // TODO: There must be a better way to get a culture from a language tag than this. I'd use CultureInfo(String) constructor to validate, but its behavior
            // is dependent on operating system, and Ubuntu seems to play pretty fast and loose with language tag validation. This approach works great for expected tags
            // like "en" and "en-us", but for some reason, the constructor doesn't throw a CultureNotFoundException for the tags "nurrrr", "12-lz", and "ql-!>?" on Ubuntu.
            CultureInfo languageCulture = CultureInfo.GetCultures(CultureTypes.SpecificCultures).FirstOrDefault(culture => culture.Name.ToLower() == LanguageTag);
            if (languageCulture == null) languageCulture = CultureInfo.GetCultures(CultureTypes.SpecificCultures).FirstOrDefault(culture => culture.Parent.Name.ToLower() == LanguageTag);
            if (languageCulture == null) throw new CultureNotFoundException($"Culture is not supported. (Parameter 'LanguageTag'){Environment.NewLine}{LanguageTag} is an invalid culture identifier.");

            this.EnglishName = EnglishName;
            this.LanguageTag = LanguageTag;
            this.Culture = languageCulture;
            this.IsDefault = IsDefault;
        }
        
        // Note: This constructor is only intended to be used to testing purposes. Typically, the Id should only be set from the database.
        public Language(String EnglishName, String LanguageTag, Guid Id, Boolean IsDefault = false) : this(EnglishName, LanguageTag, IsDefault)
        {
            this.Id = Id;
        }

        public Guid Id { get; private set; }
        public String EnglishName { get; private set; }
        // Note: This field is intended to store a IETF BCP 47 language tag. See https://en.wikipedia.org/wiki/IETF_language_tag for information.
        public String LanguageTag { get; private set; }
        public CultureInfo Culture { get; private set; }
        public Boolean IsDefault { get; private set; }

        // Navigation properties
        public IList<ModTitle> ModTitles { get; private set; } = new List<ModTitle>();
    }
}
