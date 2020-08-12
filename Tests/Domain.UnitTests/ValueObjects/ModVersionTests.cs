using System;
using System.Linq;
using Xunit;
using FactorioProductionCells.Domain.ValueObjects;
using FactorioProductionCells.TestData.Domain.ValueObjects;
using FactorioProductionCells.TestData.Common;

namespace FactorioProductionCells.Domain.UnitTests.ValueObjects
{
    public class ModVersionTests
    {
        #region Copy Constructor
        [Theory]
        [MemberData(nameof(ModVersionTestData.ValidStaticModVersionsWithMajor), MemberType=typeof(ModVersionTestData))]
        [MemberData(nameof(ModVersionTestData.ValidRandomModVersionsWithMajor), MemberType=typeof(ModVersionTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectMajor(ModVersion modVersion, Int32 expectedMajor)
        {
            var testModVersion = new ModVersion(modVersion);
            Assert.Equal(expectedMajor, testModVersion.Major);
        }

        [Theory]
        [MemberData(nameof(ModVersionTestData.ValidStaticModVersionsWithMinor), MemberType=typeof(ModVersionTestData))]
        [MemberData(nameof(ModVersionTestData.ValidRandomModVersionsWithMinor), MemberType=typeof(ModVersionTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectMinor(ModVersion modVersion, Int32 expectedMinor)
        {
            var testModVersion = new ModVersion(modVersion);
            Assert.Equal(expectedMinor, testModVersion.Minor);
        }

        [Theory]
        [MemberData(nameof(ModVersionTestData.ValidStaticModVersionsWithPatch), MemberType=typeof(ModVersionTestData))]
        [MemberData(nameof(ModVersionTestData.ValidRandomModVersionsWithPatch), MemberType=typeof(ModVersionTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectPatch(ModVersion modVersion, Int32 expectedPatch)
        {
            var testModVersion = new ModVersion(modVersion);
            Assert.Equal(expectedPatch, testModVersion.Patch);
        }

        [Fact]
        public void CopyConstructor_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ModVersion(original: null));
            Assert.Equal("original is required. (Parameter 'original')", exception.Message);
        }
        #endregion
        
        #region For
        [Theory]
        [InlineData("1.0.0", 1)]
        [InlineData("43.883.34567", 43)]
        [InlineData("2147483647.14.0", Int32.MaxValue)]
        [MemberData(nameof(ModVersionTestData.ValidStaticStringsWithMajorInt), MemberType=typeof(ModVersionTestData))]
        [MemberData(nameof(ModVersionTestData.ValidRandomStringsWithMajorInt), MemberType=typeof(ModVersionTestData))]
        public void For_WhenGivenValidString_ReturnsCorrectMajorVersion(String modVersionString, Int32 major)
        {
            var modVersion = ModVersion.For(modVersionString);
            Assert.Equal(major, modVersion.Major);
        }

        [Theory]
        [InlineData("1.0.0", 0)]
        [InlineData("43.883.34567", 883)]
        [InlineData("14.2147483647.0", Int32.MaxValue)]
        [MemberData(nameof(ModVersionTestData.ValidStaticStringsWithMinorInt), MemberType=typeof(ModVersionTestData))]
        [MemberData(nameof(ModVersionTestData.ValidRandomStringsWithMinorInt), MemberType=typeof(ModVersionTestData))]
        public void For_WhenGivenValidString_ReturnsCorrectMinorVersion(String modVersionString, Int32 minor)
        {
            var modVersion = ModVersion.For(modVersionString);
            Assert.Equal(minor, modVersion.Minor);
        }

        [Theory]
        [InlineData("1.0.0", 0)]
        [InlineData("43.883.34567", 34567)]
        [InlineData("14.0.2147483647", Int32.MaxValue)]
        [MemberData(nameof(ModVersionTestData.ValidStaticStringsWithPatchInt), MemberType=typeof(ModVersionTestData))]
        [MemberData(nameof(ModVersionTestData.ValidRandomStringsWithPatchInt), MemberType=typeof(ModVersionTestData))]
        public void For_WhenGivenValidString_ReturnsCorrectPatchVersion(String modVersionString, Int32 patch)
        {
            var modVersion = ModVersion.For(modVersionString);
            Assert.Equal(patch, modVersion.Patch);
        }

        [Theory]
        [InlineData("-1.4.16")]
        [InlineData("-43.-883.-34567")]
        [InlineData("-2147483647.14.0")]
        public void For_WhenGivenStringWithNegativeMajorVersion_ThrowsArgumentOutOfRangeException(String modVersionString)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => ModVersion.For(modVersionString));
            Assert.Equal($"Unable to parse \"{modVersionString}\" into a ModVersion - version parts must be positive. (Parameter 'modVersionString')", exception.Message);
        }

        [Theory]
        [InlineData("0.-4.16")]
        [InlineData("-43.-883.-34567")]
        [InlineData("14.-2147483647.0")]
        public void For_WhenGivenStringWithNegativeMinorVersion_ThrowsArgumentOutOfRangeException(String modVersionString)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => ModVersion.For(modVersionString));
            Assert.Equal($"Unable to parse \"{modVersionString}\" into a ModVersion - version parts must be positive. (Parameter 'modVersionString')", exception.Message);
        }

        [Theory]
        [InlineData("0.0.-16")]
        [InlineData("-43.-883.-34567")]
        [InlineData("14.0.-2147483647")]
        public void For_WhenGivenStringWithNegativePatchVersion_ThrowsArgumentOutOfRangeException(String modVersionString)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => ModVersion.For(modVersionString));
            Assert.Equal($"Unable to parse \"{modVersionString}\" into a ModVersion - version parts must be positive. (Parameter 'modVersionString')", exception.Message);
        }

        [Fact]
        public void For_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => ModVersion.For(null));
            Assert.Equal("modVersionString is required. (Parameter 'modVersionString')", exception.Message);
        }

