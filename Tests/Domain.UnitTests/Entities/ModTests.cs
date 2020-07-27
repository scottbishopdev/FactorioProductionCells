using System;
using System.Text;
using System.Collections.Generic;
using Xunit;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.ValueObjects;
using FactorioProductionCells.Domain.UnitTests.ValueObjects;

namespace FactorioProductionCells.Domain.UnitTests.Entities
{
    public class ModTests
    {
        // TODO: Implement a class context to create the test mod clone instance for all tests. We end up doing that a lot.
        public static List<ModTitle> TestModTitles = new List<ModTitle> { ModTitleTests.TestModEnglishTitle, ModTitleTests.TestModGermanTitle };
        public static List<Release> TestModReleases = new List<Release> { ReleaseTests.TestModReleaseOnePointFourPointSixteen, ReleaseTests.TestModReleaseOnePointFivePointOne };
        public static Mod TestMod = new Mod(
            Name: "Test Mod",
            Titles: TestModTitles,
            Releases: TestModReleases
        );
        private static Random Random = new Random();

        #region ModConstructor
        [Fact]
        public void ModConstructor_WhenValidParameters_ReturnsCorrectName()
        {
            Assert.Equal("Test Mod", TestMod.Name);
        }

        [Fact]
        public void ModConstructor_WhenValidParameters_ReturnsCorrectReleases()
        {
            Assert.Equal(TestModTitles, TestMod.Titles);
        }

        [Fact]
        public void ModConstructor_WhenValidParameters_ReturnsCorrectTitles()
        {
            Assert.Equal(TestModReleases, TestMod.Releases);
        }

