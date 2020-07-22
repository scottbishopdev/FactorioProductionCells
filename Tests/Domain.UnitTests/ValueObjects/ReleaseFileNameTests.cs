using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.ValueObjects;

namespace FactorioProductionCells.Domain.UnitTests.ValueObjects
{
    public class ReleaseFileNameTests
    {
        internal static ReleaseFileName OnePointFourPointSixteenReleaseFileName = ReleaseFileName.For("Test Mod_1.4.16.zip");
        internal static ReleaseFileName OnePointFivePointOneReleaseFileName = ReleaseFileName.For("Test Mod_1.5.1.zip");
        internal static ReleaseFileName OnePointFourPointSixteenCloneReleaseFileName = ReleaseFileName.For("Test Mod_1.4.16.zip");
        internal static ReleaseFileName NotATestModReleaseFileName = ReleaseFileName.For("NotATestMod_2.5.17.zip");
        private static List<Int32> integerValues = new List<Int32> { 0, 1, 2, Int32.MaxValue };
        private static Random Random = new Random();
      
        #region For
        [Theory]
        [InlineData("TestMod_1.4.16.zip", "TestMod")]
        [InlineData(" TestMod_1.4.16.zip    ", "TestMod")]
        [InlineData("TestMod_1.4.16.zipTestMod_1.4.16.zip", "TestMod_1.4.16.zipTestMod")]
        [MemberData(nameof(ValidReleaseFileNameWithRandomModName))]
        public void For_WhenGivenValidString_ReturnsCorrectModName(String releaseFileNameString, String modName)
        {
            var releaseFileName = ReleaseFileName.For(releaseFileNameString);
            Assert.Equal(modName.Trim(), releaseFileName.ModName);
        }

        [Theory]
        [InlineData("TestMod_1.4.16.zip", "1.4.16")]
        [InlineData(" TestMod_1.4.16.zip    ", "1.4.16")]
        [MemberData(nameof(ValidReleaseFileNamesWithRandomModVersion))]
        public void For_WhenGivenValidString_ReturnsCorrectModVersion(String releaseFileNameString, String modVersionString)
        {
            var releaseFileName = ReleaseFileName.For(releaseFileNameString);
            Assert.Equal(ModVersion.For(modVersionString), releaseFileName.ModVersion);
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
        [InlineData("")]
        [InlineData("What're you going to do, bleed on me?   I'm *INVINCIBLE*!!!")]
        public void For_WhenGivenInvalidFormat_ThrowsArgumentException(String releaseFileNameString)
        {
            var exception = Assert.Throws<ArgumentException>(() => ReleaseFileName.For(releaseFileNameString));
            Assert.Equal($"Unable to parse \"{releaseFileNameString}\" to a valid ReleaseFileName due to formatting. (Parameter 'releaseFileNameString')", exception.Message);
        }

        [Fact]
        public void For_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => ReleaseFileName.For(null));
            Assert.Equal("A value for the release file name must be provided. (Parameter 'releaseFileNameString')", exception.Message);
        }

        [Theory]
        [MemberData(nameof(ReleaseFileNamesWithModNameTooLong))]
        public void For_WhenGivenModNameTooLong_ThrowsArgumentException(String releaseFileNameString)
        {
            var exception = Assert.Throws<ArgumentException>(() => ReleaseFileName.For(releaseFileNameString));
            Assert.Equal($"The mod name specified exceeds the maximum length of {Mod.NameLength}. (Parameter 'releaseFileNameString')", exception.Message);
        }
        #endregion

        #region ToStringOperator
        [Fact]
        public void ToStringOperator_WhenGivenReleaseFileName_ReturnsCorrectString()
        {
            String testModString = OnePointFourPointSixteenReleaseFileName;
            Assert.Equal("Test Mod_1.4.16.zip", testModString);
        }
        #endregion

        #region ToReleaseFileNameOperator
        [Theory]
        [MemberData(nameof(ToReleaseFileNameOperatorData))]
        public void ToReleaseFileNameOperator_WhenGivenString_ReturnsCorrectReleaseFileName(String releaseFileNameString, ReleaseFileName releaseFileName)
        {
            ReleaseFileName newReleaseFileName = (ReleaseFileName)releaseFileNameString;
            Assert.Equal(releaseFileName, newReleaseFileName);
        }
        #endregion

        #region Equals
        [Fact]
        public void Equals_WhenProvidedEqualReleaseFileNames_ReturnsTrue()
        {
            Assert.True(OnePointFourPointSixteenReleaseFileName.Equals((Object)OnePointFourPointSixteenCloneReleaseFileName));
        }