        [Theory]
        [MemberData(nameof(CommonTestData.EmptyAndWhitespaceStrings), MemberType=typeof(CommonTestData))]
        public void For_WhenGivenEmptyStringOrWhitespace_ThrowsArgumentException(String modVersionString)
        {
            var exception = Assert.Throws<ArgumentException>(() => ModVersion.For(modVersionString));
            Assert.Equal("modVersionString may not be empty. (Parameter 'modVersionString')", exception.Message);
        }

        [Theory]
        [InlineData("1416")]
        [InlineData("0-416")]
        [InlineData("0-4-16")]
        [InlineData("0.4.")]
        [InlineData("1.")]
        [InlineData("0.14")]
        [InlineData("1..")]
        [InlineData("1")]
        [InlineData("What're you going to do, bleed on me?   I'm *INVINCIBLE*!!!")]
        [InlineData("foo.bar.baz")]
        public void For_WhenGivenInvalidFormat_ThrowsArgumentException(String modVersionString)
        {
            var exception = Assert.Throws<ArgumentException>(() => ModVersion.For(modVersionString));
            Assert.Equal($"Unable to parse \"{modVersionString}\" to a valid ModVersion due to formatting. (Parameter 'modVersionString')", exception.Message);
        }
        #endregion

        #region ToStringOperator
        [Theory]
        [MemberData(nameof(ModVersionTestData.ValidStaticModVersionsFromForWithStrings), MemberType=typeof(ModVersionTestData))]
        [MemberData(nameof(ModVersionTestData.ValidRandomModVersionsFromForWithStrings), MemberType=typeof(ModVersionTestData))]
        public void ToStringOperator_WhenGivenModVersion_ReturnsCorrectString(ModVersion version, String expected)
        {
            String modVersionString = version;
            Assert.Equal(expected, modVersionString);
        }
        #endregion

