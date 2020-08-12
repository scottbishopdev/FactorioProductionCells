using System;
using Xunit;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.TestData.Common;
using FactorioProductionCells.TestData.Domain.Entities;

namespace FactorioProductionCells.Domain.UnitTests.Entities
{
    public class ModTitleTests
    {
        #region Copy Constructor
        [Theory]
        [MemberData(nameof(ModTitleTestData.ValidStaticModTitlesWithLanguage), MemberType=typeof(ModTitleTestData))]
        [MemberData(nameof(ModTitleTestData.ValidRandomModTitlesWithLanguage), MemberType=typeof(ModTitleTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectLanguage(ModTitle modTitle, Language expectedLanguage)
        {
            var testModTitle = new ModTitle(modTitle);
            Assert.Equal(expectedLanguage, modTitle.Language);
        }

        [Theory]
        [MemberData(nameof(ModTitleTestData.ValidStaticModTitlesWithLanguageId), MemberType=typeof(ModTitleTestData))]
        [MemberData(nameof(ModTitleTestData.ValidRandomModTitlesWithLanguageId), MemberType=typeof(ModTitleTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectLanguageId(ModTitle modTitle, Guid expectedLanguageId)
        {
            var testModTitle = new ModTitle(modTitle);
            Assert.Equal(expectedLanguageId, modTitle.LanguageId);
        }

        [Theory]
        [MemberData(nameof(ModTitleTestData.ValidStaticModTitlesWithTitle), MemberType=typeof(ModTitleTestData))]
        [MemberData(nameof(ModTitleTestData.ValidRandomModTitlesWithTitle), MemberType=typeof(ModTitleTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectTitle(ModTitle modTitle, String expectedTitle)
        {
            var testModTitle = new ModTitle(modTitle);
            Assert.Equal(expectedTitle, modTitle.Title);
        }

        [Fact]
        public void CopyConstructor_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ModTitle(null));
            Assert.Equal("original is required. (Parameter 'original')", exception.Message);
        }
        #endregion

        #region Individual Value Constructor
        [Theory]
        [MemberData(nameof(ModTitleTestData.ValidStaticModTitlesCreationProperties), MemberType=typeof(ModTitleTestData))]
        [MemberData(nameof(ModTitleTestData.ValidRandomModTitlesCreationProperties), MemberType=typeof(ModTitleTestData))]
        public void ModTitleConstructor_WhenValidParameters_ReturnsCorrectLanguage(Language language, String title)
        {
            var testModTitle = new ModTitle(language, title);
            Assert.Equal(language, testModTitle.Language);
        }

        [Theory]
        [MemberData(nameof(ModTitleTestData.ValidStaticModTitlesCreationProperties), MemberType=typeof(ModTitleTestData))]
        [MemberData(nameof(ModTitleTestData.ValidRandomModTitlesCreationProperties), MemberType=typeof(ModTitleTestData))]
        public void ModTitleConstructor_WhenValidParameters_ReturnsCorrectLanguageId(Language language, String title)
        {
            var testModTitle = new ModTitle(language, title);
            Assert.Equal(language.Id, testModTitle.LanguageId);
        }

        [Theory]
        [MemberData(nameof(ModTitleTestData.ValidStaticModTitlesCreationProperties), MemberType=typeof(ModTitleTestData))]
        [MemberData(nameof(ModTitleTestData.ValidRandomModTitlesCreationProperties), MemberType=typeof(ModTitleTestData))]
        public void ModTitleConstructor_WhenValidParameters_ReturnsCorrectTitle(Language language, String title)
        {
            var testModTitle = new ModTitle(language, title);
            Assert.Equal(title, testModTitle.Title);
        }

        [Fact]
        public void ModTitleConstructor_WhenLanguageIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ModTitle(null, ModTestData.TestModTestDataPoint.Name));
            Assert.Equal("Language is required. (Parameter 'Language')", exception.Message);
        }

        [Fact]
        public void ModTitleConstructor_WhenTitleIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ModTitle(LanguageTestData.EnglishWithoutId, null));
            Assert.Equal("Title is required. (Parameter 'Title')", exception.Message);
        }

        [Theory]
        [MemberData(nameof(CommonTestData.EmptyAndWhitespaceStrings), MemberType=typeof(CommonTestData))]
        public void ModTitleConstructor_WhenTitleIsEmpty_ThrowsArgumentException(String modTitleString)
        {
            var exception = Assert.Throws<ArgumentException>(() => new ModTitle(LanguageTestData.EnglishWithoutId, modTitleString));
            Assert.Equal("Title may not be empty. (Parameter 'Title')", exception.Message);
        }

        [Fact]
        public void ModTitleConstructor_WhenTitleIsTooLong_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new ModTitle(
                Language: LanguageTestData.EnglishWithoutId,
                Title: TestDataHelpers.GetRandomizedUnicodeCharacterString(ModTitle.TitleLength + 1)));
            Assert.Equal($"Title must not exceed {ModTitle.TitleLength.ToString()} characters. (Parameter 'Title')", exception.Message);
        }
        #endregion

        #region Equals
        [Theory]
        [MemberData(nameof(ModTitleTestData.ValidStaticEqualModTitlePairs), MemberType=typeof(ModTitleTestData))]
        [MemberData(nameof(ModTitleTestData.ValidRandomEqualModTitlePairs), MemberType=typeof(ModTitleTestData))]
        public void Equals_WhenProvidedEqualModTitles_ReturnsTrue(ModTitle left, ModTitle right)
        {
            Assert.True(left.Equals(right));
        }

        [Theory]
        [MemberData(nameof(ModTitleTestData.ValidStaticNonEqualModTitlePairs), MemberType=typeof(ModTitleTestData))]
        public void Equals_WhenProvidedNotEqualModTitles_ReturnsFalse(ModTitle left, ModTitle right)
        {
            Assert.False(left.Equals(right));
        }

        [Fact]
        public void Equals_WhenGivenNull_ReturnsFalse()
        {
            Assert.False(ModTitleTestData.TestModEnglishTitle.Equals(null));
        }
        #endregion
    }
}
