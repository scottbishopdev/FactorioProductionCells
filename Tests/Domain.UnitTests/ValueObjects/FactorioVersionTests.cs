using System;
using System.Linq;
using Xunit;
using FactorioProductionCells.Domain.ValueObjects;
using FactorioProductionCells.TestData.Domain.ValueObjects;
using FactorioProductionCells.TestData.Common;

namespace FactorioProductionCells.Domain.UnitTests.ValueObjects
{
    public class FactorioVersionTests
    {
        #region Copy Constructor
        [Theory]
        [MemberData(nameof(FactorioVersionTestData.ValidStaticFactorioVersionsWithMajor), MemberType=typeof(FactorioVersionTestData))]
        [MemberData(nameof(FactorioVersionTestData.ValidRandomFactorioVersionsWithMajor), MemberType=typeof(FactorioVersionTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectMajor(FactorioVersion factorioVersion, Int32 expectedMajor)
        {
            var testFactorioVersion = new FactorioVersion(factorioVersion);
            Assert.Equal(expectedMajor, testFactorioVersion.Major);
        }

        [Theory]
        [MemberData(nameof(FactorioVersionTestData.ValidStaticFactorioVersionsWithMinor), MemberType=typeof(FactorioVersionTestData))]
        [MemberData(nameof(FactorioVersionTestData.ValidRandomFactorioVersionsWithMinor), MemberType=typeof(FactorioVersionTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectMinor(FactorioVersion factorioVersion, Int32 expectedMinor)
        {
            var testFactorioVersion = new FactorioVersion(factorioVersion);
            Assert.Equal(expectedMinor, testFactorioVersion.Minor);
        }

        [Fact]
        public void CopyConstructor_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new FactorioVersion(original: null));
            Assert.Equal("original is required. (Parameter 'original')", exception.Message);
        }
        #endregion
        
        #region For
        [Theory]
        [InlineData("43.883", 43)]
        [InlineData("2147483647.14", Int32.MaxValue)]
        [MemberData(nameof(FactorioVersionTestData.ValidStaticStringsWithMajorInt), MemberType=typeof(FactorioVersionTestData))]
        [MemberData(nameof(FactorioVersionTestData.ValidRandomStringsWithMajorInt), MemberType=typeof(FactorioVersionTestData))]
        public void For_WhenGivenStringWithValidMajorVersion_ReturnsCorrectMajorVersion(String factorioVersionString, Int32 major)
        {
            var factorioVersion = FactorioVersion.For(factorioVersionString);
            Assert.Equal(major, factorioVersion.Major);
        }

        [Theory]
        [InlineData("43.883", 883)]
        [InlineData("14.2147483647", Int32.MaxValue)]
        [MemberData(nameof(FactorioVersionTestData.ValidStaticStringsWithMinorInt), MemberType=typeof(FactorioVersionTestData))]
        [MemberData(nameof(FactorioVersionTestData.ValidRandomStringsWithMinorInt), MemberType=typeof(FactorioVersionTestData))]
        public void For_WhenGivenStringWithValidMinorVersion_ReturnsCorrectMinorVersion(String factorioVersionString, Int32 minor)
        {
            var factorioVersion = FactorioVersion.For(factorioVersionString);
            Assert.Equal(minor, factorioVersion.Minor);
        }

        [Theory]
        [InlineData("-1.0")]
        [InlineData("-43.-883")]
        [InlineData("-2147483647.14")]
        public void For_WhenGivenStringWithNegativeMajorVersion_ThrowsArgumentOutOfRangeException(String factorioVersionString)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => FactorioVersion.For(factorioVersionString));
            Assert.Equal($"Unable to parse \"{factorioVersionString}\" into a FactorioVersion - version parts must be positive. (Parameter 'factorioVersionString')", exception.Message);
        }

        [Theory]
        [InlineData("0.-1")]
        [InlineData("-43.-883")]
        [InlineData("14.-2147483647")]
        public void For_WhenGivenStringWithNegativeMinorVersion_ThrowsArgumentOutOfRangeException(String factorioVersionString)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => FactorioVersion.For(factorioVersionString));
            Assert.Equal($"Unable to parse \"{factorioVersionString}\" into a FactorioVersion - version parts must be positive. (Parameter 'factorioVersionString')", exception.Message);
        }

