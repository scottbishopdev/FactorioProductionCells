using System;
using System.Collections.Generic;
using Xunit;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.ValueObjects;
using FactorioProductionCells.TestData.Domain.ValueObjects;
using FactorioProductionCells.TestData.Domain.Entities;
using FactorioProductionCells.TestData.Common;

namespace FactorioProductionCells.Domain.UnitTests.Entities
{
    public class ModTests
    {
        #region CopyConstructor
        [Theory]
        [MemberData(nameof(ModTestData.ValidStaticModsWithName), MemberType=typeof(ModTestData))]
        [MemberData(nameof(ModTestData.ValidRandomModsWithName), MemberType=typeof(ModTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectName(Mod mod, String expectedName)
        {
            var testMod = new Mod(mod);
            Assert.Equal(expectedName, testMod.Name);
        }

        [Theory]
        [MemberData(nameof(ModTestData.ValidStaticModsWithTitles), MemberType=typeof(ModTestData))]
        [MemberData(nameof(ModTestData.ValidRandomModsWithTitles), MemberType=typeof(ModTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectTitles(Mod mod, List<ModTitle> expectedTitles)
        {
            var testMod = new Mod(mod);
            Assert.Equal(expectedTitles, testMod.Titles);
        }

        [Theory]
        [MemberData(nameof(ModTestData.ValidStaticModsWithReleases), MemberType=typeof(ModTestData))]
        [MemberData(nameof(ModTestData.ValidRandomModsWithReleases), MemberType=typeof(ModTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectReleases(Mod mod, List<Release> expectedReleases)
        {
            var testMod = new Mod(mod);
            Assert.Equal(expectedReleases, testMod.Releases);
        }

        [Fact]
        public void CopyConstructor_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Mod(null));
            Assert.Equal("original is required. (Parameter 'original')", exception.Message);
        }
        #endregion

        #region Individual Value Constructor
        [Theory]
        [MemberData(nameof(ModTestData.ValidStaticModsCreationProperties), MemberType=typeof(ModTestData))]
        [MemberData(nameof(ModTestData.ValidRandomModsCreationProperties), MemberType=typeof(ModTestData))]
        public void ModConstructor_WhenValidParameters_ReturnsCorrectName(String name, List<ModTitle> titles, List<Release> releases)
        {
            var testmod = new Mod(name, titles, releases);
            Assert.Equal(releases, testmod.Releases);
        }

        [Theory]
        [MemberData(nameof(ModTestData.ValidStaticModsCreationProperties), MemberType=typeof(ModTestData))]
        [MemberData(nameof(ModTestData.ValidRandomModsCreationProperties), MemberType=typeof(ModTestData))]
        public void ModConstructor_WhenValidParameters_ReturnsCorrectTitles(String name, List<ModTitle> titles, List<Release> releases)
        {
            var testmod = new Mod(name, titles, releases);
            Assert.Equal(titles, testmod.Titles);
        }

        [Theory]
        [MemberData(nameof(ModTestData.ValidStaticModsCreationProperties), MemberType=typeof(ModTestData))]
        [MemberData(nameof(ModTestData.ValidRandomModsCreationProperties), MemberType=typeof(ModTestData))]
        public void ModConstructor_WhenValidParameters_ReturnsCorrectReleases(String name, List<ModTitle> titles, List<Release> releases)
        {
            var testmod = new Mod(name, titles, releases);
            Assert.Equal(releases, testmod.Releases);
        }

        [Fact]
        public void ModConstructor_WhenNameIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Mod(
                Name: null,
                Titles: ModTitleTestData.TestModTitles,
                Releases: ReleaseTestData.TestModReleases));
            Assert.Equal("Name is required. (Parameter 'Name')", exception.Message);
        }

        [Theory]
        [MemberData(nameof(CommonTestData.EmptyAndWhitespaceStrings), MemberType=typeof(CommonTestData))]
        public void ModConstructor_WhenNameIsEmpty_ThrowsArgumentException(String modNameString)
        {
            var exception = Assert.Throws<ArgumentException>(() => new Mod(
                Name: modNameString,
                Titles: ModTitleTestData.TestModTitles,
                Releases: ReleaseTestData.TestModReleases));
            Assert.Equal("Name may not be empty. (Parameter 'Name')", exception.Message);
        }

        [Fact]
        public void ModConstructor_WhenNameIsTooLong_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Mod(
                Name: TestDataHelpers.GetRandomCharacterStringFromSet(Mod.ValidModNameCharacters, Mod.NameLength + 1),
                Titles: ModTitleTestData.TestModTitles,
                Releases: ReleaseTestData.TestModReleases));
            Assert.Equal($"Name must not exceed {Mod.NameLength.ToString()} characters. (Parameter 'Name')", exception.Message);
        }

        [Theory]
        [InlineData("`````")]
        [InlineData("$#@%")]
        [InlineData("?")]
        [InlineData("Test Mod - 943$")]
        [InlineData("What're you going to do, bleed on me?   I'm *INVINCIBLE*!!!")]
        public void ModConstructor_WhenNameIsInvalidFormat_ThrowsArgumentException(String modNameString)
        {
            var exception = Assert.Throws<ArgumentException>(() => new Mod(
                Name: modNameString,
                Titles: ModTitleTestData.TestModTitles,
                Releases: ReleaseTestData.TestModReleases));
            Assert.Equal($"Unable to parse \"{modNameString}\" to a valid mod name due to formatting. (Parameter 'Name')", exception.Message);
        }

        [Fact]
        public void ModConstructor_WhenTitlesIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Mod(
                Name: ModTestData.TestModTestDataPoint.Name,
                Titles: null,
                Releases: ReleaseTestData.TestModReleases));
            Assert.Equal("Titles is required. (Parameter 'Titles')", exception.Message);
        }

        [Fact]
        public void ModConstructor_WhenTitlesIsEmpty_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Mod(
                Name: ModTestData.TestModTestDataPoint.Name,
                Titles: new List<ModTitle>(),
                Releases: ReleaseTestData.TestModReleases));
            Assert.Equal("Titles must contain at least one entry. (Parameter 'Titles')", exception.Message);
        }