        [Fact]
        public void Equals_WhenProvidedNotEqualReleaseFileNames_ReturnsFalse()
        {
            Assert.False(OnePointFourPointSixteenReleaseFileName.Equals((Object)NotATestModReleaseFileName));
        }
        
        [Theory]
        [MemberData(nameof(Equals_InvalidTypesData))]
        public void Equals_WhenProvidedDifferentType_ThrowsArgumentException(Object right)
        {
            var exception = Assert.Throws<ArgumentException>(() => OnePointFourPointSixteenReleaseFileName.Equals(right));
            Assert.Equal("Unable to compare the specified object to a ReleaseFileName. (Parameter 'obj')", exception.Message);
        }
        #endregion

        #region GetHashCode
        [Fact]
        public void GetHashCode_MatchingReleaseFileNames_ReturnSameHashCode()
        {
            Assert.Equal(OnePointFourPointSixteenReleaseFileName.GetHashCode(), OnePointFourPointSixteenCloneReleaseFileName.GetHashCode());
        }

        [Fact]
        public void GetHashCode_NonMatchingReleaseFileNames_ReturnDifferentHashCode()
        {
            Assert.NotEqual(OnePointFourPointSixteenReleaseFileName.GetHashCode(), NotATestModReleaseFileName.GetHashCode());
        }
        #endregion

        #region EqualsOperator
        [Fact]
        public void EqualsOperator_WhenGivenMatchingReleaseFileNames_ReturnsTrue()
        {
            Assert.True(OnePointFourPointSixteenReleaseFileName == OnePointFourPointSixteenCloneReleaseFileName);
        }

        [Fact]
        public void EqualsOperator_WhenGivenNotMatchingReleaseFileNames_ReturnsFalse()
        {
            Assert.False(OnePointFourPointSixteenReleaseFileName == NotATestModReleaseFileName);
        }
        #endregion

        #region NotEqualsOperator
        [Fact]
        public void NotEqualsOperator_WhenGivenNotMatchingReleaseFileNames_ReturnsTrue()
        {
            Assert.True(OnePointFourPointSixteenReleaseFileName != NotATestModReleaseFileName);
        }
        
        [Fact]
        public void NotEqualsOperator_WhenGivenMatchingReleaseFileNames_ReturnsFalse()
        {
            Assert.False(OnePointFourPointSixteenReleaseFileName != OnePointFourPointSixteenCloneReleaseFileName);
        }
        #endregion

        #region ToString
        [Fact]
        public void ToString_WhenGivenReleaseFileName_ReturnsCorrectString()
        {
            Assert.Equal("Test Mod_1.4.16.zip", OnePointFourPointSixteenReleaseFileName.ToString());
        }
        #endregion

        #region GetAtomicValues
        [Fact]
        public void GetAtomicValues_WhenGivenReleaseFileName_ReturnsCorrectValues()
        {
            var atomicValues = OnePointFourPointSixteenReleaseFileName.GetAtomicValues();
            Assert.Equal("Test Mod", atomicValues.ElementAt(0));
            Assert.Equal(ModVersion.For("1.4.16"), atomicValues.ElementAt(1));
        }
        #endregion

        public static IEnumerable<object[]> ValidReleaseFileNameWithRandomModName()
        {
            String randomModName = GetRandomCharacterString(ReleaseFileName.ValidModNameCharacters, Mod.NameLength);
            return new List<object[]>
            {
                new object[] { new String(randomModName + "_1.4.16.zip"), randomModName}
            };
        }

        public static IEnumerable<object[]> ValidReleaseFileNamesWithRandomModVersion =>
            from leftMajor in integerValues
            from leftMinor in integerValues
            from leftPatch in integerValues
            select new object[] { $"TestMod_{leftMajor.ToString()}.{leftMinor.ToString()}.{leftPatch.ToString()}.zip", $"{leftMajor.ToString()}.{leftMinor.ToString()}.{leftPatch.ToString()}" };

        public static IEnumerable<object[]> ReleaseFileNamesWithModNameTooLong =>
            new List<object[]>
            {
                new object[] {
                    new String(GetRandomCharacterString(ReleaseFileName.ValidModNameCharacters, Mod.NameLength + 1) + "_1.4.16.zip")
                },
                new object[] {
                    new String(GetRandomCharacterString(ReleaseFileName.ValidModNameCharacters, Mod.NameLength + 100) + "_1.4.16.zip")
                }
            };

        public static IEnumerable<object[]> ToReleaseFileNameOperatorData =>
            new List<object[]>
            {
                new object[] {"TestMod_1.4.16.zip", ReleaseFileName.For("TestMod_1.4.16.zip")}
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
