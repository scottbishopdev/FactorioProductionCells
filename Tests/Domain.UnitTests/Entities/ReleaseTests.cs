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
    public class ReleaseTests
    {
        #region Copy Constructor
        [Theory]
        [MemberData(nameof(ReleaseTestData.ValidStaticReleasesWithReleasedAt), MemberType=typeof(ReleaseTestData))]
        [MemberData(nameof(ReleaseTestData.ValidRandomReleasesWithReleasedAt), MemberType=typeof(ReleaseTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectReleasedAt(Release release, DateTime expectedReleasedAt)
        {
            var testRelease = new Release(release);
            Assert.Equal(expectedReleasedAt, testRelease.ReleasedAt);
        }

        [Theory]
        [MemberData(nameof(ReleaseTestData.ValidStaticReleasesWithSha1), MemberType=typeof(ReleaseTestData))]
        [MemberData(nameof(ReleaseTestData.ValidRandomReleasesWithSha1), MemberType=typeof(ReleaseTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectSha1(Release release, String expectedSha1)
        {
            var testRelease = new Release(release);
            Assert.Equal(expectedSha1, testRelease.Sha1);
        }

        [Theory]
        [MemberData(nameof(ReleaseTestData.ValidStaticReleasesWithModVersion), MemberType=typeof(ReleaseTestData))]
        [MemberData(nameof(ReleaseTestData.ValidRandomReleasesWithModVersion), MemberType=typeof(ReleaseTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectModVersion(Release release, ModVersion expectedModVersion)
        {
            var testRelease = new Release(release);
            Assert.Equal(expectedModVersion, testRelease.ModVersion);
        }

        [Theory]
        [MemberData(nameof(ReleaseTestData.ValidStaticReleasesWithFactorioVersion), MemberType=typeof(ReleaseTestData))]
        [MemberData(nameof(ReleaseTestData.ValidRandomReleasesWithFactorioVersion), MemberType=typeof(ReleaseTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectFactorioVersion(Release release, FactorioVersion expectedFactorioVersion)
        {
            var testRelease = new Release(release);
            Assert.Equal(expectedFactorioVersion, testRelease.FactorioVersion);
        }
        
        [Theory]
        [MemberData(nameof(ReleaseTestData.ValidStaticReleasesWithReleaseDownloadUrl), MemberType=typeof(ReleaseTestData))]
        [MemberData(nameof(ReleaseTestData.ValidRandomReleasesWithReleaseDownloadUrl), MemberType=typeof(ReleaseTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectReleaseDownloadUrl(Release release, ReleaseDownloadUrl expectedReleaseDownloadUrl)
        {
            var testRelease = new Release(release);
            Assert.Equal(expectedReleaseDownloadUrl, testRelease.ReleaseDownloadUrl);
        }

        [Theory]
        [MemberData(nameof(ReleaseTestData.ValidStaticReleasesWithReleaseFileName), MemberType=typeof(ReleaseTestData))]
        [MemberData(nameof(ReleaseTestData.ValidRandomReleasesWithReleaseFileName), MemberType=typeof(ReleaseTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectReleaseFileName(Release release, ReleaseFileName expectedReleaseFileName)
        {
            var testRelease = new Release(release);
            Assert.Equal(expectedReleaseFileName, testRelease.ReleaseFileName);
        }

        [Theory]
        [MemberData(nameof(ReleaseTestData.ValidStaticReleasesWithDependencies), MemberType=typeof(ReleaseTestData))]
        [MemberData(nameof(ReleaseTestData.ValidRandomReleasesWithDependencies), MemberType=typeof(ReleaseTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectDependencies(Release release, List<Dependency> expectedDependencies)
        {
            var testRelease = new Release(release);
            Assert.Equal(expectedDependencies, testRelease.Dependencies);
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
        [MemberData(nameof(ReleaseTestData.ValidStaticReleasesCreationProperties), MemberType=typeof(ReleaseTestData))]
        [MemberData(nameof(ReleaseTestData.ValidRandomReleasesCreationProperties), MemberType=typeof(ReleaseTestData))]
        public void ReleaseConstructor_WhenValidParameters_ReturnsCorrectReleasedAt(DateTime releasedAt, String sha1String, ReleaseDownloadUrl releaseDownloadUrl, ReleaseFileName releaseFileName, ModVersion modVersion, FactorioVersion factorioVersion, List<Dependency> dependencies)
        {
            var testRelease = new Release(releasedAt, sha1String, releaseDownloadUrl, releaseFileName, modVersion, factorioVersion, dependencies);
            Assert.Equal(releasedAt, testRelease.ReleasedAt);
        }

        [Theory]
        [MemberData(nameof(ReleaseTestData.ValidStaticReleasesCreationProperties), MemberType=typeof(ReleaseTestData))]
        [MemberData(nameof(ReleaseTestData.ValidRandomReleasesCreationProperties), MemberType=typeof(ReleaseTestData))]
        public void ReleaseConstructor_WhenValidParameters_ReturnsCorrectSha1(DateTime releasedAt, String sha1String, ReleaseDownloadUrl releaseDownloadUrl, ReleaseFileName releaseFileName, ModVersion modVersion, FactorioVersion factorioVersion, List<Dependency> dependencies)
        {
            var testRelease = new Release(releasedAt, sha1String, releaseDownloadUrl, releaseFileName, modVersion, factorioVersion, dependencies);
            Assert.Equal(sha1String, testRelease.Sha1);
        }

        [Theory]
        [MemberData(nameof(ReleaseTestData.ValidStaticReleasesCreationProperties), MemberType=typeof(ReleaseTestData))]
        [MemberData(nameof(ReleaseTestData.ValidRandomReleasesCreationProperties), MemberType=typeof(ReleaseTestData))]
        public void ReleaseConstructor_WhenValidParameters_ReturnsCorrectModVersion(DateTime releasedAt, String sha1String, ReleaseDownloadUrl releaseDownloadUrl, ReleaseFileName releaseFileName, ModVersion modVersion, FactorioVersion factorioVersion, List<Dependency> dependencies)
        {
            var testRelease = new Release(releasedAt, sha1String, releaseDownloadUrl, releaseFileName, modVersion, factorioVersion, dependencies);
            Assert.Equal(modVersion, testRelease.ModVersion);
        }

        [Theory]
        [MemberData(nameof(ReleaseTestData.ValidStaticReleasesCreationProperties), MemberType=typeof(ReleaseTestData))]
        [MemberData(nameof(ReleaseTestData.ValidRandomReleasesCreationProperties), MemberType=typeof(ReleaseTestData))]
        public void ReleaseConstructor_WhenValidParameters_ReturnsCorrectFactorioVersion(DateTime releasedAt, String sha1String, ReleaseDownloadUrl releaseDownloadUrl, ReleaseFileName releaseFileName, ModVersion modVersion, FactorioVersion factorioVersion, List<Dependency> dependencies)
        {
            var testRelease = new Release(releasedAt, sha1String, releaseDownloadUrl, releaseFileName, modVersion, factorioVersion, dependencies);
            Assert.Equal(factorioVersion, testRelease.FactorioVersion);
        }

        [Theory]
        [MemberData(nameof(ReleaseTestData.ValidStaticReleasesCreationProperties), MemberType=typeof(ReleaseTestData))]
        [MemberData(nameof(ReleaseTestData.ValidRandomReleasesCreationProperties), MemberType=typeof(ReleaseTestData))]
        public void ReleaseConstructor_WhenValidParameters_ReturnsCorrectReleaseDownloadUrl(DateTime releasedAt, String sha1String, ReleaseDownloadUrl releaseDownloadUrl, ReleaseFileName releaseFileName, ModVersion modVersion, FactorioVersion factorioVersion, List<Dependency> dependencies)
        {
            var testRelease = new Release(releasedAt, sha1String, releaseDownloadUrl, releaseFileName, modVersion, factorioVersion, dependencies);
            Assert.Equal(releaseDownloadUrl, testRelease.ReleaseDownloadUrl);
        }

        [Theory]
        [MemberData(nameof(ReleaseTestData.ValidStaticReleasesCreationProperties), MemberType=typeof(ReleaseTestData))]
        [MemberData(nameof(ReleaseTestData.ValidRandomReleasesCreationProperties), MemberType=typeof(ReleaseTestData))]
        public void ReleaseConstructor_WhenValidParameters_ReturnsCorrectReleaseFileName(DateTime releasedAt, String sha1String, ReleaseDownloadUrl releaseDownloadUrl, ReleaseFileName releaseFileName, ModVersion modVersion, FactorioVersion factorioVersion, List<Dependency> dependencies)
        {
            var testRelease = new Release(releasedAt, sha1String, releaseDownloadUrl, releaseFileName, modVersion, factorioVersion, dependencies);
            Assert.Equal(releaseFileName, testRelease.ReleaseFileName);
        }

        [Theory]
        [MemberData(nameof(ReleaseTestData.ValidStaticReleasesCreationProperties), MemberType=typeof(ReleaseTestData))]
        [MemberData(nameof(ReleaseTestData.ValidRandomReleasesCreationProperties), MemberType=typeof(ReleaseTestData))]
        public void ReleaseConstructor_WhenValidParameters_ReturnsCorrectDependencies(DateTime releasedAt, String sha1String, ReleaseDownloadUrl releaseDownloadUrl, ReleaseFileName releaseFileName, ModVersion modVersion, FactorioVersion factorioVersion, List<Dependency> dependencies)
        {
            var testRelease = new Release(releasedAt, sha1String, releaseDownloadUrl, releaseFileName, modVersion, factorioVersion, dependencies);
            Assert.Equal(dependencies, testRelease.Dependencies);
        }

        [Fact]
        public void ReleaseConstructor_WhenReleasedAtIsInFuture_ThrowsArgumentOutOfRangeException()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Release(
                ReleasedAt: DateTime.MaxValue,
                Sha1String: ReleaseTestData.TestModAlphaReleaseSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTestData.TestModAlphaReleaseFileName,
                ModVersion: ModVersionTestData.TestModAlphaReleaseVersion,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: DependencyTestData.TestModAlphaReleaseDependencies));
            Assert.Equal("ReleasedAt must be set to a time in the past. (Parameter 'ReleasedAt')", exception.Message);
        }

        [Fact]
        public void ReleaseConstructor_WhenSha1StringIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Release(
                ReleasedAt: ReleaseTestData.TestModAlphaReleaseDate,
                Sha1String: null,
                ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTestData.TestModAlphaReleaseFileName,
                ModVersion: ModVersionTestData.TestModAlphaReleaseVersion,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: DependencyTestData.TestModAlphaReleaseDependencies));
            Assert.Equal("Sha1String is required. (Parameter 'Sha1String')", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("af742436d50c6b0eecZZ2257c19a090b7eda5232")]
        [InlineData("af742436d50c6!=eec7f2257c19a090b7eda5232")]
        [InlineData("af742436d50c6b0eec  7f2257c19a090b7eda5232")]
        [InlineData("What're you going to do, bleed on me?   I'm *INVINCIBLE*!!!")]
        public void ReleaseConstructor_WhenSha1IsInvalidFormat_ThrowsArgumentException(String sha1String)
        {
            var exception = Assert.Throws<ArgumentException>(() => new Release(
                ReleasedAt: ReleaseTestData.TestModAlphaReleaseDate,
                Sha1String: sha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTestData.TestModAlphaReleaseFileName,
                ModVersion: ModVersionTestData.TestModAlphaReleaseVersion,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: DependencyTestData.TestModAlphaReleaseDependencies));
            Assert.Equal($"Unable to parse \"{sha1String}\" to a valid SHA-1 hash due to formatting. (Parameter 'Sha1String')", exception.Message);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("af742436d50c6b0eec7f2257c19a090b7eda523")]
        [InlineData("af742436d50c6b0eec7f2257c19a090b7eda5232b")]
        [InlineData("af742436d50c6b0eec7f2257c19a090b7eda5232b747891299364667bbceaa99f01")]
        public void ReleaseConstructor_WhenSha1IsInvalidLength_ThrowsArgumentException(String sha1String)
        {
            var exception = Assert.Throws<ArgumentException>(() => new Release(
                ReleasedAt: ReleaseTestData.TestModAlphaReleaseDate,
                Sha1String: sha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTestData.TestModAlphaReleaseFileName,
                ModVersion: ModVersionTestData.TestModAlphaReleaseVersion,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: DependencyTestData.TestModAlphaReleaseDependencies));
            Assert.Equal($"Unable to parse \"{sha1String}\" to a valid SHA-1 hash due to length. The SHA-1 hash must have a length of exactly {Release.Sha1Length} characters. (Parameter 'Sha1String')", exception.Message);
        }
        
        [Fact]
        public void ReleaseConstructor_WhenReleaseDownloadUrlIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Release(
                ReleasedAt: ReleaseTestData.TestModAlphaReleaseDate,
                Sha1String: ReleaseTestData.TestModAlphaReleaseSha1String,
                ReleaseDownloadUrl: null,
                ReleaseFileName: ReleaseFileNameTestData.TestModAlphaReleaseFileName,
                ModVersion: ModVersionTestData.TestModAlphaReleaseVersion,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: DependencyTestData.TestModAlphaReleaseDependencies));
            Assert.Equal("ReleaseDownloadUrl is required. (Parameter 'ReleaseDownloadUrl')", exception.Message);
        }

        [Fact]
        public void ReleaseConstructor_WhenReleaseFileNameIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Release(
                ReleasedAt: ReleaseTestData.TestModAlphaReleaseDate,
                Sha1String: ReleaseTestData.TestModAlphaReleaseSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
                ReleaseFileName: null,
                ModVersion: ModVersionTestData.TestModAlphaReleaseVersion,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: DependencyTestData.TestModAlphaReleaseDependencies));
            Assert.Equal("ReleaseFileName is required. (Parameter 'ReleaseFileName')", exception.Message);
        }

        [Fact]
        public void ReleaseConstructor_WhenModVersionIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Release(
                ReleasedAt: ReleaseTestData.TestModAlphaReleaseDate,
                Sha1String: ReleaseTestData.TestModAlphaReleaseSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTestData.TestModAlphaReleaseFileName,
                ModVersion: null,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: DependencyTestData.TestModAlphaReleaseDependencies));
            Assert.Equal("ModVersion is required. (Parameter 'ModVersion')", exception.Message);
        }

        [Fact]
        public void ReleaseConstructor_WhenFactorioVersionIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Release(
                ReleasedAt: ReleaseTestData.TestModAlphaReleaseDate,
                Sha1String: ReleaseTestData.TestModAlphaReleaseSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTestData.TestModAlphaReleaseFileName,
                ModVersion: ModVersionTestData.TestModAlphaReleaseVersion,
                FactorioVersion: null,
                Dependencies: DependencyTestData.TestModAlphaReleaseDependencies));
            Assert.Equal("FactorioVersion is required. (Parameter 'FactorioVersion')", exception.Message);
        }

        [Fact]
        public void ReleaseConstructor_WhenDependenciesIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Release(
                ReleasedAt: ReleaseTestData.TestModAlphaReleaseDate,
                Sha1String: ReleaseTestData.TestModAlphaReleaseSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTestData.TestModAlphaReleaseFileName,
                ModVersion: ModVersionTestData.TestModAlphaReleaseVersion,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: null));
            Assert.Equal("Dependencies is required. (Parameter 'Dependencies')", exception.Message);
        }

        [Fact]
        public void ReleaseConstructor_WhenDependenciesListIsEmpty_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Release(
                ReleasedAt: ReleaseTestData.TestModAlphaReleaseDate,
                Sha1String: ReleaseTestData.TestModAlphaReleaseSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTestData.TestModAlphaReleaseFileName,
                ModVersion: ModVersionTestData.TestModAlphaReleaseVersion,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: new List<Dependency>()));
            Assert.Equal("Dependencies must contain at least one entry. (Parameter 'Dependencies')", exception.Message);
        }

        [Fact]
        public void ReleaseConstructor_WhenModVersionDoesNotMatchReleaseFileNameVersion_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Release(
                ReleasedAt: ReleaseTestData.TestModAlphaReleaseDate,
                Sha1String: ReleaseTestData.TestModAlphaReleaseSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTestData.TestModAlphaReleaseFileName,
                ModVersion: ModVersionTestData.ZeroPointFourPointThree,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: DependencyTestData.TestModAlphaReleaseDependencies));
            Assert.Equal("The specified release file name version does not match the specified release version. (Parameter 'ReleaseFileName')", exception.Message);
        }
        #endregion

        #region CompareTo
        [Theory]
        [MemberData(nameof(ReleaseTestData.Comparison_LeftLowerData), MemberType=typeof(ReleaseTestData))]
        public void CompareTo_WhenProvidedMoreRecentRelease_ReturnsNegativeOne(DateTime left, DateTime right)
        {
            Release leftRelease = new Release(
                ReleasedAt: left,
                Sha1String: ReleaseTestData.TestModAlphaReleaseSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTestData.TestModAlphaReleaseFileName,
                ModVersion: ModVersionTestData.TestModAlphaReleaseVersion,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: DependencyTestData.TestModAlphaReleaseDependencies);
            
            Release rightRelease = new Release(
                ReleasedAt: right,
                Sha1String: ReleaseTestData.TestModAlphaReleaseSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTestData.TestModAlphaReleaseFileName,
                ModVersion: ModVersionTestData.TestModAlphaReleaseVersion,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: DependencyTestData.TestModAlphaReleaseDependencies);

            Assert.Equal(-1, leftRelease.CompareTo(rightRelease));
        }

        [Theory]
        [MemberData(nameof(ReleaseTestData.Comparison_EqualData), MemberType=typeof(ReleaseTestData))]
        public void CompareTo_WhenProvidedEqualRelease_ReturnsZero(DateTime left, DateTime right)
        {
            Release leftRelease = new Release(
                ReleasedAt: left,
                Sha1String: ReleaseTestData.TestModAlphaReleaseSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTestData.TestModAlphaReleaseFileName,
                ModVersion: ModVersionTestData.TestModAlphaReleaseVersion,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: DependencyTestData.TestModAlphaReleaseDependencies);
            
            Release rightRelease = new Release(
                ReleasedAt: right,
                Sha1String: ReleaseTestData.TestModAlphaReleaseSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTestData.TestModAlphaReleaseFileName,
                ModVersion: ModVersionTestData.TestModAlphaReleaseVersion,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: DependencyTestData.TestModAlphaReleaseDependencies);

            Assert.Equal(0, leftRelease.CompareTo(rightRelease));
        }

        [Theory]
        [MemberData(nameof(ReleaseTestData.Comparison_RightLowerData), MemberType=typeof(ReleaseTestData))]
        public void CompareTo_WhenProvidedOlderRelease_ReturnsOne(DateTime left, DateTime right)
        {
            Release leftRelease = new Release(
                ReleasedAt: left,
                Sha1String: ReleaseTestData.TestModAlphaReleaseSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTestData.TestModAlphaReleaseFileName,
                ModVersion: ModVersionTestData.TestModAlphaReleaseVersion,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: DependencyTestData.TestModAlphaReleaseDependencies);
            
            Release rightRelease = new Release(
                ReleasedAt: right,
                Sha1String: ReleaseTestData.TestModAlphaReleaseSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTestData.TestModAlphaReleaseFileName,
                ModVersion: ModVersionTestData.TestModAlphaReleaseVersion,
                FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
                Dependencies: DependencyTestData.TestModAlphaReleaseDependencies);

            Assert.Equal(1, leftRelease.CompareTo(rightRelease));
        }
        
        [Theory]
        [MemberData(nameof(CommonTestData.VariousValueTypeData), MemberType=typeof(CommonTestData))]
        public void CompareTo_WhenProvidedDifferentType_ThrowsArgumentException(Object right)
        {
            var exception = Assert.Throws<ArgumentException>(() => ReleaseTestData.TestModAlphaRelease.CompareTo(right));
            Assert.Equal("The specified object to compare is not a Release. (Parameter 'obj')", exception.Message);
        }
        #endregion

        #region Equals
        [Theory]
        [MemberData(nameof(ReleaseTestData.ValidStaticEqualReleasePairs), MemberType=typeof(ReleaseTestData))]
        [MemberData(nameof(ReleaseTestData.ValidRandomEqualReleasePairs), MemberType=typeof(ReleaseTestData))]
        public void Equals_WhenProvidedEqualReleases_ReturnsTrue(Release left, Release right)
        {
            Assert.True(left.Equals(right));
        }

        [Theory]
        [MemberData(nameof(ReleaseTestData.ValidStaticNonEqualReleasePairs), MemberType=typeof(ReleaseTestData))]
        public void Equals_WhenProvidedNotEqualReleases_ReturnsFalse(Release left, Release right)
        {
            Assert.False(left.Equals(right));
        }

        [Fact]
        public void Equals_WhenGivenNull_ReturnsFalse()
        {
            Assert.False(ReleaseTestData.TestModAlphaRelease.Equals(null));
        }
        #endregion
    }
}