        #region ToModVersionOperator
        [Theory]
        [MemberData(nameof(ModVersionTestData.ValidStaticModVersionsFromForWithStrings), MemberType=typeof(ModVersionTestData))]
        [MemberData(nameof(ModVersionTestData.ValidRandomModVersionsFromForWithStrings), MemberType=typeof(ModVersionTestData))]
        public void ToModVersionOperator_WhenGivenString_ReturnsCorrectModVersion(ModVersion expectedModVersion, String modVersionString)
        {
            ModVersion newModVersion = (ModVersion)modVersionString;
            Assert.Equal(expectedModVersion, newModVersion);
        }
        #endregion

        #region Equals
        [Theory]
        [MemberData(nameof(ModVersionTestData.ValidStaticEqualModVersionPairs), MemberType=typeof(ModVersionTestData))]
        [MemberData(nameof(ModVersionTestData.ValidRandomEqualModVersionPairs), MemberType=typeof(ModVersionTestData))]
        public void Equals_WhenProvidedEqualVersion_ReturnsTrue(ModVersion left, ModVersion right)
        {
            Assert.True(left.Equals((Object)right));
        }

        [Theory]
        [MemberData(nameof(ModVersionTestData.ValidStaticNonEqualModVersionPairs), MemberType=typeof(ModVersionTestData))]
        public void Equals_WhenProvidedNotEqualVersion_ReturnsFalse(ModVersion left, ModVersion right)
        {
            Assert.False(left.Equals((Object)right));
        }

        [Theory]
        [MemberData(nameof(CommonTestData.VariousValueTypeData), MemberType=typeof(CommonTestData))]
        public void Equals_WhenProvidedDifferentType_ThrowsArgumentException(Object right)
        {
            var exception = Assert.Throws<ArgumentException>(() => ModVersionTestData.TestModAlphaReleaseVersion.Equals(right));
            Assert.Equal("Unable to compare the specified object to a ModVersion. (Parameter 'obj')", exception.Message);
        }
        #endregion

        #region GetHashCode
        [Theory]
        [MemberData(nameof(ModVersionTestData.ValidStaticEqualModVersionPairs), MemberType=typeof(ModVersionTestData))]
        [MemberData(nameof(ModVersionTestData.ValidRandomEqualModVersionPairs), MemberType=typeof(ModVersionTestData))]
        public void GetHashCode_MatchingVersions_ReturnSameHashCode(ModVersion left, ModVersion right)
        {
            Assert.Equal(left.GetHashCode(), right.GetHashCode());
        }

        [Theory]
        [MemberData(nameof(ModVersionTestData.ValidStaticNonEqualModVersionPairs), MemberType=typeof(ModVersionTestData))]
        public void GetHashCode_NonMatchingVersions_ReturnDifferentHashCode(ModVersion left, ModVersion right)
        {
            Assert.NotEqual(left.GetHashCode(), right.GetHashCode());
        }
        #endregion

        #region EqualsOperator
        [Theory]
        [MemberData(nameof(ModVersionTestData.ValidStaticEqualModVersionPairs), MemberType=typeof(ModVersionTestData))]
        [MemberData(nameof(ModVersionTestData.ValidRandomEqualModVersionPairs), MemberType=typeof(ModVersionTestData))]
        public void EqualsOperator_WhenGivenMatchingVersions_ReturnsTrue(ModVersion left, ModVersion right)
        {
            Assert.True(left == right);
        }

        [Theory]
        [MemberData(nameof(ModVersionTestData.ValidStaticNonEqualModVersionPairs), MemberType=typeof(ModVersionTestData))]
        public void EqualsOperator_WhenGivenNotMatchingVersions_ReturnsFalse(ModVersion left, ModVersion right)
        {
            Assert.False(left == right);
        }
        #endregion

        #region NotEqualsOperator
        [Theory]
        [MemberData(nameof(ModVersionTestData.ValidStaticNonEqualModVersionPairs), MemberType=typeof(ModVersionTestData))]
        public void NotEqualsOperator_WhenGivenNotMatchingVersions_ReturnsTrue(ModVersion left, ModVersion right)
        {
            Assert.True(left != right);
        }

