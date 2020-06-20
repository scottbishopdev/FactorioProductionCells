using System;
using Xunit;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Domain.UnitTests.Entities
{
    public class LanguageTests
    {
        [Theory]
        [InlineData(null, "en-us", false)]
        [InlineData("English", null, false)]
        public void Language_WhenConstructorParameterIsNull_ThrowsArgumentNullException(String englishName, String languageTag, Boolean isDefault)
        {
            Assert.Throws<ArgumentNullException>(() => new Language(
                EnglishName: englishName,
                LanguageTag: languageTag,
                IsDefault: isDefault));
        }

        [Theory]
        [InlineData("", "en-us", false)]
        [InlineData("English", "", false)]
        public void Language_WhenStringParameterIsEmpty_ThrowsArgumentException(String englishName, String languageTag, Boolean isDefault)
        {
            Assert.Throws<ArgumentException>(() => new Language(
                EnglishName: englishName,
                LanguageTag: languageTag,
                IsDefault: isDefault));
        }

        [Theory]
        [InlineData("", "en-us", false)]
        [InlineData("English", "", false)]
        public void Language_WhenStringParameterIsTooLong_ThrowsArgumentException(String englishName, String languageTag, Boolean isDefault)
        {
            Assert.Throws<ArgumentException>(() => new Language(
                EnglishName: englishName,
                LanguageTag: languageTag,
                IsDefault: isDefault));
        }

        // TODO: Should we check that whitespace is getting trimmed?
        // TODO: Eventually, we'll need to validate that the language code string provided is valid. I might be able to do this through CultureInfo?
    }
}
