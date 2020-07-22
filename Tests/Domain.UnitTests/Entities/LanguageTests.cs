using System;
using System.Globalization;
using System.Linq;
using Xunit;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Domain.UnitTests.Entities
{
    public class LanguageTests
    {
        internal static Guid EnglishId = new Guid("0e557edc-ae73-4799-8731-80761d775c8c");
        internal static Guid GermanId = new Guid("6f7bd7a7-eecb-4a30-9731-59182d25bcf2");
        internal static Language English = new Language("English", "en", true);
        internal static Language EnglishWithId = new Language("English", "en", EnglishId, true);
        internal static Language GermanWithId = new Language("German", "de", GermanId, false);
        internal static Language EnglishUnitedStatesTest = new Language("English", "en-us");
        private static Random Random = new Random();

        #region LanguageConstructor
        [Fact]
        public void LanguageConstructor_WhenValidParameters_ReturnsCorrectEnglishName()
        {
            Assert.Equal("English", English.EnglishName);
        }

        [Fact]
        public void LanguageConstructor_WhenValidParameters_ReturnsCorrectLanguageTag()
        {
            Assert.Equal("en", English.LanguageTag);
        }

        [Fact]
        public void LanguageConstructor_WhenValidParameters_ReturnsCorrectCulture()
        {
            Assert.Equal(new CultureInfo("en"), English.Culture);
        }

        [Fact]
        public void LanguageConstructor_WhenValidParametersWithoutIsDefault_ReturnsCorrectIsDefault()
        {
            Assert.False(EnglishUnitedStatesTest.IsDefault);
        }

        [Fact]
        public void LanguageConstructor_WhenValidsWithIsDefault_ReturnsCorrectIsDefault()
        {
            Assert.True(English.IsDefault);
        }

        [Fact]
        public void LanguageConstructor_WhenEnglishNameIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Language(
                EnglishName: null,
                LanguageTag: "en-us",
                IsDefault: false));
            Assert.Equal("EnglishName is required. (Parameter 'EnglishName')", exception.Message);
        }

        [Fact]
        public void LanguageConstructor_WhenEnglishNameIsEmpty_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Language(
                EnglishName: "",
                LanguageTag: "en-us",
                IsDefault: false));
            Assert.Equal("EnglishName may not be empty. (Parameter 'EnglishName')", exception.Message);
        }

        [Fact]
        public void LanguageConstructor_WhenEnglishNameIsTooLong_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Language(
                EnglishName: GetRandomCharacterString("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", Language.EnglishNameLength + 1),
                LanguageTag: "en-us",
                IsDefault: false));
            Assert.Equal($"EnglishName must not exceed {Language.EnglishNameLength.ToString()} characters. (Parameter 'EnglishName')", exception.Message);
        }

        [Fact]
        public void LanguageConstructor_WhenLanguageTagIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Language(
                EnglishName: "English",
                LanguageTag: null,
                IsDefault: false));
            Assert.Equal("LanguageTag is required. (Parameter 'LanguageTag')", exception.Message);
        }

        [Fact]
        public void LanguageConstructor_WhenLanguageTagIsEmpty_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Language(
                EnglishName: "English",
                LanguageTag: "",
                IsDefault: false));
            Assert.Equal("LanguageTag may not be empty. (Parameter 'LanguageTag')", exception.Message);
        }

        [Fact]
        public void LanguageConstructor_WhenLanguageTagIsTooLong_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Language(
                EnglishName: "English",
                LanguageTag: GetRandomCharacterString("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-", Language.LanguageTagLength + 1),
                IsDefault: false));
            Assert.Equal($"LanguageTag must not exceed {Language.LanguageTagLength.ToString()} characters. (Parameter 'LanguageTag')", exception.Message);
        }

        [Theory]
        [InlineData("!")]
        [InlineData("nurrrr")]
        [InlineData("12-lz")]
        [InlineData("ql-!>?")]
        [InlineData("Not a Babel Fish.")]
        public void LanguageConstructor_WhenLanguageTagIsNotValidLanguageTag_ThrowsCultureNotFoundException(String languageTag)
        {
            var exception = Assert.Throws<CultureNotFoundException>(() => new Language(
                EnglishName: "English",
                LanguageTag: languageTag,
                IsDefault: false));
            Assert.Equal($"Culture is not supported. (Parameter 'name')\r\n{languageTag} is an invalid culture identifier.", exception.Message);
        }
        #endregion

        #region LanguageConstructorWithGuid
        [Fact]
        public void LanguageConstructorWithGuid_WhenValidParameters_ReturnsCorrectId()
        {
            Assert.Equal(EnglishId, EnglishWithId.Id);
        }
        #endregion

        private static String GetRandomCharacterString(String characterSet, Int32 length)
        {
            return new String(Enumerable.Repeat(characterSet, length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
