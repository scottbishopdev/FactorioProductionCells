using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.ValueObjects;

namespace FactorioProductionCells.Domain.UnitTests.ValueObjects
{
    public class ReleaseDownloadUrlTests
    {
        internal static ReleaseDownloadUrl TestModDownloadUrl = ReleaseDownloadUrl.For("/download/Test%20Mod/1234567890abcd1234567890");
        internal static ReleaseDownloadUrl TestModCloneDownloadUrl = ReleaseDownloadUrl.For("/download/Test%20Mod/1234567890abcd1234567890");
        internal static ReleaseDownloadUrl NotATestModDownloadUrl = ReleaseDownloadUrl.For("/download/NotATestMod/1234567890abcd1234567890");
        private static Random Random = new Random();
        
        #region For
        [Theory]
        [InlineData("/download/TestMod/1234567890abcd1234567890", "TestMod")]
        [InlineData("   /download/TestMod/1234567890abcd1234567890 ", "TestMod")]
        [MemberData(nameof(ValidDownloadUrlsWithRandomModName))]
        public void For_WhenGivenValidString_ReturnsCorrectModName(String releaseDownloadUrlString, String modName)
        {
            var releaseDownloadUrl = ReleaseDownloadUrl.For(releaseDownloadUrlString);
            Assert.Equal(modName, releaseDownloadUrl.ModName);
        }

        [Theory]
        [InlineData("/download/TestMod/1234567890abcd1234567890", "1234567890abcd1234567890")]
        [InlineData("   /download/TestMod/1234567890abcd1234567890 ", "1234567890abcd1234567890")]
        [MemberData(nameof(ValidDownloadUrlsWithRandomReleaseToken))]
        public void For_WhenGivenValidString_ReturnsCorrectReleaseToken(String releaseDownloadUrlString, String releaseToken)
        {
            var releaseDownloadUrl = ReleaseDownloadUrl.For(releaseDownloadUrlString);
            Assert.Equal(releaseToken, releaseDownloadUrl.ReleaseToken);
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
        [InlineData("")]
        [InlineData("What're you going to do, bleed on me?   I'm *INVINCIBLE*!!!")]
        public void For_WhenGivenInvalidFormat_ThrowsArgumentException(String releaseDownloadUrlString)
        {
            var exception = Assert.Throws<ArgumentException>(() => ReleaseDownloadUrl.For(releaseDownloadUrlString));
            Assert.Equal($"Unable to parse \"{releaseDownloadUrlString}\" to a valid ReleaseDownloadUrl due to formatting. (Parameter 'releaseDownloadUrlString')", exception.Message);
        }

        [Fact]
        public void For_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => ReleaseDownloadUrl.For(null));
            Assert.Equal("A value for the release download URL must be provided. (Parameter 'releaseDownloadUrlString')", exception.Message);
        }

        [Theory]
        [MemberData(nameof(DownloadUrlsWithModNameTooLong))]
        public void For_WhenGivenModNameTooLong_ThrowsArgumentException(String releaseDownloadUrlString)
        {
            var exception = Assert.Throws<ArgumentException>(() => ReleaseDownloadUrl.For(releaseDownloadUrlString));
            Assert.Equal($"The mod name specified exceeds the maximum length of {Mod.NameLength}. (Parameter 'releaseDownloadUrlString')", exception.Message);
        }

        [Theory]
        [MemberData(nameof(DownloadUrlsWithReleaseTokenTooLong))]
        public void For_WhenGivenReleaseTokenTooLong_ThrowsArgumentException(String releaseDownloadUrlString)
        {
            var exception = Assert.Throws<ArgumentException>(() => ReleaseDownloadUrl.For(releaseDownloadUrlString));
            Assert.Equal($"The release token specified exceeds the maximum length of {ReleaseDownloadUrl.ReleaseTokenLength}. (Parameter 'releaseDownloadUrlString')", exception.Message);
        }
        #endregion

        #region ToStringOperator
        [Fact]
        public void ToStringOperator_WhenGivenReleaseDownloadUrl_ReturnsCorrectString()
        {
            String testModString = TestModDownloadUrl;
            Assert.Equal("/download/Test%20Mod/1234567890abcd1234567890", testModString);
        }
        #endregion

        #region ToReleaseDownloadUrlOperator
        [Theory]
        [MemberData(nameof(ToReleaseDownloadUrlOperatorData))]
        public void ToReleaseDownloadUrlOperator_WhenGivenString_ReturnsCorrectReleaseDownloadUrl(String releaseDownloadUrlString, ReleaseDownloadUrl releaseDownloadUrl)
        {
            ReleaseDownloadUrl newReleaseDownloadUrl = (ReleaseDownloadUrl)releaseDownloadUrlString;
            Assert.Equal(releaseDownloadUrl, newReleaseDownloadUrl);
        }
        #endregion

        #region Equals
        [Fact]
        public void Equals_WhenProvidedEqualReleaseDownloadUrls_ReturnsTrue()
        {
            Assert.True(TestModDownloadUrl.Equals((Object)TestModCloneDownloadUrl));
        }

        [Fact]
        public void Equals_WhenProvidedNotEqualReleaseDownloadUrls_ReturnsFalse()
        {
            Assert.False(TestModDownloadUrl.Equals((Object)NotATestModDownloadUrl));
        }
        