        [Theory]
        [MemberData(nameof(ModVersionTestData.ValidStaticEqualModVersionPairs), MemberType=typeof(ModVersionTestData))]
        [MemberData(nameof(ModVersionTestData.ValidRandomEqualModVersionPairs), MemberType=typeof(ModVersionTestData))]
        public void NotEqualsOperator_WhenGivenMatchingVersions_ReturnsFalse(ModVersion left, ModVersion right)
        {
            Assert.False(left != right);
        }
        #endregion

        #region GreaterThanOperator
        [Theory]
        [MemberData(nameof(ModVersionTestData.Comparison_RightLowerData), MemberType=typeof(ModVersionTestData))]
        public void GreaterThanOperator_WhenRightIsLowerVersion_ReturnsTrue(String left, String right)
        {
            Assert.True(ModVersion.For(left) > ModVersion.For(right));
        }
        
        [Theory]
        [MemberData(nameof(ModVersionTestData.Comparison_EqualData), MemberType=typeof(ModVersionTestData))]
        public void GreaterThanOperator_WhenVersionsAreEqual_ReturnsFalse(String left, String right)
        {
            Assert.False(ModVersion.For(left) > ModVersion.For(right));
        }

        [Theory]
        [MemberData(nameof(ModVersionTestData.Comparison_LeftLowerData), MemberType=typeof(ModVersionTestData))]
        public void GreaterThanOperator_WhenLeftIsLowerVersion_ReturnsFalse(String left, String right)
        {
            Assert.False(ModVersion.For(left) > ModVersion.For(right));
        }
        #endregion

        #region GreaterThanEqualsOperator
        [Theory]
        [MemberData(nameof(ModVersionTestData.Comparison_RightLowerData), MemberType=typeof(ModVersionTestData))]
        public void GreaterThanEqualsOperator_WhenRightIsLowerVersion_ReturnsTrue(String left, String right)
        {
            Assert.True(ModVersion.For(left) >= ModVersion.For(right));
        }
        
        [Theory]
        [MemberData(nameof(ModVersionTestData.Comparison_EqualData), MemberType=typeof(ModVersionTestData))]
        public void GreaterThanEqualsOperator_WhenVersionsAreEqual_ReturnsTrue(String left, String right)
        {
            Assert.True(ModVersion.For(left) >= ModVersion.For(right));
        }

        [Theory]
        [MemberData(nameof(ModVersionTestData.Comparison_LeftLowerData), MemberType=typeof(ModVersionTestData))]
        public void GreaterThanEqualsOperator_WhenLeftIsLowerVersion_ReturnsFalse(String left, String right)
        {
            Assert.False(ModVersion.For(left) >= ModVersion.For(right));
        }
        #endregion

        #region LessThanOperator
        [Theory]
        [MemberData(nameof(ModVersionTestData.Comparison_RightLowerData), MemberType=typeof(ModVersionTestData))]
        public void LessThanOperator_WhenRightIsLowerVersion_ReturnsFalse(String left, String right)
        {
            Assert.False(ModVersion.For(left) < ModVersion.For(right));
        }
        
        [Theory]
        [MemberData(nameof(ModVersionTestData.Comparison_EqualData), MemberType=typeof(ModVersionTestData))]
        public void LessThanOperator_WhenVersionsAreEqual_ReturnsFalse(String left, String right)
        {
            Assert.False(ModVersion.For(left) < ModVersion.For(right));
        }

        [Theory]
        [MemberData(nameof(ModVersionTestData.Comparison_LeftLowerData), MemberType=typeof(ModVersionTestData))]
        public void LessThanOperator_WhenLeftIsLowerVersion_ReturnsTrue(String left, String right)
        {
            Assert.True(ModVersion.For(left) < ModVersion.For(right));
        }
        #endregion