        [Fact]
        public void For_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => FactorioVersion.For(null));
            Assert.Equal("factorioVersionString is required. (Parameter 'factorioVersionString')", exception.Message);
        }

        [Theory]
        [MemberData(nameof(CommonTestData.EmptyAndWhitespaceStrings), MemberType=typeof(CommonTestData))]
        public void For_WhenGivenEmptyStringOrWhitespace_ThrowsArgumentException(String factorioVersionString)
        {
            var exception = Assert.Throws<ArgumentException>(() => FactorioVersion.For(factorioVersionString));
            Assert.Equal($"factorioVersionString may not be empty. (Parameter 'factorioVersionString')", exception.Message);
        }

        [Theory]
        [InlineData("017")]
        [InlineData("0-17")]
        [InlineData("0.17.0")]
        [InlineData("What're you going to do, bleed on me?   I'm *INVINCIBLE*!!!")]
        [InlineData("foo.bar")]
        public void For_WhenGivenInvalidFormat_ThrowsArgumentException(String factorioVersionString)
        {
            var exception = Assert.Throws<ArgumentException>(() => FactorioVersion.For(factorioVersionString));
            Assert.Equal($"Unable to parse \"{factorioVersionString}\" to a valid FactorioVersion due to formatting. (Parameter 'factorioVersionString')", exception.Message);
        }
        #endregion

        #region ToStringOperator
        [Theory]
        [MemberData(nameof(FactorioVersionTestData.ValidStaticFactorioVersionsFromForWithStrings), MemberType=typeof(FactorioVersionTestData))]
        [MemberData(nameof(FactorioVersionTestData.ValidRandomFactorioVersionsFromForWithStrings), MemberType=typeof(FactorioVersionTestData))]
        public void ToStringOperator_WhenGivenFactorioVersion_ReturnsCorrectString(FactorioVersion version, String expected)
        {
            String factorioVersionString = version;
            Assert.Equal(expected, factorioVersionString);
        }
        #endregion

        #region ToFactorioVersionOperator
        [Theory]
        [MemberData(nameof(FactorioVersionTestData.ValidStaticFactorioVersionsFromForWithStrings), MemberType=typeof(FactorioVersionTestData))]
        [MemberData(nameof(FactorioVersionTestData.ValidRandomFactorioVersionsFromForWithStrings), MemberType=typeof(FactorioVersionTestData))]
        public void ToFactorioVersionOperator_WhenGivenString_ReturnsCorrectFactorioVersion(FactorioVersion expectedFactorioVersion, String factorioVersionString)
        {
            FactorioVersion newFactorioVersion = (FactorioVersion)factorioVersionString;
            Assert.Equal(expectedFactorioVersion, newFactorioVersion);
        }
        #endregion

        #region Equals
        [Theory]
        [MemberData(nameof(FactorioVersionTestData.ValidStaticEqualFactorioVersionPairs), MemberType=typeof(FactorioVersionTestData))]
        [MemberData(nameof(FactorioVersionTestData.ValidRandomEqualFactorioVersionPairs), MemberType=typeof(FactorioVersionTestData))]
        public void Equals_WhenProvidedEqualVersion_ReturnsTrue(FactorioVersion left, FactorioVersion right)
        {
            Assert.True(left.Equals((Object)right));
        }

        [Theory]
        [MemberData(nameof(FactorioVersionTestData.ValidStaticNonEqualFactorioVersionPairs), MemberType=typeof(FactorioVersionTestData))]
        public void Equals_WhenProvidedNotEqualVersion_ReturnsFalse(FactorioVersion left, FactorioVersion right)
        {
            Assert.False(left.Equals((Object)right));
        }

        [Theory]
        [MemberData(nameof(CommonTestData.VariousValueTypeData), MemberType=typeof(CommonTestData))]
        public void Equals_WhenProvidedDifferentType_ThrowsArgumentException(Object right)
        {
            var exception = Assert.Throws<ArgumentException>(() => FactorioVersionTestData.ZeroPointSeventeen.Equals(right));
            Assert.Equal("Unable to compare the specified object to a FactorioVersion. (Parameter 'obj')", exception.Message);
        }
        #endregion

        #region GetHashCode
        [Theory]
        [MemberData(nameof(FactorioVersionTestData.ValidStaticEqualFactorioVersionPairs), MemberType=typeof(FactorioVersionTestData))]
        [MemberData(nameof(FactorioVersionTestData.ValidRandomEqualFactorioVersionPairs), MemberType=typeof(FactorioVersionTestData))]
        public void GetHashCode_MatchingVersions_ReturnSameHashCode(FactorioVersion left, FactorioVersion right)
        {
            Assert.Equal(left.GetHashCode(), right.GetHashCode());
        }

        [Theory]
        [MemberData(nameof(FactorioVersionTestData.ValidStaticNonEqualFactorioVersionPairs), MemberType=typeof(FactorioVersionTestData))]
        public void GetHashCode_NonMatchingVersions_ReturnDifferentHashCode(FactorioVersion left, FactorioVersion right)
        {
            Assert.NotEqual(left.GetHashCode(), right.GetHashCode());
        }
        #endregion

        #region EqualsOperator
        [Theory]
        [MemberData(nameof(FactorioVersionTestData.ValidStaticEqualFactorioVersionPairs), MemberType=typeof(FactorioVersionTestData))]
        [MemberData(nameof(FactorioVersionTestData.ValidRandomEqualFactorioVersionPairs), MemberType=typeof(FactorioVersionTestData))]
        public void EqualsOperator_WhenGivenMatchingVersions_ReturnsTrue(FactorioVersion left, FactorioVersion right)
        {
            Assert.True(left == right);
        }

        [Theory]
        [MemberData(nameof(FactorioVersionTestData.ValidStaticNonEqualFactorioVersionPairs), MemberType=typeof(FactorioVersionTestData))]
        public void EqualsOperator_WhenGivenNotMatchingVersions_ReturnsFalse(FactorioVersion left, FactorioVersion right)
        {
            Assert.False(left == right);
        }
        #endregion

        #region NotEqualsOperator
        [Theory]
        [MemberData(nameof(FactorioVersionTestData.ValidStaticNonEqualFactorioVersionPairs), MemberType=typeof(FactorioVersionTestData))]
        public void NotEqualsOperator_WhenGivenNotMatchingVersions_ReturnsTrue(FactorioVersion left, FactorioVersion right)
        {
            Assert.True(left != right);
        }

        [Theory]
        [MemberData(nameof(FactorioVersionTestData.ValidStaticEqualFactorioVersionPairs), MemberType=typeof(FactorioVersionTestData))]
        [MemberData(nameof(FactorioVersionTestData.ValidRandomEqualFactorioVersionPairs), MemberType=typeof(FactorioVersionTestData))]
        public void NotEqualsOperator_WhenGivenMatchingVersions_ReturnsFalse(FactorioVersion left, FactorioVersion right)
        {
            Assert.False(left != right);
        }
        #endregion

        #region GreaterThanOperator
        [Theory]
        [MemberData(nameof(FactorioVersionTestData.Comparison_RightLowerData), MemberType=typeof(FactorioVersionTestData))]
        public void GreaterThanOperator_WhenRightIsLowerVersion_ReturnsTrue(String left, String right)
        {
            Assert.True(FactorioVersion.For(left) > FactorioVersion.For(right));
        }
        
        [Theory]
        [MemberData(nameof(FactorioVersionTestData.Comparison_EqualData), MemberType=typeof(FactorioVersionTestData))]
        public void GreaterThanOperator_WhenVersionsAreEqual_ReturnsFalse(String left, String right)
        {
            Assert.False(FactorioVersion.For(left) > FactorioVersion.For(right));
        }

        [Theory]
        [MemberData(nameof(FactorioVersionTestData.Comparison_LeftLowerData), MemberType=typeof(FactorioVersionTestData))]
        public void GreaterThanOperator_WhenLeftIsLowerVersion_ReturnsFalse(String left, String right)
        {
            Assert.False(FactorioVersion.For(left) > FactorioVersion.For(right));
        }
        #endregion

        #region GreaterThanEqualsOperator
        [Theory]
        [MemberData(nameof(FactorioVersionTestData.Comparison_RightLowerData), MemberType=typeof(FactorioVersionTestData))]
        public void GreaterThanEqualsOperator_WhenRightIsLowerVersion_ReturnsTrue(String left, String right)
        {
            Assert.True(FactorioVersion.For(left) >= FactorioVersion.For(right));
        }
        
        [Theory]
        [MemberData(nameof(FactorioVersionTestData.Comparison_EqualData), MemberType=typeof(FactorioVersionTestData))]
        public void GreaterThanEqualsOperator_WhenVersionsAreEqual_ReturnsTrue(String left, String right)
        {
            Assert.True(FactorioVersion.For(left) >= FactorioVersion.For(right));
        }

        [Theory]
        [MemberData(nameof(FactorioVersionTestData.Comparison_LeftLowerData), MemberType=typeof(FactorioVersionTestData))]
        public void GreaterThanEqualsOperator_WhenLeftIsLowerVersion_ReturnsFalse(String left, String right)
        {
            Assert.False(FactorioVersion.For(left) >= FactorioVersion.For(right));
        }
        #endregion

        #region LessThanOperator
        [Theory]
        [MemberData(nameof(FactorioVersionTestData.Comparison_RightLowerData), MemberType=typeof(FactorioVersionTestData))]
        public void LessThanOperator_WhenRightIsLowerVersion_ReturnsFalse(String left, String right)
        {
            Assert.False(FactorioVersion.For(left) < FactorioVersion.For(right));
        }
        
        [Theory]
        [MemberData(nameof(FactorioVersionTestData.Comparison_EqualData), MemberType=typeof(FactorioVersionTestData))]
        public void LessThanOperator_WhenVersionsAreEqual_ReturnsFalse(String left, String right)
        {
            Assert.False(FactorioVersion.For(left) < FactorioVersion.For(right));
        }

        [Theory]
        [MemberData(nameof(FactorioVersionTestData.Comparison_LeftLowerData), MemberType=typeof(FactorioVersionTestData))]
        public void LessThanOperator_WhenLeftIsLowerVersion_ReturnsTrue(String left, String right)
        {
            Assert.True(FactorioVersion.For(left) < FactorioVersion.For(right));
        }
        #endregion

        #region LessThanEqualsOperator
        [Theory]
        [MemberData(nameof(FactorioVersionTestData.Comparison_RightLowerData), MemberType=typeof(FactorioVersionTestData))]
        public void LessThanEqualsOperator_WhenRightIsLowerVersion_ReturnsFalse(String left, String right)
        {
            Assert.False(FactorioVersion.For(left) <= FactorioVersion.For(right));
        }
        
        [Theory]
        [MemberData(nameof(FactorioVersionTestData.Comparison_EqualData), MemberType=typeof(FactorioVersionTestData))]
        public void LessThanEqualsOperator_WhenVersionsAreEqual_ReturnsTrue(String left, String right)
        {
            Assert.True(FactorioVersion.For(left) <= FactorioVersion.For(right));
        }

        [Theory]
        [MemberData(nameof(FactorioVersionTestData.Comparison_LeftLowerData), MemberType=typeof(FactorioVersionTestData))]
        public void LessThanEqualsOperator_WhenLeftIsLowerVersion_ReturnsTrue(String left, String right)
        {
            Assert.True(FactorioVersion.For(left) <= FactorioVersion.For(right));
        }
        #endregion

        #region CompareTo
        [Theory]
        [MemberData(nameof(FactorioVersionTestData.Comparison_LeftLowerData), MemberType=typeof(FactorioVersionTestData))]
        public void CompareTo_WhenProvidedLargerVersion_ReturnsNegativeOne(String left, String right)
        {
            Assert.Equal(-1, FactorioVersion.For(left).CompareTo(FactorioVersion.For(right)));
        }

        [Theory]
        [MemberData(nameof(FactorioVersionTestData.Comparison_EqualData), MemberType=typeof(FactorioVersionTestData))]
        public void CompareTo_WhenProvidedEqualVersion_ReturnsZero(String left, String right)
        {
            Assert.Equal(0, FactorioVersion.For(left).CompareTo(FactorioVersion.For(right)));
        }

        [Theory]
        [MemberData(nameof(FactorioVersionTestData.Comparison_RightLowerData), MemberType=typeof(FactorioVersionTestData))]
        public void CompareTo_WhenProvidedSmallerVersion_ReturnsOne(String left, String right)
        {
            Assert.Equal(1, FactorioVersion.For(left).CompareTo(FactorioVersion.For(right)));
        }
        
        [Theory]
        [MemberData(nameof(CommonTestData.VariousValueTypeData), MemberType=typeof(CommonTestData))]
        public void CompareTo_WhenProvidedDifferentType_ThrowsArgumentException(Object right)
        {
            var exception = Assert.Throws<ArgumentException>(() => FactorioVersionTestData.ZeroPointSeventeen.CompareTo(right));
            Assert.Equal("The specified object to compare is not a FactorioVersion. (Parameter 'obj')", exception.Message);
        }
        #endregion

        #region ToString
        [Theory]
        [MemberData(nameof(FactorioVersionTestData.ValidStaticFactorioVersionsFromForWithStrings), MemberType=typeof(FactorioVersionTestData))]
        [MemberData(nameof(FactorioVersionTestData.ValidRandomFactorioVersionsFromForWithStrings), MemberType=typeof(FactorioVersionTestData))]
        public void ToString_WhenGivenFactorioVersion_ReturnsCorrectString(FactorioVersion version, String expected)
        {
            Assert.Equal(expected, version.ToString());
        }
        #endregion

        #region GetAtomicValues
        [Theory]
        [MemberData(nameof(FactorioVersionTestData.ValidStaticFactorioVersionsFromForWithInts), MemberType=typeof(FactorioVersionTestData))]
        [MemberData(nameof(FactorioVersionTestData.ValidRandomFactorioVersionsFromForWithInts), MemberType=typeof(FactorioVersionTestData))]
        public void GetAtomicValues_WhenGivenFactorioVersion_ReturnsCorrectValues(FactorioVersion version, Int32 expectedMajor, Int32 expectedMinor)
        {
            var atomicValues = version.GetAtomicValues();
            Assert.Equal(expectedMajor, atomicValues.ElementAt(0));
            Assert.Equal(expectedMinor, atomicValues.ElementAt(1));
        }
        #endregion
    }
}