        [Fact]
        public void ModConstructor_WhenReleasesIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Mod(
                Name: ModTestData.TestModTestDataPoint.Name,
                Titles: ModTitleTestData.TestModTitles,
                Releases: null));
            Assert.Equal("Releases is required. (Parameter 'Releases')", exception.Message);
        }

        [Fact]
        public void ModConstructor_WhenReleasesIsEmpty_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Mod(
                Name: ModTestData.TestModTestDataPoint.Name,
                Titles: ModTitleTestData.TestModTitles,
                Releases: new List<Release>()));
            Assert.Equal("Releases must contain at least one entry. (Parameter 'Releases')", exception.Message);
        }

        [Fact]
        public void ModConstructor_WhenReleasesContainsReleaseDownloadUrlWithNonMatchingModName_ThrowsArgumentException()
        {
            ReleaseFileName releaseFileName = ReleaseFileName.For($"NotAMatch_{ModVersionTestData.TestModAlphaReleaseVersion}.zip");
            Release testRelease = new Release(
                ReleasedAt: ReleaseTestData.TestModAlphaReleaseDate,
                Sha1String: ReleaseTestData.TestModAlphaReleaseSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
                ReleaseFileName: releaseFileName,
                ModVersion: ModVersionTestData.TestModAlphaReleaseVersion,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: DependencyTestData.TestModAlphaReleaseDependencies
            );

            var exception = Assert.Throws<ArgumentException>(() => new Mod(
                Name: ModTestData.TestModTestDataPoint.Name,
                Titles: ModTitleTestData.TestModTitles,
                Releases: new List<Release> { testRelease, ReleaseTestData.TestModBetaRelease } ));
            Assert.Equal($"The mod name in the release filename \"{releaseFileName}\" does not match the specified mod name \"{ModTestData.TestModTestDataPoint.Name}\". (Parameter 'Releases')", exception.Message);
        }

        [Fact]
        public void ModConstructor_WhenReleasesContainsReleasFileNameWithNonMatchingModName_ThrowsArgumentException()
        {
            ReleaseDownloadUrl releaseDownloadUrl = ReleaseDownloadUrl.For($"/download/NotAMatch/{ReleaseDownloadUrlTestData.GenerateValidRandomizedReleaseDownloadUrlToken()}");
            Release testRelease = new Release(
                ReleasedAt: ReleaseTestData.TestModAlphaReleaseDate,
                Sha1String: ReleaseTestData.TestModAlphaReleaseSha1String,
                ReleaseDownloadUrl: releaseDownloadUrl,
                ReleaseFileName: ReleaseFileNameTestData.TestModAlphaReleaseFileName,
                ModVersion: ModVersionTestData.TestModAlphaReleaseVersion,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: DependencyTestData.TestModAlphaReleaseDependencies
            );

            var exception = Assert.Throws<ArgumentException>(() => new Mod(
                Name: ModTestData.TestModTestDataPoint.Name,
                Titles: ModTitleTestData.TestModTitles,
                Releases: new List<Release> { testRelease, ReleaseTestData.TestModBetaRelease } ));
            Assert.Equal($"The mod name in the release download URL \"{releaseDownloadUrl}\" does not match the specified mod name \"{ModTestData.TestModTestDataPoint.Name}\". (Parameter 'Releases')", exception.Message);
        }
        #endregion

        #region TryAddRelease
        [Fact]
        public void TryAddRelease_WhenGivenValidRelease_AddsReleaseToList()
        {
            Mod testMod = new Mod(
                Name: ModTestData.TestModTestDataPoint.Name,
                Titles: ModTitleTestData.TestModTitles,
                Releases: new List<Release>() { new Release(ReleaseTestData.TestModAlphaRelease) });
            testMod.TryAddRelease(ReleaseTestData.TestModBetaRelease);
            Assert.Contains(ReleaseTestData.TestModBetaRelease, testMod.Releases);
        }
        
        [Fact]
        public void TryAddRelease_WhenNewReleaseIsNull_ThrowsArgumentNullException()
        {
            Mod testMod = new Mod(ModTestData.TestMod);
            var exception = Assert.Throws<ArgumentNullException>(() => testMod.TryAddRelease(null));
            Assert.Equal("newRelease is required. (Parameter 'newRelease')", exception.Message);
        }

        [Fact]
        public void TryAddRelease_WhenNewReleaseModVersionAlreadyExists_ThrowsInvalidOperationException()
        {
            Mod testMod = new Mod(ModTestData.TestMod);
            var exception = Assert.Throws<InvalidOperationException>(() => ModTestData.TestMod.TryAddRelease(ReleaseTestData.TestModAlphaRelease));
            Assert.Equal("A release with the specified version already exists for this mod.", exception.Message);
        }

        [Fact]
        public void TryAddRelease_WhenNewReleaseDownloadUrlModNameDoesNotMatch_ThrowsInvalidOperationException()
        {
            ReleaseDownloadUrl releaseDownloadUrl = ReleaseDownloadUrl.For($"/download/NotAMatch/1234567890abcd1234567890");
            
            var exception = Assert.Throws<InvalidOperationException>(() => ModTestData.TestMod.TryAddRelease(new Release(
                ReleasedAt: ReleaseTestData.TestModGammaReleaseDate,
                Sha1String: ReleaseTestData.TestModGammaReleaseSha1String,
                ReleaseDownloadUrl: releaseDownloadUrl,
                ReleaseFileName: ReleaseFileNameTestData.TestModGammaReleaseFileName,
                ModVersion: ModVersionTestData.TestModGammaReleaseVersion,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: DependencyTestData.TestModGammaReleaseDependencies)));
            Assert.Equal("The specified download URL does not properly reference this mod.", exception.Message);
        }

        [Fact]
        public void TryAddRelease_WhenNewReleaseFileNameModNameDoesNotMatch_ThrowsInvalidOperationException()
        {
            ReleaseFileName releaseFileName = ReleaseFileName.For($"NotAMatch_{ModVersionTestData.TestModGammaReleaseVersion}.zip");

            var exception = Assert.Throws<InvalidOperationException>(() => ModTestData.TestMod.TryAddRelease(new Release(
                ReleasedAt: ReleaseTestData.TestModGammaReleaseDate,
                Sha1String: ReleaseTestData.TestModGammaReleaseSha1String,  
                ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
                ReleaseFileName: releaseFileName,
                ModVersion: ModVersionTestData.TestModGammaReleaseVersion,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: DependencyTestData.TestModGammaReleaseDependencies)));
            Assert.Equal("The specified release file name does not properly reference this mod.", exception.Message);
        } 
        #endregion

        #region TryRemoveRelease
        [Fact]
        public void TryRemoveRelease_WhenReleaseExists_RemovesReleaseAndReturnsTrue()
        {
            Mod testMod = new Mod(ModTestData.TestMod);
            Assert.True(testMod.TryRemoveRelease(ReleaseTestData.TestModBetaRelease));
            Assert.DoesNotContain(ReleaseTestData.TestModBetaRelease, testMod.Releases);
        }

        [Fact]
        public void TryRemoveRelease_WhenReleaseDoesNotExist_ReturnsFalse()
        {
            Mod testMod = new Mod(ModTestData.TestModTestDataPoint.Name, ModTitleTestData.TestModTitles, ReleaseTestData.TestModReleases);
            Assert.False(testMod.TryRemoveRelease(ReleaseTestData.BobsFunctionsLibraryFirstRelease));
        }

        [Fact]
        public void TryRemoveRelease_WhenOneReleaseLeft_ThrowsInvalidOperationException()
        {
            Mod testMod = new Mod(ModTestData.TestModTestDataPoint.Name, ModTitleTestData.TestModTitles, new List<Release>() { new Release(ReleaseTestData.TestModBetaRelease) });
            var exception = Assert.Throws<InvalidOperationException>(() => testMod.TryRemoveRelease(new Release(ReleaseTestData.TestModBetaRelease)));
            Assert.Equal("A mod must always have at least one release. You may not remove the last release from a mod's list of releases.", exception.Message);
        }
        #endregion

        #region GetLatestRelease
        [Fact]
        public void GetLatestRelease_WhenInvoked_ReturnsMostRecentRelease()
        {
            Assert.Equal(ReleaseTestData.TestModBetaRelease, ModTestData.TestMod.GetLatestRelease());
        }
        #endregion

        #region TryAddTitle
        [Fact]
        public void TryAddTitle_WhenGivenValidModTitle_AddsModTitleToList()
        {
            Mod testMod = new Mod(ModTestData.TestModTestDataPoint.Name, new List<ModTitle> { ModTitleTestData.TestModEnglishTitle }, ReleaseTestData.TestModReleases);
            testMod.TryAddTitle(ModTitleTestData.TestModGermanTitle);
            Assert.Contains(ModTitleTestData.TestModGermanTitle, testMod.Titles);
        }
        
        [Fact]
        public void TryAddTitle_WhenNewModTitleIsNull_ThrowsArgumentNullException()
        {
            Mod testMod = new Mod(ModTestData.TestModTestDataPoint.Name, ModTitleTestData.TestModTitles, ReleaseTestData.TestModReleases);
            var exception = Assert.Throws<ArgumentNullException>(() => testMod.TryAddTitle(null));
            Assert.Equal("newModTitle is required. (Parameter 'newModTitle')", exception.Message);
        }

        [Fact]
        public void TryAddTitle_WhenNewModTitleLanguageAlreadyExists_ThrowsInvalidOperationException()
        {
            Mod testMod = new Mod(ModTestData.TestModTestDataPoint.Name, ModTitleTestData.TestModTitles, ReleaseTestData.TestModReleases);
            var exception = Assert.Throws<InvalidOperationException>(() => testMod.TryAddTitle(new ModTitle(LanguageTestData.GermanWithId, "Übungsmod")));
            Assert.Equal("A title with the specified language already exists for this mod.", exception.Message);
        }
        #endregion

        #region TryRemoveTitle
        [Fact]
        public void TryRemoveTitle_WhenTitleExists_RemovesReleaseAndReturnsTrue()
        {
            Assert.True(ModTestData.TestMod.TryRemoveTitle(ModTitleTestData.TestModGermanTitle));
            Assert.DoesNotContain(ModTitleTestData.TestModGermanTitle, ModTestData.TestMod.Titles);
        }

        [Fact]
        public void TryRemoveTitle_WhenTitleDoesNotExist_ReturnsFalse()
        {
            Mod testMod = new Mod(ModTestData.TestModTestDataPoint.Name, ModTitleTestData.TestModTitles, ReleaseTestData.TestModReleases);
            Assert.False(testMod.TryRemoveTitle(new ModTitle(LanguageTestData.GermanWithId, "Übungsmod")));
        }

        [Fact]
        public void TryRemoveTitle_WhenOneTitleLeft_ThrowsInvalidOperationException()
        {
            Mod testMod = new Mod(ModTestData.TestModTestDataPoint.Name, new List<ModTitle> { ModTitleTestData.TestModEnglishTitle }, ReleaseTestData.TestModReleases);
            var exception = Assert.Throws<InvalidOperationException>(() => testMod.TryRemoveTitle(ModTitleTestData.TestModEnglishTitle));
            Assert.Equal("A mod must always have at least one title. You may not remove the last title from a mod's list of titles.", exception.Message);
        }
        #endregion

        #region Equals
        [Theory]
        [MemberData(nameof(ModTestData.ValidStaticEqualModPairs), MemberType=typeof(ModTestData))]
        [MemberData(nameof(ModTestData.ValidRandomEqualModPairs), MemberType=typeof(ModTestData))]
        public void Equals_WhenProvidedEqualMods_ReturnsTrue(Mod left, Mod right)
        {
            Assert.True(left.Equals(right));
        }

        [Theory]
        [MemberData(nameof(ModTestData.ValidStaticNonEqualModPairs), MemberType=typeof(ModTestData))]
        public void Equals_WhenProvidedNotEqualMods_ReturnsFalse(Mod left, Mod right)
        {
            Assert.False(left.Equals(right));
        }

        [Fact]
        public void Equals_WhenGivenNull_ReturnsFalse()
        {
            Assert.False(ModTestData.TestMod.Equals(null));
        }
        #endregion
    }
}