        [Fact]
        public void ModConstructor_WhenNameIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Mod(
                Name: null,
                Titles: TestModTitles,
                Releases: TestModReleases));
            Assert.Equal("Name is required. (Parameter 'Name')", exception.Message);
        }

        [Fact]
        public void ModConstructor_WhenNameIsEmpty_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Mod(
                Name: "",
                Titles: TestModTitles,
                Releases: TestModReleases));
            Assert.Equal("Name may not be empty. (Parameter 'Name')", exception.Message);
        }

        [Fact]
        public void ModConstructor_WhenNameIsTooLong_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Mod(
                Name: GetRandomCharacterString(Mod.NameLength + 1),
                Titles: TestModTitles,
                Releases: TestModReleases));
            Assert.Equal($"Name must not exceed {Mod.NameLength.ToString()} characters. (Parameter 'Name')", exception.Message);
        }

        [Fact]
        public void ModConstructor_WhenTitlesIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Mod(
                Name: "Test Mod",
                Titles: null,
                Releases: TestModReleases));
            Assert.Equal("Titles is required. (Parameter 'Titles')", exception.Message);
        }

        [Fact]
        public void ModConstructor_WhenTitlesIsEmpty_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Mod(
                Name: "Test Mod",
                Titles: new List<ModTitle>(),
                Releases: TestModReleases));
            Assert.Equal("Titles must contain at least one entry. (Parameter 'Titles')", exception.Message);
        }

        [Fact]
        public void ModConstructor_WhenReleasesIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Mod(
                Name: "Test Mod",
                Titles: TestModTitles,
                Releases: null));
            Assert.Equal("Releases is required. (Parameter 'Releases')", exception.Message);
        }

        /*
        [Fact]
        public void ModConstructor_WhenReleasesIsEmpty_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Mod(
                Name: "Test Mod",
                Titles: TestModTitles,
                Releases: new List<Release>()));
            Assert.Equal("Releases must contain at least one entry. (Parameter 'Releases')", exception.Message);
        }
        */
        #endregion

        #region TryAddRelease
        [Fact]
        public void TryAddRelease_WhenGivenValidRelease_AddsReleaseToList()
        {
            Mod testMod = new Mod("Test Mod", TestModTitles, new List<Release> { ReleaseTests.TestModReleaseOnePointFourPointSixteen });
            testMod.TryAddRelease(ReleaseTests.TestModReleaseOnePointFivePointOne);
            Assert.Contains(ReleaseTests.TestModReleaseOnePointFivePointOne, testMod.Releases);
        }
        
        [Fact]
        public void TryAddRelease_WhenNewReleaseIsNull_ThrowsArgumentNullException()
        {
            Mod testMod = new Mod("Test Mod", TestModTitles, TestModReleases);
            var exception = Assert.Throws<ArgumentNullException>(() => testMod.TryAddRelease(null));
            Assert.Equal("newRelease is required. (Parameter 'newRelease')", exception.Message);
        }

        [Fact]
        public void TryAddRelease_WhenNewReleaseModVersionAlreadyExists_ThrowsInvalidOperationException()
        {
            Mod testMod = new Mod("Test Mod", TestModTitles, TestModReleases);
            var exception = Assert.Throws<InvalidOperationException>(() => testMod.TryAddRelease(ReleaseTests.TestModReleaseOnePointFourPointSixteen));
            Assert.Equal("A release with the specified version already exists for this mod.", exception.Message);
        }

        [Fact]
        public void TryAddRelease_WhenNewReleaseDownloadUrlModNameDoesNotMatch_ThrowsInvalidOperationException()
        {
            Mod testMod = new Mod("Test Mod", TestModTitles, TestModReleases);
            var exception = Assert.Throws<InvalidOperationException>(() => testMod.TryAddRelease( new Release(
                ReleasedAt: DateTime.Parse("11/25/2019 6:00:00 PM"),
                Sha1String: "3ca7285d4afaf29e85e838bf85fc0844d970ca6a",
                ReleaseDownloadUrl: ReleaseDownloadUrlTests.NotATestModDownloadUrl,
                ReleaseFileName: ReleaseFileName.For("Test Mod_1.5.2.zip"), 
                ModVersion: ModVersion.For("1.5.2"),
                FactorioVersion: FactorioVersionTests.ZeroPointSeventeen,
                Dependencies: new List<Dependency> { DependencyTests.TestModBaseDependencyFromFor }
            )));
            Assert.Equal("The specified download URL does not properly reference this mod.", exception.Message);
        }

        [Fact]
        public void TryAddRelease_WhenNewReleaseFileNameModNameDoesNotMatch_ThrowsInvalidOperationException()
        {
            Mod testMod = new Mod("Test Mod", TestModTitles, TestModReleases);
            var exception = Assert.Throws<InvalidOperationException>(() => testMod.TryAddRelease( new Release(
                ReleasedAt: DateTime.Parse("11/25/2019 6:00:00 PM"),
                Sha1String: "3ca7285d4afaf29e85e838bf85fc0844d970ca6a",
                ReleaseDownloadUrl: ReleaseDownloadUrlTests.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTests.NotATestModReleaseFileName,
                ModVersion: ModVersion.For("2.5.17"),
                FactorioVersion: FactorioVersionTests.ZeroPointSeventeen,
                Dependencies: new List<Dependency> { DependencyTests.TestModBaseDependencyFromFor }
            )));
            Assert.Equal("The specified release file name does not properly reference this mod.", exception.Message);
        }
        #endregion

        #region TryRemoveRelease
        [Fact]
        public void TryRemoveRelease_WhenReleaseExists_RemovesReleaseAndReturnsTrue()
        {
            Mod testMod = new Mod("Test Mod", TestModTitles, TestModReleases);
            Assert.True(testMod.TryRemoveRelease(ReleaseTests.TestModReleaseOnePointFivePointOne));
            Assert.DoesNotContain(ReleaseTests.TestModReleaseOnePointFivePointOne, testMod.Releases);
        }

        [Fact]
        public void TryRemoveRelease_WhenReleaseDoesNotExist_ReturnsFalse()
        {
            Mod testMod = new Mod("Test Mod", TestModTitles, TestModReleases);
            Assert.False(testMod.TryRemoveRelease(new Release(
                ReleasedAt: DateTime.Parse("7/16/2020 11:59:56 PM"),
                Sha1String: "8fddce7fcf6adc504b573f0ca1fc1dcef63d85e7",
                ReleaseDownloadUrl: ReleaseDownloadUrl.For("/download/Test%20Mod/9876543210fedc9876543210"),
                ReleaseFileName: ReleaseFileName.For("Test Mod_1.6.0.zip"),
                ModVersion: ModVersion.For("1.6.0"),
                FactorioVersion: FactorioVersionTests.ZeroPointSeventeen,
                Dependencies: ReleaseTests.TestModReleaseDependencies)));
        }

        [Fact]
        public void TryRemoveRelease_WhenOneReleaseLeft_ThrowsInvalidOperationException()
        {
            Mod testMod = new Mod("Test Mod", TestModTitles, new List<Release> { ReleaseTests.TestModReleaseOnePointFivePointOne });
            var exception = Assert.Throws<InvalidOperationException>(() => testMod.TryRemoveRelease(ReleaseTests.TestModReleaseOnePointFivePointOne));
            Assert.Equal("A mod must always have at least one release. You may not remove the last release from a mod's list of releases.", exception.Message);
        }
        #endregion
        
        #region GetLatestRelease
        [Fact]
        public void GetLatestRelease_WhenInvoked_ReturnsMostRecentRelease()
        {
            Assert.Equal(ReleaseTests.TestModReleaseOnePointFivePointOne, TestMod.GetLatestRelease());
        }
        #endregion

        #region TryAddTitle
        [Fact]
        public void TryAddTitle_WhenGivenValidModTitle_AddsModTitleToList()
        {
            Mod testMod = new Mod("Test Mod", new List<ModTitle> { ModTitleTests.TestModEnglishTitle }, TestModReleases);
            testMod.TryAddTitle(ModTitleTests.TestModGermanTitle);
            Assert.Contains(ModTitleTests.TestModGermanTitle, testMod.Titles);
        }
        
        [Fact]
        public void TryAddTitle_WhenNewModTitleIsNull_ThrowsArgumentNullException()
        {
            Mod testMod = new Mod("Test Mod", TestModTitles, TestModReleases);
            var exception = Assert.Throws<ArgumentNullException>(() => testMod.TryAddTitle(null));
            Assert.Equal("newModTitle is required. (Parameter 'newModTitle')", exception.Message);
        }

        [Fact]
        public void TryAddTitle_WhenNewModTitleLanguageAlreadyExists_ThrowsInvalidOperationException()
        {
            Mod testMod = new Mod("Test Mod", TestModTitles, TestModReleases);
            var exception = Assert.Throws<InvalidOperationException>(() => testMod.TryAddTitle(new ModTitle(LanguageTests.GermanWithId, "Übungsmod")));
            Assert.Equal("A title with the specified language already exists for this mod.", exception.Message);
        }
        #endregion

        #region TryRemoveTitle
        [Fact]
        public void TryRemoveTitle_WhenTitleExists_RemovesReleaseAndReturnsTrue()
        {
            Mod testMod = new Mod("Test Mod", TestModTitles, TestModReleases);
            Assert.True(testMod.TryRemoveTitle(ModTitleTests.TestModGermanTitle));
            Assert.DoesNotContain(ModTitleTests.TestModGermanTitle, testMod.Titles);
        }

        [Fact]
        public void TryRemoveTitle_WhenTitleDoesNotExist_ReturnsFalse()
        {
            Mod testMod = new Mod("Test Mod", TestModTitles, TestModReleases);
            Assert.False(testMod.TryRemoveTitle(new ModTitle(LanguageTests.GermanWithId, "Übungsmod")));
        }

        [Fact]
        public void TryRemoveTitle_WhenOneTitleLeft_ThrowsInvalidOperationException()
        {
            Mod testMod = new Mod("Test Mod", new List<ModTitle> { ModTitleTests.TestModEnglishTitle }, TestModReleases);
            var exception = Assert.Throws<InvalidOperationException>(() => testMod.TryRemoveTitle(ModTitleTests.TestModEnglishTitle));
            Assert.Equal("A mod must always have at least one title. You may not remove the last title from a mod's list of titles.", exception.Message);
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
