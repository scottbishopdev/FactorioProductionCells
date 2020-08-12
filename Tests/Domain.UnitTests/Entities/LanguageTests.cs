using System;
using System.Globalization;
using Xunit;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.TestData.Common;
using FactorioProductionCells.TestData.Domain.Entities;

namespace FactorioProductionCells.Domain.UnitTests.Entities
{
    public class LanguageTests
    {
        #region Copy Constructor
        [Theory]
        [MemberData(nameof(LanguageTestData.ValidStaticLanguagesWithId), MemberType=typeof(LanguageTestData))]
        [MemberData(nameof(LanguageTestData.ValidRandomLanguagesWithId), MemberType=typeof(LanguageTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectId(Language language, Guid expectedId)
        {
            var testLanguage = new Language(language);
            Assert.Equal(expectedId, testLanguage.Id);
        }

        [Theory]
        [MemberData(nameof(LanguageTestData.ValidStaticLanguagesWithEnglishName), MemberType=typeof(LanguageTestData))]
        [MemberData(nameof(LanguageTestData.ValidRandomLanguagesWithEnglishName), MemberType=typeof(LanguageTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectEnglishName(Language language, String expectedEnglishName)
        {
            var testLanguage = new Language(language);
            Assert.Equal(expectedEnglishName, testLanguage.EnglishName);
        }

        [Theory]
        [MemberData(nameof(LanguageTestData.ValidStaticLanguagesWithLanguageTag), MemberType=typeof(LanguageTestData))]
        [MemberData(nameof(LanguageTestData.ValidRandomLanguagesWithLanguageTag), MemberType=typeof(LanguageTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectLanguageTag(Language language, String expectedLanguageTag)
        {
            var testLanguage = new Language(language);
            Assert.Equal(expectedLanguageTag, testLanguage.LanguageTag);
        }

        [Theory]
        [MemberData(nameof(LanguageTestData.ValidStaticLanguagesWithIsDefault), MemberType=typeof(LanguageTestData))]
        [MemberData(nameof(LanguageTestData.ValidRandomLanguagesWithIsDefault), MemberType=typeof(LanguageTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectIsDefault(Language language, Boolean expectedIsDefault)
        {
            var testLanguage = new Language(language);
            Assert.Equal(expectedIsDefault, testLanguage.IsDefault);
        }

        [Theory]
        [MemberData(nameof(LanguageTestData.ValidStaticLanguagesWithExpectedCulture), MemberType=typeof(LanguageTestData))]
        [MemberData(nameof(LanguageTestData.ValidRandomLanguagesWithExpectedCulture), MemberType=typeof(LanguageTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectCulture(Language language, CultureInfo expectedCulture)
        {
            var testLanguage = new Language(language);
            Assert.Equal(expectedCulture, testLanguage.Culture);
        }

        [Fact]
        public void CopyConstructor_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Dependency(null));
            Assert.Equal("original is required. (Parameter 'original')", exception.Message);
        }
        #endregion

        #region Individual Value Constructor
        [Theory]
        [MemberData(nameof(LanguageTestData.ValidStaticLanguagesCreationProperties), MemberType=typeof(LanguageTestData))]
        [MemberData(nameof(LanguageTestData.ValidRandomLanguagesCreationProperties), MemberType=typeof(LanguageTestData))]
        public void LanguageConstructor_WhenValidParameters_ReturnsCorrectEnglishName(String englishName, String languageTag, Boolean isDefault)
        {
            var testLanguage = new Language(englishName, languageTag, isDefault);
            Assert.Equal(englishName, testLanguage.EnglishName);
        }

        [Theory]
        [MemberData(nameof(LanguageTestData.ValidStaticLanguagesCreationProperties), MemberType=typeof(LanguageTestData))]
        [MemberData(nameof(LanguageTestData.ValidRandomLanguagesCreationProperties), MemberType=typeof(LanguageTestData))]
        public void LanguageConstructor_WhenValidParameters_ReturnsCorrectLanguageTag(String englishName, String languageTag, Boolean isDefault)
        {
            var testLanguage = new Language(englishName, languageTag, isDefault);
            Assert.Equal(languageTag, testLanguage.LanguageTag);
        }

        [Theory]
        [MemberData(nameof(LanguageTestData.ValidStaticLanguagesCreationProperties), MemberType=typeof(LanguageTestData))]
        [MemberData(nameof(LanguageTestData.ValidRandomLanguagesCreationProperties), MemberType=typeof(LanguageTestData))]
        public void LanguageConstructor_WhenValidParametersWithoutIsDefault_ReturnsCorrectIsDefault(String englishName, String languageTag, Boolean isDefault)
        {
            var testLanguage = new Language(englishName, languageTag, isDefault);
            Assert.Equal(isDefault, testLanguage.IsDefault);
        }

        [Theory]
        [MemberData(nameof(LanguageTestData.ValidStaticLanguagesCreationPropertiesWithExpectedCulture), MemberType=typeof(LanguageTestData))]
        [MemberData(nameof(LanguageTestData.ValidRandomLanguagesCreationPropertiesWithExpectedCulture), MemberType=typeof(LanguageTestData))]
        public void LanguageConstructor_WhenValidParametersWithoutIsDefault_ReturnsCorrectCulture(String englishName, String languageTag, Boolean isDefault, CultureInfo expectedCulture)
        {
            var testLanguage = new Language(englishName, languageTag, isDefault);
            Assert.Equal(expectedCulture, testLanguage.Culture);
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
                EnglishName: TestDataHelpers.GetRandomCharacterStringFromSet("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", Language.EnglishNameLength + 1),
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
                LanguageTag: TestDataHelpers.GetRandomCharacterStringFromSet("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-", Language.LanguageTagLength + 1),
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
            Assert.Equal($"Culture is not supported. (Parameter 'LanguageTag'){Environment.NewLine}{languageTag} is an invalid culture identifier.", exception.Message);
        }
        #endregion

        #region Language Constructor With Id
        [Theory]
        [MemberData(nameof(LanguageTestData.ValidStaticLanguagesCreationPropertiesWithId), MemberType=typeof(LanguageTestData))]
        [MemberData(nameof(LanguageTestData.ValidRandomLanguagesCreationPropertiesWithId), MemberType=typeof(LanguageTestData))]
        public void LanguageConstructorWithId_WhenValidParameters_ReturnsCorrectId(String englishName, String languageTag, Boolean isDefault, Guid id)
        {
            var testLanguage = new Language(id, englishName, languageTag, isDefault);
            Assert.Equal(id, testLanguage.Id);
        }
        #endregion

        #region Equals
        [Theory]
        [MemberData(nameof(LanguageTestData.ValidStaticEqualLanguagePairs), MemberType=typeof(LanguageTestData))]
        [MemberData(nameof(LanguageTestData.ValidRandomEqualLanguagePairs), MemberType=typeof(LanguageTestData))]
        public void Equals_WhenProvidedEqualDependencies_ReturnsTrue(Language left, Language right)
        {
            Assert.True(left.Equals(right));
        }

        [Theory]
        [MemberData(nameof(LanguageTestData.ValidStaticNonEqualLanguagePairs), MemberType=typeof(LanguageTestData))]
        public void Equals_WhenProvidedNotEqualDependencies_ReturnsFalse(Language left, Language right)
        {
            Assert.False(left.Equals(right));
        }

        [Fact]
        public void Equals_WhenGivenNull_ReturnsFalse()
        {
            Assert.False(LanguageTestData.EnglishWithId.Equals(null));
        }
        #endregion
    }
}
