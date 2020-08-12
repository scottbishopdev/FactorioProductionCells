using System;
using System.Linq;
using Xunit;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.ValueObjects;
using FactorioProductionCells.TestData.Domain.ValueObjects;
using FactorioProductionCells.TestData.Common;

namespace FactorioProductionCells.Domain.UnitTests.ValueObjects
{
    public class ReleaseDownloadUrlTests
    {
        #region Copy Constructor
        [Theory]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidStaticReleaseDownloadUrlsWithModName), MemberType=typeof(ReleaseDownloadUrlTestData))]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidRandomReleaseDownloadUrlsWithModName), MemberType=typeof(ReleaseDownloadUrlTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectModName(ReleaseDownloadUrl releaseDownloadUrl, String expectedModName)
        {
            var testReleaseDownloadUrl = new ReleaseDownloadUrl(releaseDownloadUrl);
            Assert.Equal(expectedModName, testReleaseDownloadUrl.ModName);
        }

        [Theory]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidStaticReleaseDownloadUrlsWithReleaseToken), MemberType=typeof(ReleaseDownloadUrlTestData))]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidRandomReleaseDownloadUrlsWithReleaseToken), MemberType=typeof(ReleaseDownloadUrlTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectReleaseToken(ReleaseDownloadUrl releaseDownloadUrl, String expectedReleaseToken)
        {
            var testReleaseDownloadUrl = new ReleaseDownloadUrl(releaseDownloadUrl);
            Assert.Equal(expectedReleaseToken, testReleaseDownloadUrl.ReleaseToken);
        }

        [Fact]
        public void CopyConstructor_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ReleaseDownloadUrl(original: null));
            Assert.Equal("original is required. (Parameter 'original')", exception.Message);
        }
        #endregion
        
        #region For
        [Theory]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidStaticStringsWithModName), MemberType=typeof(ReleaseDownloadUrlTestData))]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidRandomStringsWithModName), MemberType=typeof(ReleaseDownloadUrlTestData))]
        public void For_WhenGivenValidString_ReturnsCorrectModName(String releaseDownloadUrlString, String modName)
        {
            var releaseDownloadUrl = ReleaseDownloadUrl.For(releaseDownloadUrlString);
            Assert.Equal(modName, releaseDownloadUrl.ModName);
        }

        [Theory]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidStaticStringsWithReleaseToken), MemberType=typeof(ReleaseDownloadUrlTestData))]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidRandomStringsWithReleaseToken), MemberType=typeof(ReleaseDownloadUrlTestData))]
        public void For_WhenGivenValidString_ReturnsCorrectReleaseToken(String releaseDownloadUrlString, String releaseToken)
        {
            var releaseDownloadUrl = ReleaseDownloadUrl.For(releaseDownloadUrlString);
            Assert.Equal(releaseToken, releaseDownloadUrl.ReleaseToken);
        }

        [Fact]
        public void For_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => ReleaseDownloadUrl.For(null));
            Assert.Equal("releaseDownloadUrlString is required. (Parameter 'releaseDownloadUrlString')", exception.Message);
        }

        [Theory]
        [MemberData(nameof(CommonTestData.EmptyAndWhitespaceStrings), MemberType=typeof(CommonTestData))]
        public void For_WhenGivenEmptyStringOrWhitespace_ThrowsArgumentException(String releaseDownloadUrlString)
        {
            var exception = Assert.Throws<ArgumentException>(() => ReleaseDownloadUrl.For(releaseDownloadUrlString));
            Assert.Equal($"releaseDownloadUrlString may not be empty. (Parameter 'releaseDownloadUrlString')", exception.Message);
        }

        [Theory]
        [InlineData("/dornload/TestMod/1234567890abcd1234567890")]
        [InlineData("/download/!nv@l1dCh@r$/1234567890abcd1234567890")]
        [InlineData("/download/TestMod/WYSIWYG")]
        [InlineData("/download/TestMod/ZZZZZZZZZZZZZZZZZZZZZZZZ")]
        [InlineData("/download/TestMod/!@#$%^&&*()+=~`<>|;:?/,.")]
        [InlineData("//TestMod/1234567890abcd1234567890")]
        [InlineData("/download//1234567890abcd1234567890")]
        [InlineData("/download/TestMod/")]
        [InlineData("/TestMod/1234567890abcd1234567890")]
        [InlineData("/download/1234567890abcd1234567890")]
        [InlineData("/download/TestMod")]
        [InlineData("What're you going to do, bleed on me?   I'm *INVINCIBLE*!!!")]
        public void For_WhenGivenInvalidFormat_ThrowsArgumentException(String releaseDownloadUrlString)
        {
            var exception = Assert.Throws<ArgumentException>(() => ReleaseDownloadUrl.For(releaseDownloadUrlString));
            Assert.Equal($"Unable to parse \"{releaseDownloadUrlString}\" to a valid ReleaseDownloadUrl due to formatting. (Parameter 'releaseDownloadUrlString')", exception.Message);
        }

        [Theory]
        [MemberData(nameof(ReleaseDownloadUrlTestData.RandomReleaseDownloadUrlsWithModNameTooLong), parameters: 3, MemberType=typeof(ReleaseDownloadUrlTestData))]
        public void For_WhenGivenModNameTooLong_ThrowsArgumentException(String releaseDownloadUrlString)
        {
            var exception = Assert.Throws<ArgumentException>(() => ReleaseDownloadUrl.For(releaseDownloadUrlString));
            Assert.Equal($"The mod name specified exceeds the maximum length of {Mod.NameLength}. (Parameter 'releaseDownloadUrlString')", exception.Message);
        }

        [Theory]
        [MemberData(nameof(ReleaseDownloadUrlTestData.RandomReleaseDownloadUrlsWithReleaseTokenTooLong), parameters: 3, MemberType=typeof(ReleaseDownloadUrlTestData))]
        public void For_WhenGivenReleaseTokenTooLong_ThrowsArgumentException(String releaseDownloadUrlString)
        {
            var exception = Assert.Throws<ArgumentException>(() => ReleaseDownloadUrl.For(releaseDownloadUrlString));
            Assert.Equal($"The release token specified exceeds the maximum length of {ReleaseDownloadUrl.ReleaseTokenLength}. (Parameter 'releaseDownloadUrlString')", exception.Message);
        }
        #endregion

        #region ToStringOperator
        [Theory]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidStaticReleaseDownloadUrlsFromForWithStrings), MemberType=typeof(ReleaseDownloadUrlTestData))]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidRandomReleaseDownloadUrlsFromForWithStrings), MemberType=typeof(ReleaseDownloadUrlTestData))]
        public void ToStringOperator_WhenGivenReleaseDownloadUrl_ReturnsCorrectString(ReleaseDownloadUrl releaseDownloadUrl, String expected)
        {
            String releaseDownloadUrlString = releaseDownloadUrl;
            Assert.Equal(expected, releaseDownloadUrlString);
        }
        #endregion

        #region ToReleaseDownloadUrlOperator
        [Theory]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidStaticReleaseDownloadUrlsFromForWithStrings), MemberType=typeof(ReleaseDownloadUrlTestData))]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidRandomReleaseDownloadUrlsFromForWithStrings), MemberType=typeof(ReleaseDownloadUrlTestData))]
        public void ToReleaseDownloadUrlOperator_WhenGivenString_ReturnsCorrectReleaseDownloadUrl(ReleaseDownloadUrl releaseDownloadUrl, String releaseDownloadUrlString)
        {
            ReleaseDownloadUrl newReleaseDownloadUrl = (ReleaseDownloadUrl)releaseDownloadUrlString;
            Assert.Equal(releaseDownloadUrl, newReleaseDownloadUrl);
        }
        #endregion

        #region Equals
        [Theory]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidStaticEqualReleaseDownloadUrlPairs), MemberType=typeof(ReleaseDownloadUrlTestData))]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidRandomEqualReleaseDownloadUrlPairs), MemberType=typeof(ReleaseDownloadUrlTestData))]
        public void Equals_WhenProvidedEqualReleaseDownloadUrls_ReturnsTrue(ReleaseDownloadUrl left, ReleaseDownloadUrl right)
        {
            Assert.True(left.Equals((Object)right));
        }

        [Theory]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidStaticNonEqualReleaseDownloadUrlPairs), MemberType=typeof(ReleaseDownloadUrlTestData))]
        public void Equals_WhenProvidedNotEqualVersion_ReturnsFalse(ReleaseDownloadUrl left, ReleaseDownloadUrl right)
        {
            Assert.False(left.Equals((Object)right));
        }
        
        [Theory]
        [MemberData(nameof(CommonTestData.VariousValueTypeData), MemberType=typeof(CommonTestData))]
        public void Equals_WhenProvidedDifferentType_ThrowsArgumentException(Object right)
        {
            var exception = Assert.Throws<ArgumentException>(() => ReleaseDownloadUrlTestData.TestModDownloadUrl.Equals(right));
            Assert.Equal("Unable to compare the specified object to a ReleaseDownloadUrl. (Parameter 'obj')", exception.Message);
        }
        #endregion

        #region GetHashCode
        [Theory]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidStaticEqualReleaseDownloadUrlPairs), MemberType=typeof(ReleaseDownloadUrlTestData))]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidRandomEqualReleaseDownloadUrlPairs), MemberType=typeof(ReleaseDownloadUrlTestData))]
        public void GetHashCode_MatchingReleaseDownloadUrls_ReturnSameHashCode(ReleaseDownloadUrl left, ReleaseDownloadUrl right)
        {
            Assert.Equal(left.GetHashCode(), right.GetHashCode());
        }

        [Theory]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidStaticNonEqualReleaseDownloadUrlPairs), MemberType=typeof(ReleaseDownloadUrlTestData))]
        public void GetHashCode_NonMatchingReleaseDownloadUrls_ReturnDifferentHashCode(ReleaseDownloadUrl left, ReleaseDownloadUrl right)
        {
            Assert.NotEqual(left.GetHashCode(), right.GetHashCode());
        }
        #endregion

        #region EqualsOperator
        [Theory]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidStaticEqualReleaseDownloadUrlPairs), MemberType=typeof(ReleaseDownloadUrlTestData))]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidRandomEqualReleaseDownloadUrlPairs), MemberType=typeof(ReleaseDownloadUrlTestData))]
        public void EqualsOperator_WhenGivenMatchingReleaseDownloadUrls_ReturnsTrue(ReleaseDownloadUrl left, ReleaseDownloadUrl right)
        {
            Assert.True(left == right);
        }

        [Theory]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidStaticNonEqualReleaseDownloadUrlPairs), MemberType=typeof(ReleaseDownloadUrlTestData))]
        public void EqualsOperator_WhenGivenNotMatchingReleaseDownloadUrls_ReturnsFalse(ReleaseDownloadUrl left, ReleaseDownloadUrl right)
        {
            Assert.False(left == right);
        }
        #endregion

        #region NotEqualsOperator
        [Theory]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidStaticNonEqualReleaseDownloadUrlPairs), MemberType=typeof(ReleaseDownloadUrlTestData))]
        public void NotEqualsOperator_WhenGivenNotMatchingReleaseDownloadUrls_ReturnsTrue(ReleaseDownloadUrl left, ReleaseDownloadUrl right)
        {
            Assert.True(left != right);
        }
        
        [Theory]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidStaticEqualReleaseDownloadUrlPairs), MemberType=typeof(ReleaseDownloadUrlTestData))]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidRandomEqualReleaseDownloadUrlPairs), MemberType=typeof(ReleaseDownloadUrlTestData))]
        public void NotEqualsOperator_WhenGivenMatchingReleaseDownloadUrls_ReturnsFalse(ReleaseDownloadUrl left, ReleaseDownloadUrl right)
        {
            Assert.False(left != right);
        }
        #endregion

        #region ToString
        [Theory]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidStaticReleaseDownloadUrlsFromForWithStrings), MemberType=typeof(ReleaseDownloadUrlTestData))]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidRandomReleaseDownloadUrlsFromForWithStrings), MemberType=typeof(ReleaseDownloadUrlTestData))]
        public void ToString_WhenGivenReleaseDownloadUrl_ReturnsCorrectString(ReleaseDownloadUrl releaseDownloadUrl, String expected)
        {
            Assert.Equal(expected, releaseDownloadUrl.ToString());
        }
        #endregion

        #region GetAtomicValues
        [Theory]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidStaticReleaseDownloadUrlsFromForWithModNameAndReleaseToken), MemberType=typeof(ReleaseDownloadUrlTestData))]
        [MemberData(nameof(ReleaseDownloadUrlTestData.ValidRandomReleaseDownloadUrlsFromForWithModNameAndReleaseToken), MemberType=typeof(ReleaseDownloadUrlTestData))]
        public void GetAtomicValues_WhenGivenReleaseDownloadUrl_ReturnsCorrectValues(ReleaseDownloadUrl releaseDownloadUrl, String expectedModName, String expectedReleaseToken)
        {
            var atomicValues = releaseDownloadUrl.GetAtomicValues();
            Assert.Equal(expectedModName, atomicValues.ElementAt(0));
            Assert.Equal(expectedReleaseToken, atomicValues.ElementAt(1));
        }
        #endregion
    }
}
