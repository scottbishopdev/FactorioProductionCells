using System;
using System.Linq;
using Xunit;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.ValueObjects;
using FactorioProductionCells.TestData.Domain.ValueObjects;
using FactorioProductionCells.TestData.Common;

namespace FactorioProductionCells.Domain.UnitTests.ValueObjects
{
    public class ReleaseFileNameTests
    {
        #region Copy Constructor
        [Theory]
        [MemberData(nameof(ReleaseFileNameTestData.ValidStaticReleaseFileNamesWithModName), MemberType=typeof(ReleaseFileNameTestData))]
        [MemberData(nameof(ReleaseFileNameTestData.ValidRandomReleaseFileNamesWithModName), MemberType=typeof(ReleaseFileNameTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectModName(ReleaseFileName releaseFileName, String expectedModName)
        {
            var testReleaseFileName = new ReleaseFileName(releaseFileName);
            Assert.Equal(expectedModName, testReleaseFileName.ModName);
        }

        [Theory]
        [MemberData(nameof(ReleaseFileNameTestData.ValidStaticReleaseFileNamesWithModVersion), MemberType=typeof(ReleaseFileNameTestData))]
        [MemberData(nameof(ReleaseFileNameTestData.ValidRandomReleaseFileNamesWithModVersion), MemberType=typeof(ReleaseFileNameTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectModVersion(ReleaseFileName releaseFileName, ModVersion expectedModVersion)
        {
            var testReleaseFileName = new ReleaseFileName(releaseFileName);
            Assert.Equal(expectedModVersion, testReleaseFileName.ModVersion);
        }

        [Fact]
        public void CopyConstructor_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ReleaseFileName(original: null));
            Assert.Equal("original is required. (Parameter 'original')", exception.Message);
        }
        #endregion
        
        #region For
        [Theory]
        [MemberData(nameof(ReleaseFileNameTestData.ValidStaticStringsWithModName), MemberType=typeof(ReleaseFileNameTestData))]
        [MemberData(nameof(ReleaseFileNameTestData.ValidRandomStringsWithModName), MemberType=typeof(ReleaseFileNameTestData))]
        public void For_WhenGivenValidString_ReturnsCorrectModName(String releaseFileNameString, String modName)
        {
            var releaseFileName = ReleaseFileName.For(releaseFileNameString);
            Assert.Equal(modName, releaseFileName.ModName);
        }

        [Theory]
        [MemberData(nameof(ReleaseFileNameTestData.ValidStaticStringsWithModVersion), MemberType=typeof(ReleaseFileNameTestData))]
        [MemberData(nameof(ReleaseFileNameTestData.ValidRandomStringsWithModVersion), MemberType=typeof(ReleaseFileNameTestData))]
        public void For_WhenGivenValidString_ReturnsCorrectModVersion(String releaseFileNameString, ModVersion modVersion)
        {
            var releaseFileName = ReleaseFileName.For(releaseFileNameString);
            Assert.Equal(modVersion, releaseFileName.ModVersion);
        }

        [Fact]
        public void For_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => ReleaseFileName.For(null));
            Assert.Equal("releaseFileNameString is required. (Parameter 'releaseFileNameString')", exception.Message);
        }

        [Theory]
        [MemberData(nameof(CommonTestData.EmptyAndWhitespaceStrings), MemberType=typeof(CommonTestData))]
        public void For_WhenGivenEmptyStringOrWhitespace_ThrowsArgumentException(String releaseFileNameString)
        {
            var exception = Assert.Throws<ArgumentException>(() => ReleaseFileName.For(releaseFileNameString));
            Assert.Equal($"releaseFileNameString may not be empty. (Parameter 'releaseFileNameString')", exception.Message);
        }

        [Theory]
        [InlineData("!nv@l1dCh@r$_1.4.16.zip")]
        [InlineData("TestMod-1.4.16.zip")]
        [InlineData("TestMod1.4.16.zip")]
        [InlineData("TestMod_14.16.zip")]
        [InlineData("TestMod_1.416.zip")]
        [InlineData("TestMod_1.4.16zip")]
        [InlineData("TestMod__.4.16.zip")]
        [InlineData("TestMod_1.a.16.zip")]
        [InlineData("TestMod_1.4...zip")]
        [InlineData("What're you going to do, bleed on me?   I'm *INVINCIBLE*!!!")]
        public void For_WhenGivenInvalidFormat_ThrowsArgumentException(String releaseFileNameString)
        {
            var exception = Assert.Throws<ArgumentException>(() => ReleaseFileName.For(releaseFileNameString));
            Assert.Equal($"Unable to parse \"{releaseFileNameString}\" to a valid ReleaseFileName due to formatting. (Parameter 'releaseFileNameString')", exception.Message);
        }

        [Theory]
        [MemberData(nameof(ReleaseFileNameTestData.RandomReleaseFileNamesWithModNameTooLong), parameters: 3, MemberType=typeof(ReleaseFileNameTestData))]
        public void For_WhenGivenModNameTooLong_ThrowsArgumentException(String releaseFileNameString)
        {
            var exception = Assert.Throws<ArgumentException>(() => ReleaseFileName.For(releaseFileNameString));
            Assert.Equal($"The mod name specified exceeds the maximum length of {Mod.NameLength}. (Parameter 'releaseFileNameString')", exception.Message);
        }
        #endregion

        #region ToStringOperator
        [Theory]
        [MemberData(nameof(ReleaseFileNameTestData.ValidStaticReleaseFileNamesFromForWithStrings), MemberType=typeof(ReleaseFileNameTestData))]
        [MemberData(nameof(ReleaseFileNameTestData.ValidRandomReleaseFileNamesFromForWithStrings), MemberType=typeof(ReleaseFileNameTestData))]
        public void ToStringOperator_WhenGivenReleaseFileName_ReturnsCorrectString(ReleaseFileName releaseFileName, String expected)
        {
            String releaseFileNameString = releaseFileName;
            Assert.Equal(expected, releaseFileNameString);
        }
        #endregion

        #region ToReleaseFileNameOperator
        [Theory]
        [MemberData(nameof(ReleaseFileNameTestData.ValidStaticReleaseFileNamesFromForWithStrings), MemberType=typeof(ReleaseFileNameTestData))]
        [MemberData(nameof(ReleaseFileNameTestData.ValidRandomReleaseFileNamesFromForWithStrings), MemberType=typeof(ReleaseFileNameTestData))]
        public void ToReleaseFileNameOperator_WhenGivenString_ReturnsCorrectReleaseFileName(ReleaseFileName releaseFileName, String releaseFileNameString)
        {
            ReleaseFileName newReleaseFileName = (ReleaseFileName)releaseFileNameString;
            Assert.Equal(releaseFileName, newReleaseFileName);
        }
        #endregion

        #region Equals
        [Theory]
        [MemberData(nameof(ReleaseFileNameTestData.ValidStaticEqualReleaseFileNamePairs), MemberType=typeof(ReleaseFileNameTestData))]
        [MemberData(nameof(ReleaseFileNameTestData.ValidRandomEqualReleaseFileNamePairs), MemberType=typeof(ReleaseFileNameTestData))]
        public void Equals_WhenProvidedEqualReleaseFileNames_ReturnsTrue(ReleaseFileName left, ReleaseFileName right)
        {
            Assert.True(left.Equals((Object)right));
        }

        [Theory]
        [MemberData(nameof(ReleaseFileNameTestData.ValidStaticNonEqualReleaseFileNamePairs), MemberType=typeof(ReleaseFileNameTestData))]
        public void Equals_WhenProvidedNotEqualReleaseFileNames_ReturnsFalse(ReleaseFileName left, ReleaseFileName right)
        {
            Assert.False(left.Equals((Object)right));
        }

        [Theory]
        [MemberData(nameof(CommonTestData.VariousValueTypeData), MemberType=typeof(CommonTestData))]
        public void Equals_WhenProvidedDifferentType_ThrowsArgumentException(Object right)
        {
            var exception = Assert.Throws<ArgumentException>(() => ReleaseFileNameTestData.TestModAlphaReleaseFileName.Equals(right));
            Assert.Equal("Unable to compare the specified object to a ReleaseFileName. (Parameter 'obj')", exception.Message);
        }
        #endregion

        #region GetHashCode
        [Theory]
        [MemberData(nameof(ReleaseFileNameTestData.ValidStaticEqualReleaseFileNamePairs), MemberType=typeof(ReleaseFileNameTestData))]
        [MemberData(nameof(ReleaseFileNameTestData.ValidRandomEqualReleaseFileNamePairs), MemberType=typeof(ReleaseFileNameTestData))]
        public void GetHashCode_MatchingReleaseFileNames_ReturnSameHashCode(ReleaseFileName left, ReleaseFileName right)
        {
            Assert.Equal(left.GetHashCode(), right.GetHashCode());
        }

        [Theory]
        [MemberData(nameof(ReleaseFileNameTestData.ValidStaticNonEqualReleaseFileNamePairs), MemberType=typeof(ReleaseFileNameTestData))]
        public void GetHashCode_NonMatchingReleaseFileNames_ReturnDifferentHashCode(ReleaseFileName left, ReleaseFileName right)
        {
            Assert.NotEqual(left.GetHashCode(), right.GetHashCode());
        }
        #endregion

        #region EqualsOperator
        [Theory]
        [MemberData(nameof(ReleaseFileNameTestData.ValidStaticEqualReleaseFileNamePairs), MemberType=typeof(ReleaseFileNameTestData))]
        [MemberData(nameof(ReleaseFileNameTestData.ValidRandomEqualReleaseFileNamePairs), MemberType=typeof(ReleaseFileNameTestData))]
        public void EqualsOperator_WhenGivenMatchingReleaseFileNames_ReturnsTrue(ReleaseFileName left, ReleaseFileName right)
        {
            Assert.True(left == right);
        }

        [Theory]
        [MemberData(nameof(ReleaseFileNameTestData.ValidStaticNonEqualReleaseFileNamePairs), MemberType=typeof(ReleaseFileNameTestData))]
        public void EqualsOperator_WhenGivenNotMatchingReleaseFileNames_ReturnsFalse(ReleaseFileName left, ReleaseFileName right)
        {
            Assert.False(left == right);
        }
        #endregion

        #region NotEqualsOperator
        [Theory]
        [MemberData(nameof(ReleaseFileNameTestData.ValidStaticNonEqualReleaseFileNamePairs), MemberType=typeof(ReleaseFileNameTestData))]
        public void NotEqualsOperator_WhenGivenNotMatchingReleaseFileNames_ReturnsTrue(ReleaseFileName left, ReleaseFileName right)
        {
            Assert.True(left != right);
        }

        [Theory]
        [MemberData(nameof(ReleaseFileNameTestData.ValidStaticEqualReleaseFileNamePairs), MemberType=typeof(ReleaseFileNameTestData))]
        [MemberData(nameof(ReleaseFileNameTestData.ValidRandomEqualReleaseFileNamePairs), MemberType=typeof(ReleaseFileNameTestData))]
        public void NotEqualsOperator_WhenGivenMatchingReleaseFileNames_ReturnsFalse(ReleaseFileName left, ReleaseFileName right)
        {
            Assert.False(left != right);
        }
        #endregion

        #region ToString
        [Theory]
        [MemberData(nameof(ReleaseFileNameTestData.ValidStaticReleaseFileNamesFromForWithStrings), MemberType=typeof(ReleaseFileNameTestData))]
        [MemberData(nameof(ReleaseFileNameTestData.ValidRandomReleaseFileNamesFromForWithStrings), MemberType=typeof(ReleaseFileNameTestData))]
        public void ToString_WhenGivenReleaseFileName_ReturnsCorrectString(ReleaseFileName releaseFileName, String expected)
        {
            Assert.Equal(expected, releaseFileName.ToString());
        }
        #endregion

        #region GetAtomicValues
        [Theory]
        [MemberData(nameof(ReleaseFileNameTestData.ValidStaticReleaseFileNamesFromForWithModNameAndModVersion), MemberType=typeof(ReleaseFileNameTestData))]
        [MemberData(nameof(ReleaseFileNameTestData.ValidRandomReleaseFileNamesFromForWithModNameAndModVersion), MemberType=typeof(ReleaseFileNameTestData))]
        public void GetAtomicValues_WhenGivenReleaseFileName_ReturnsCorrectValues(ReleaseFileName releaseFileName, String expectedModName, ModVersion expectedModVersion)
        {
            var atomicValues = releaseFileName.GetAtomicValues();
            Assert.Equal(expectedModName, atomicValues.ElementAt(0));
            Assert.Equal(expectedModVersion, atomicValues.ElementAt(1));
        }
        #endregion
    }
}
