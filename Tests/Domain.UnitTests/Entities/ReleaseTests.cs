using System;
using System.Collections.Generic;
using Xunit;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.ValueObjects;
using FactorioProductionCells.Domain.UnitTests.ValueObjects;

namespace FactorioProductionCells.Domain.UnitTests.Entities
{
    public class ReleaseTests
    {
        internal static DateTime OnePointFourPointSixteenReleasedAt = DateTime.Parse("6/1/2020 3:24:52 AM");
        internal static DateTime OnePointFivePointOneReleasedAt = DateTime.Parse("7/3/2020 6:12:03 PM");
        internal static String OnePointFourPointSixteenSha1String = "b53320182d7e53b81b1009d8353492614f9a5cac";
        internal static String OnePointFiveSha1String = "4f6cf41a4a419c601580f967603f0d3977dab0b7";
        internal static List<Dependency> TestModReleaseDependencies = new List<Dependency> { DependencyTests.TestModBaseDependencyFromFor, DependencyTests.TestModAngelsConfigDependencyFromFor };

        internal static Release TestModReleaseOnePointFourPointSixteen = new Release(
                ReleasedAt: ReleaseTests.OnePointFourPointSixteenReleasedAt,
                Sha1String: ReleaseTests.OnePointFourPointSixteenSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTests.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTests.OnePointFourPointSixteenReleaseFileName,
                ModVersion: ModVersionTests.OnePointFourPointSixteen,
                FactorioVersion: FactorioVersionTests.ZeroPointSeventeen,
                Dependencies: TestModReleaseDependencies);

        internal static Release TestModReleaseOnePointFourPointSixteenClone = new Release(
                ReleasedAt: ReleaseTests.OnePointFourPointSixteenReleasedAt,
                Sha1String: ReleaseTests.OnePointFourPointSixteenSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTests.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTests.OnePointFourPointSixteenReleaseFileName,
                ModVersion: ModVersionTests.OnePointFourPointSixteen,
                FactorioVersion: FactorioVersionTests.ZeroPointSeventeen,
                Dependencies: TestModReleaseDependencies);

        internal static Release TestModReleaseOnePointFivePointOne = new Release(
                ReleasedAt: ReleaseTests.OnePointFivePointOneReleasedAt,
                Sha1String: ReleaseTests.OnePointFiveSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTests.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTests.OnePointFivePointOneReleaseFileName,
                ModVersion: ModVersionTests.OnePointFivePointOne,
                FactorioVersion: FactorioVersionTests.ZeroPointEighteen,
                Dependencies: TestModReleaseDependencies);

        internal static Release TestModYearOldRelease = new Release(
                ReleasedAt: DateTime.Parse("6/1/2019 3:24:52 AM"),
                Sha1String: ReleaseTests.OnePointFourPointSixteenSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTests.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTests.OnePointFourPointSixteenReleaseFileName,
                ModVersion: ModVersionTests.OnePointFourPointSixteen,
                FactorioVersion: FactorioVersionTests.ZeroPointSeventeen,
                Dependencies: TestModReleaseDependencies);

        #region ReleaseConstructor
        [Fact]
        public void ReleaseConstructor_WhenValidParameters_ReturnsCorrectReleasedAt()
        {
            Assert.Equal(OnePointFourPointSixteenReleasedAt, TestModReleaseOnePointFourPointSixteen.ReleasedAt);
        }

        [Fact]
        public void ReleaseConstructor_WhenValidParameters_ReturnsCorrectReleaseDownloadUrl()
        {
            Assert.Equal(ReleaseDownloadUrlTests.TestModDownloadUrl, TestModReleaseOnePointFourPointSixteen.ReleaseDownloadUrl);
        }

        [Fact]
        public void ReleaseConstructor_WhenValidParameters_ReturnsCorrectReleaseFileName()
        {
            Assert.Equal(ReleaseFileNameTests.OnePointFourPointSixteenReleaseFileName, TestModReleaseOnePointFourPointSixteen.ReleaseFileName);
        }

        [Fact]
        public void ReleaseConstructor_WhenValidParameters_ReturnsCorrectSha1String()
        {
            Assert.Equal(OnePointFourPointSixteenSha1String, TestModReleaseOnePointFourPointSixteen.Sha1);
        }

        [Fact]
        public void ReleaseConstructor_WhenValidParameters_ReturnsCorrectModVersion()
        {
            Assert.Equal(ModVersionTests.OnePointFourPointSixteen, TestModReleaseOnePointFourPointSixteen.ModVersion);
        }