        #region LessThanEqualsOperator
        [Theory]
        [MemberData(nameof(ModVersionTestData.Comparison_RightLowerData), MemberType=typeof(ModVersionTestData))]
        public void LessThanEqualsOperator_WhenRightIsLowerVersion_ReturnsFalse(String left, String right)
        {
            Assert.False(ModVersion.For(left) <= ModVersion.For(right));
        }
        
        [Theory]
        [MemberData(nameof(ModVersionTestData.Comparison_EqualData), MemberType=typeof(ModVersionTestData))]
        public void LessThanEqualsOperator_WhenVersionsAreEqual_ReturnsTrue(String left, String right)
        {
            Assert.True(ModVersion.For(left) <= ModVersion.For(right));
        }

        [Theory]
        [MemberData(nameof(ModVersionTestData.Comparison_LeftLowerData), MemberType=typeof(ModVersionTestData))]
        public void LessThanEqualsOperator_WhenLeftIsLowerVersion_ReturnsTrue(String left, String right)
        {
            Assert.True(ModVersion.For(left) <= ModVersion.For(right));
        }
        #endregion

        #region CompareTo
        [Theory]
        [MemberData(nameof(ModVersionTestData.Comparison_LeftLowerData), MemberType=typeof(ModVersionTestData))]
        public void CompareTo_WhenProvidedLargerVersion_ReturnsNegativeOne(String left, String right)
        {
            Assert.Equal(-1, ModVersion.For(left).CompareTo(ModVersion.For(right)));
        }

        [Theory]
        [MemberData(nameof(ModVersionTestData.Comparison_EqualData), MemberType=typeof(ModVersionTestData))]
        public void CompareTo_WhenProvidedEqualVersion_ReturnsZero(String left, String right)
        {
            Assert.Equal(0, ModVersion.For(left).CompareTo(ModVersion.For(right)));
        }

        [Theory]
        [MemberData(nameof(ModVersionTestData.Comparison_RightLowerData), MemberType=typeof(ModVersionTestData))]
        public void CompareTo_WhenProvidedSmallerVersion_ReturnsOne(String left, String right)
        {
            Assert.Equal(1, ModVersion.For(left).CompareTo(ModVersion.For(right)));
        }
        
        [Theory]
        [MemberData(nameof(CommonTestData.VariousValueTypeData), MemberType=typeof(CommonTestData))]
        public void CompareTo_WhenProvidedDifferentType_ThrowsArgumentException(Object right)
        {
            var exception = Assert.Throws<ArgumentException>(() => ModVersionTestData.TestModAlphaReleaseVersion.CompareTo(right));
            Assert.Equal($"Unable to compare the specified object to a ModVersion. (Parameter 'obj')", exception.Message);
        }
        #endregion

        #region ToString
        [Theory]
        [MemberData(nameof(ModVersionTestData.ValidStaticModVersionsFromForWithStrings), MemberType=typeof(ModVersionTestData))]
        [MemberData(nameof(ModVersionTestData.ValidRandomModVersionsFromForWithStrings), MemberType=typeof(ModVersionTestData))]
        public void ToString_WhenGivenModVersion_ReturnsCorrectString(ModVersion version, String expected)
        {
            Assert.Equal(expected, version.ToString());
        }
        #endregion

        #region GetAtomicValues
        [Theory]
        [MemberData(nameof(ModVersionTestData.ValidStaticModVersionsFromForWithInts), MemberType=typeof(ModVersionTestData))]
        [MemberData(nameof(ModVersionTestData.ValidRandomModVersionsFromForWithInts), MemberType=typeof(ModVersionTestData))]
        public void GetAtomicValues_WhenGivenModVersion_ReturnsCorrectValues(ModVersion version, Int32 expectedMajor, Int32 expectedMinor, Int32 expectedPatch)
        {
            var atomicValues = version.GetAtomicValues();
            Assert.Equal(expectedMajor, atomicValues.ElementAt(0));
            Assert.Equal(expectedMinor, atomicValues.ElementAt(1));
            Assert.Equal(expectedPatch, atomicValues.ElementAt(2));
        }
        #endregion
    }
}
