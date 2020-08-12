using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using FactorioProductionCells.Domain.Validators;

namespace FactorioProductionCells.Domain.Entities
{
    public class Language : IEquatable<Language>
    {
        public const int EnglishNameLength = 100;
        public const int LanguageTagLength = 50;

        private Language() {}

        public Language(Language original)
        {
            ObjectValidator.ValidateRequiredObject(original, nameof(original));
            
            if (original.Id != null) this.Id = original.Id;
            if (original.EnglishName != null) this.EnglishName = original.EnglishName;
            if (original.LanguageTag != null) this.LanguageTag = original.LanguageTag;
            if (original.Culture != null) this.Culture = original.Culture;
            this.IsDefault = original.IsDefault;
            this.ModTitles = original.ModTitles != null ? original.ModTitles.ConvertAll(title => new ModTitle(title)) : null;
        }

        public Language(String EnglishName, String LanguageTag, Boolean IsDefault = false)
        {
            StringValidator.ValidateRequiredStringWithMaxLength(EnglishName, nameof(EnglishName), Language.EnglishNameLength);
            StringValidator.ValidateRequiredStringWithMaxLength(LanguageTag, nameof(LanguageTag), Language.LanguageTagLength);

            // TODO: There must be a better way to get a culture from a language tag than this. I'd use CultureInfo(String) constructor to validate, but its behavior
            // is dependent on operating system, and Ubuntu seems to play pretty fast and loose with language tag validation. This approach works great for expected tags
            // like "en" and "en-us", but for some reason, the constructor doesn't throw a CultureNotFoundException for the tags "nurrrr", "12-lz", and "ql-!>?" on Ubuntu.
            CultureInfo languageCulture = CultureInfo.GetCultures(CultureTypes.NeutralCultures).FirstOrDefault(culture => culture.Name.ToLower() == LanguageTag.ToLower());
            if (languageCulture == null) languageCulture = CultureInfo.GetCultures(CultureTypes.SpecificCultures).FirstOrDefault(culture => culture.Name.ToLower() == LanguageTag.ToLower());
            if (languageCulture == null) throw new CultureNotFoundException($"Culture is not supported. (Parameter 'LanguageTag'){Environment.NewLine}{LanguageTag} is an invalid culture identifier.");
            
            this.EnglishName = EnglishName;
            this.LanguageTag = LanguageTag;
            this.Culture = languageCulture;
            this.IsDefault = IsDefault;
        }
        
        // Note: This constructor is only intended to be used to testing purposes. Typically, the Id should only be set from the database.
        public Language(Guid Id, String EnglishName, String LanguageTag, Boolean IsDefault = false) : this(EnglishName, LanguageTag, IsDefault)
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
        public List<ModTitle> ModTitles { get; private set; } = new List<ModTitle>();

        public Boolean Equals(Language right)
        {
            return right != null
                && ((this.Id == null && right.Id == null) || this.Id == right.Id)
                && ((this.EnglishName == null && right.EnglishName == null) || this.EnglishName == right.EnglishName)
                && ((this.LanguageTag == null && right.LanguageTag == null) || this.LanguageTag == right.LanguageTag)
                && ((this.Culture == null && right.Culture == null) || this.Culture == right.Culture)
                && this.IsDefault == right.IsDefault
                && ((this.ModTitles == null && right.ModTitles == null) || Enumerable.SequenceEqual(this.ModTitles, right.ModTitles));
        }
    }
}