        [Fact]
        public void ReleaseConstructor_WhenValidParameters_ReturnsCorrectFactorioVersion()
        {
            Assert.Equal(FactorioVersionTests.ZeroPointSeventeen, TestModReleaseOnePointFourPointSixteen.FactorioVersion);
        }

        [Fact]
        public void ReleaseConstructor_WhenValidParameters_ReturnsCorrectDependencies()
        {
            Assert.Equal(TestModReleaseDependencies, TestModReleaseOnePointFourPointSixteen.Dependencies);
        }

        [Fact]
        public void ReleaseConstructor_WhenReleasedAtIsInFuture_ThrowsArgumentOutOfRangeException()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Release(
                ReleasedAt: DateTime.MaxValue,
                Sha1String: ReleaseTests.OnePointFourPointSixteenSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTests.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTests.OnePointFourPointSixteenReleaseFileName,
                ModVersion: ModVersionTests.OnePointFourPointSixteen,
                FactorioVersion: FactorioVersionTests.ZeroPointSeventeen,
                Dependencies: TestModReleaseDependencies));
            Assert.Equal("ReleasedAt must be set to a time in the past. (Parameter 'ReleasedAt')", exception.Message);
        }

        [Fact]
        public void ReleaseConstructor_WhenSha1StringIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Release(
                ReleasedAt: ReleaseTests.OnePointFourPointSixteenReleasedAt,
                Sha1String: null,
                ReleaseDownloadUrl: ReleaseDownloadUrlTests.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTests.OnePointFourPointSixteenReleaseFileName,
                ModVersion: ModVersionTests.OnePointFourPointSixteen,
                FactorioVersion: FactorioVersionTests.ZeroPointSeventeen,
                Dependencies: TestModReleaseDependencies));
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
                ReleasedAt: ReleaseTests.OnePointFourPointSixteenReleasedAt,
                Sha1String: sha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTests.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTests.OnePointFourPointSixteenReleaseFileName,
                ModVersion: ModVersionTests.OnePointFourPointSixteen,
                FactorioVersion: FactorioVersionTests.ZeroPointSeventeen,
                Dependencies: TestModReleaseDependencies));
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
                ReleasedAt: ReleaseTests.OnePointFourPointSixteenReleasedAt,
                Sha1String: sha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTests.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTests.OnePointFourPointSixteenReleaseFileName,
                ModVersion: ModVersionTests.OnePointFourPointSixteen,
                FactorioVersion: FactorioVersionTests.ZeroPointSeventeen,
                Dependencies: TestModReleaseDependencies));
            Assert.Equal($"Unable to parse \"{sha1String}\" to a valid SHA-1 hash due to length. The SHA-1 hash must have a length of exactly {Release.Sha1Length} characters. (Parameter 'Sha1String')", exception.Message);
        }
        
        [Fact]
        public void ReleaseConstructor_WhenReleaseDownloadUrlIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Release(
                ReleasedAt: ReleaseTests.OnePointFourPointSixteenReleasedAt,
                Sha1String: ReleaseTests.OnePointFourPointSixteenSha1String,
                ReleaseDownloadUrl: null,
                ReleaseFileName: ReleaseFileNameTests.OnePointFourPointSixteenReleaseFileName,
                ModVersion: ModVersionTests.OnePointFourPointSixteen,
                FactorioVersion: FactorioVersionTests.ZeroPointSeventeen,
                Dependencies: TestModReleaseDependencies));
            Assert.Equal("ReleaseDownloadUrl is required. (Parameter 'ReleaseDownloadUrl')", exception.Message);
        }

        [Fact]
        public void ReleaseConstructor_WhenReleaseFileNameIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Release(
                ReleasedAt: ReleaseTests.OnePointFourPointSixteenReleasedAt,
                Sha1String: ReleaseTests.OnePointFourPointSixteenSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTests.TestModDownloadUrl,
                ReleaseFileName: null,
                ModVersion: ModVersionTests.OnePointFourPointSixteen,
                FactorioVersion: FactorioVersionTests.ZeroPointSeventeen,
                Dependencies: TestModReleaseDependencies));
            Assert.Equal("ReleaseFileName is required. (Parameter 'ReleaseFileName')", exception.Message);
        }

        [Fact]
        public void ReleaseConstructor_WhenModVersionIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Release(
                ReleasedAt: ReleaseTests.OnePointFourPointSixteenReleasedAt,
                Sha1String: ReleaseTests.OnePointFourPointSixteenSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTests.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTests.OnePointFourPointSixteenReleaseFileName,
                ModVersion: null,
                FactorioVersion: FactorioVersionTests.ZeroPointSeventeen,
                Dependencies: TestModReleaseDependencies));
            Assert.Equal("ModVersion is required. (Parameter 'ModVersion')", exception.Message);
        }

        [Fact]
        public void ReleaseConstructor_WhenFactorioVersionIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Release(
                ReleasedAt: ReleaseTests.OnePointFourPointSixteenReleasedAt,
                Sha1String: ReleaseTests.OnePointFourPointSixteenSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTests.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTests.OnePointFourPointSixteenReleaseFileName,
                ModVersion: ModVersionTests.OnePointFourPointSixteen,
                FactorioVersion: null,
                Dependencies: TestModReleaseDependencies));
            Assert.Equal("FactorioVersion is required. (Parameter 'FactorioVersion')", exception.Message);
        }

        [Fact]
        public void ReleaseConstructor_WhenDependenciesIsNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Release(
                ReleasedAt: ReleaseTests.OnePointFourPointSixteenReleasedAt,
                Sha1String: ReleaseTests.OnePointFourPointSixteenSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTests.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTests.OnePointFourPointSixteenReleaseFileName,
                ModVersion: ModVersionTests.OnePointFourPointSixteen,
                FactorioVersion: FactorioVersionTests.ZeroPointSeventeen,
                Dependencies: null));
            Assert.Equal("Dependencies is required. (Parameter 'Dependencies')", exception.Message);
        }

        [Fact]
        public void ReleaseConstructor_WhenDependenciesListIsEmpty_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Release(
                ReleasedAt: ReleaseTests.OnePointFourPointSixteenReleasedAt,
                Sha1String: ReleaseTests.OnePointFourPointSixteenSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTests.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTests.OnePointFourPointSixteenReleaseFileName,
                ModVersion: ModVersionTests.OnePointFourPointSixteen,
                FactorioVersion: FactorioVersionTests.ZeroPointSeventeen,
                Dependencies: new List<Dependency>()));
            Assert.Equal("Dependencies must contain at least one entry. (Parameter 'Dependencies')", exception.Message);
        }

        [Fact]
        public void ReleaseConstructor_WhenModVersionDoesNotMatchReleaseFileNameVersion_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Release(
                ReleasedAt: ReleaseTests.OnePointFourPointSixteenReleasedAt,
                Sha1String: ReleaseTests.OnePointFourPointSixteenSha1String,
                ReleaseDownloadUrl: ReleaseDownloadUrlTests.TestModDownloadUrl,
                ReleaseFileName: ReleaseFileNameTests.OnePointFourPointSixteenReleaseFileName,
                ModVersion: ModVersionTests.ZeroPointFourPointThree,
                FactorioVersion: FactorioVersionTests.ZeroPointSeventeen,
                Dependencies: TestModReleaseDependencies));
            Assert.Equal("The specified release file name version down not match the specified release version. (Parameter 'ReleaseFileName')", exception.Message);
        }
        #endregion

        #region CompareTo
        [Fact]
        public void CompareTo_WhenProvidedMoreRecentRelease_ReturnsNegativeOne()
        {
            Assert.Equal(-1, TestModYearOldRelease.CompareTo(TestModReleaseOnePointFourPointSixteen));
        }

        [Fact]
        public void CompareTo_WhenProvidedEqualRelease_ReturnsZero()
        {
            Assert.Equal(0, TestModReleaseOnePointFourPointSixteen.CompareTo(TestModReleaseOnePointFourPointSixteenClone));
        }

        [Fact]
        public void CompareTo_WhenProvidedOlderRelease_ReturnsOne()
        {
            Assert.Equal(1, TestModReleaseOnePointFourPointSixteen.CompareTo(TestModYearOldRelease));
        }
        
        [Theory]
        [MemberData(nameof(Equals_InvalidTypesData))]
        public void CompareTo_WhenProvidedDifferentType_ThrowsArgumentException(Object right)
        {
            var exception = Assert.Throws<ArgumentException>(() => TestModReleaseOnePointFourPointSixteen.CompareTo(right));
            Assert.Equal("The specified object to compare is not a Release. (Parameter 'obj')", exception.Message);
        }
        #endregion

        #region GetDependencies

        #endregion

        public static IEnumerable<object[]> Equals_InvalidTypesData =>
            new List<object[]>
            {
                new object[] {14},
                new object[] {"String"},
                new object[] {Guid.NewGuid()}
            };
    }
}
