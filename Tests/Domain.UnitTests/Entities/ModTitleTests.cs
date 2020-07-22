using System;
using System.Text;
using Xunit;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Domain.UnitTests.Entities
{
    public class ModTitleTests
    {
        public static ModTitle TestModEnglishTitle = new ModTitle(LanguageTests.EnglishWithId, "Test Mod");
        public static ModTitle TestModGermanTitle = new ModTitle(LanguageTests.GermanWithId, "Prüfung Modifikator");
        private static Random Random = new Random();

        #region ModTitleConstructor
        [Fact]
        public void ModTitleConstructor_WhenValidParameters_ReturnsCorrectLanguage()
        {
            Assert.Equal(LanguageTests.EnglishWithId, TestModEnglishTitle.Language);
        }

        [Fact]
        public void ModTitleConstructor_WhenValidParameters_ReturnsCorrectLanguageId()
        {
            Assert.Equal(LanguageTests.EnglishId, TestModEnglishTitle.LanguageId);
        }

        [Fact]
        public void ModTitleConstructor_WhenValidParameters_ReturnsCorrectTitle()
        {
            Assert.Equal("Test Mod", TestModEnglishTitle.Title);
        }

        [Fact]
        public void ModTitleConstructor_WhenLanguageIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ModTitle(null, "Test Mod"));
            Assert.Equal("Language is required. (Parameter 'Language')", exception.Message);
        }

        [Fact]
        public void ModTitleConstructor_WhenTitleIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ModTitle(LanguageTests.English, null));
            Assert.Equal("Title is required. (Parameter 'Title')", exception.Message);
        }

        [Fact]
        public void ModTitleConstructor_WhenTitleIsEmpty_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new ModTitle(LanguageTests.English, ""));
            Assert.Equal("Title may not be empty. (Parameter 'Title')", exception.Message);
        }

        [Fact]
        public void ModTitleConstructor_WhenTitleIsTooLong_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new ModTitle(LanguageTests.English, GetRandomCharacterString(ModTitle.TitleLength + 1)));
            Assert.Equal($"Title must not exceed {ModTitle.TitleLength.ToString()} characters. (Parameter 'Title')", exception.Message);
        }
        #endregion
        
        private static String GetRandomCharacterString(Int32 length)
        {
            var stringBuilder = new StringBuilder();
            
            while (stringBuilder.Length - 1 <= length)
            {
                var character = Convert.ToChar(Random.Next(char.MinValue, char.MaxValue));
                if (!char.IsControl(character))
                {
                    stringBuilder.Append(character);
                }
            }
            
            return stringBuilder.ToString();
        }
    }
}