        [Theory]
        [MemberData(nameof(Equals_InvalidTypesData))]
        public void Equals_WhenProvidedDifferentType_ThrowsArgumentException(Object right)
        {
            var exception = Assert.Throws<ArgumentException>(() => TestModDownloadUrl.Equals(right));
            Assert.Equal("Unable to compare the specified object to a ReleaseDownloadUrl. (Parameter 'obj')", exception.Message);
        }
        #endregion

        #region GetHashCode
        [Fact]
        public void GetHashCode_MatchingReleaseDownloadUrls_ReturnSameHashCode()
        {
            Assert.Equal(TestModDownloadUrl.GetHashCode(), TestModCloneDownloadUrl.GetHashCode());
        }

        [Fact]
        public void GetHashCode_NonMatchingReleaseDownloadUrls_ReturnDifferentHashCode()
        {
            Assert.NotEqual(TestModDownloadUrl.GetHashCode(), NotATestModDownloadUrl.GetHashCode());
        }
        #endregion

        #region EqualsOperator
        [Fact]
        public void EqualsOperator_WhenGivenMatchingReleaseDownloadUrls_ReturnsTrue()
        {
            Assert.True(TestModDownloadUrl == TestModCloneDownloadUrl);
        }

        [Fact]
        public void EqualsOperator_WhenGivenNotMatchingReleaseDownloadUrls_ReturnsFalse()
        {
            Assert.False(TestModDownloadUrl == NotATestModDownloadUrl);
        }
        #endregion

        #region NotEqualsOperator
        [Fact]
        public void NotEqualsOperator_WhenGivenNotMatchingReleaseDownloadUrls_ReturnsTrue()
        {
            Assert.True(TestModDownloadUrl != NotATestModDownloadUrl);
        }
        
        [Fact]
        public void NotEqualsOperator_WhenGivenMatchingReleaseDownloadUrls_ReturnsFalse()
        {
            Assert.False(TestModDownloadUrl != TestModCloneDownloadUrl);
        }
        #endregion

        #region ToString
        [Fact]
        public void ToString_WhenGivenReleaseDownloadUrl_ReturnsCorrectString()
        {
            Assert.Equal("/download/Test%20Mod/1234567890abcd1234567890", TestModDownloadUrl.ToString());
        }
        #endregion

        #region GetAtomicValues
        [Fact]
        public void GetAtomicValues_WhenGivenReleaseDownloadUrl_ReturnsCorrectValues()
        {
            var atomicValues = TestModDownloadUrl.GetAtomicValues();
            Assert.Equal("Test%20Mod", atomicValues.ElementAt(0));
            Assert.Equal("1234567890abcd1234567890", atomicValues.ElementAt(1));
        }
        #endregion

        public static IEnumerable<object[]> ToReleaseDownloadUrlOperatorData =>
            new List<object[]>
            {
                new object[] {"/download/TestMod/1234567890abcd1234567890", ReleaseDownloadUrl.For("/download/TestMod/1234567890abcd1234567890")}
            };

        public static IEnumerable<object[]> ValidDownloadUrlsWithRandomModName()
        {
            String randomModName = GetRandomCharacterString(ReleaseDownloadUrl.ValidModNameCharacters, Mod.NameLength);
            return new List<object[]>
            {
                new object[] { new String("/download/" + randomModName + "/1234567890abcd1234567890"), randomModName}
            };
        }

        public static IEnumerable<object[]> ValidDownloadUrlsWithRandomReleaseToken()
        {
            String randomReleaseToken = GetRandomCharacterString(ReleaseDownloadUrl.ValidReleaseTokenCharacters, ReleaseDownloadUrl.ReleaseTokenLength);
            return new List<object[]>
            {
                new object[] { new String("/download/TestMod/" + randomReleaseToken), randomReleaseToken}
            };
        }

        public static IEnumerable<object[]> DownloadUrlsWithModNameTooLong =>
            new List<object[]>
            {
                new object[] {
                    new String("/download/" + GetRandomCharacterString(ReleaseDownloadUrl.ValidModNameCharacters, Mod.NameLength + 1) + "/1234567890abcd1234567890")
                },
                new object[] {
                    new String("/download/" + GetRandomCharacterString(ReleaseDownloadUrl.ValidModNameCharacters, Mod.NameLength + 100) + "/1234567890abcd1234567890")
                }
            };

        public static IEnumerable<object[]> DownloadUrlsWithReleaseTokenTooLong =>
            new List<object[]>
            {
                new object[] {
                    new String("/download/TestMod/" + GetRandomCharacterString(ReleaseDownloadUrl.ValidReleaseTokenCharacters, ReleaseDownloadUrl.ReleaseTokenLength + 1))
                },
                new object[] {
                    new String("/download/TestMod/" + GetRandomCharacterString(ReleaseDownloadUrl.ValidReleaseTokenCharacters, ReleaseDownloadUrl.ReleaseTokenLength + 100))
                }
            };

        public static IEnumerable<object[]> Equals_InvalidTypesData =>
            new List<object[]>
            {
                new object[] {14},
                new object[] {"String"},
                new object[] {Guid.NewGuid()}
            };

        private static String GetRandomCharacterString(String characterSet, Int32 length)
        {
            return new String(Enumerable.Repeat(characterSet, length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
