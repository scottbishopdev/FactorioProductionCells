using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FactorioProductionCells.Domain.ValueObjects;

namespace FactorioProductionCells.Domain.UnitTests.ValueObjects
{
    public class FactorioVersionTests
    {
        private static FactorioVersion ZeroPointSeventeen = FactorioVersion.For("0.17");
        private static FactorioVersion ZeroPointSeventeenClone = FactorioVersion.For("0.17");
        private static FactorioVersion OnePointZero = FactorioVersion.For("1.0");
        private static List<Int32> integerValues = new List<Int32> { 0, 1, 2, Int32.MaxValue };

        #region For
        [Theory]
        [InlineData("0.17", 0)]
        [InlineData("   0.17 ", 0)]
        [InlineData("1.0", 1)] // Never gonna happen. :P
        [InlineData("43.883", 43)]
        [InlineData("2147483647.14", Int32.MaxValue)]
        public void For_WhenGivenStringWithValidMajorVersion_ReturnsCorrectMajorVersion(String factorioVersionString, Int32 major)
        {
            var factorioVersion = FactorioVersion.For(factorioVersionString);
            Assert.Equal(major, factorioVersion.Major);
        }

        [Theory]
        [InlineData("-1.0")]
        [InlineData("-43.-883")]
        [InlineData("-2147483647.14")]
        public void For_WhenGivenStringWithNegativeMajorVersion_ThrowsArgumentOutOfRangeException(String factorioVersionString)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => FactorioVersion.For(factorioVersionString));
        }

        [Theory]
        [InlineData("0.17", 17)]
        [InlineData(" 0.17  ", 17)]
        [InlineData("1.0", 0)]
        [InlineData("43.883", 883)]
        [InlineData("14.2147483647", Int32.MaxValue)]
        public void For_WhenGivenStringWithValidMinorVersion_ReturnsCorrectMinorVersion(String factorioVersionString, Int32 minor)
        {
            var factorioVersion = FactorioVersion.For(factorioVersionString);
            Assert.Equal(minor, factorioVersion.Minor);
        }

        [Theory]
        [InlineData("0.-1")]
        [InlineData("-43.-883")]
        [InlineData("14.-2147483647")]
        public void For_WhenGivenStringWithNegativeMinorVersion_ThrowsArgumentOutOfRangeException(String factorioVersionString)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => FactorioVersion.For(factorioVersionString));
        }

        [Theory]
        [InlineData("017")]
        [InlineData("0-17")]
        [InlineData("0.17.0")]
        [InlineData("")]
        [InlineData("What're you going to do, bleed on me?   I'm *INVINCIBLE*!!!")]
        [InlineData("foo.bar")]
        public void For_WhenGivenInvalidFormat_ThrowsArgumentException(String factorioVersionString)
        {
            Assert.Throws<ArgumentException>(() => FactorioVersion.For(factorioVersionString));
        }

        [Fact]
        public void For_WhenGivenInvalidFormat_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => FactorioVersion.For(null));
        }
        #endregion

        #region ToStringOperator
        [Fact]
        public void ToStringOperator_WhenGivenFactorioVersion_ReturnsCorrectString()
        {
            String factorioVersionString = ZeroPointSeventeen;
            Assert.Equal("0.17", factorioVersionString);
        }
        #endregion

        #region ToFactorioVersionOperator
        [Theory]
        [MemberData(nameof(ToFactorioVersionOperatorData))]
        public void ToFactorioVersionOperator_WhenGivenString_ReturnsCorrectFactorioVersion(String factorioVersionString, FactorioVersion factorioVersion)
        {
            FactorioVersion newFactorioVersion = (FactorioVersion)factorioVersionString;
            Assert.Equal(factorioVersion, newFactorioVersion);
        }
        #endregion

        #region Equals
        [Fact]
        public void Equals_WhenProvidedEqualVersion_ReturnsTrue()
        {
            Assert.True(ZeroPointSeventeen.Equals((Object)ZeroPointSeventeenClone));
        }

        [Fact]
        public void Equals_WhenProvidedNotEqualVersion_ReturnsFalse()
        {
            Assert.False(ZeroPointSeventeen.Equals((Object)OnePointZero));
        }
        
        [Theory]
        [MemberData(nameof(Equals_InvalidTypesData))]
        public void Equals_WhenProvidedDifferentType_ThrowsArgumentException(Object right)
        {
            Assert.Throws<ArgumentException>(() => ZeroPointSeventeen.Equals(right));
        }
        #endregion

        #region GetHashCode
        [Fact]
        public void GetHashCode_MatchingVersions_ReturnSameHashCode()
        {
            Assert.Equal(ZeroPointSeventeen.GetHashCode(), ZeroPointSeventeenClone.GetHashCode());
        }

        [Fact]
        public void GetHashCode_MatchingVersions_ReturnDifferentHashCode()
        {
            Assert.NotEqual(ZeroPointSeventeen.GetHashCode(), OnePointZero.GetHashCode());
        }
        #endregion

        #region EqualsOperator
        [Fact]
        public void EqualsOperator_WhenGivenMatchingVersions_ReturnsTrue()
        {
            Assert.True(ZeroPointSeventeen == ZeroPointSeventeenClone);
        }

        [Fact]
        public void EqualsOperator_WhenGivenNotMatchingVersions_ReturnsFalse()
        {
            Assert.False(ZeroPointSeventeen == OnePointZero);
        }
        #endregion

        #region NotEqualsOperator
        [Fact]
        public void NotEqualsOperator_WhenGivenNotMatchingVersions_ReturnsTrue()
        {
            Assert.True(ZeroPointSeventeen != OnePointZero);
        }
        
        [Fact]
        public void NotEqualsOperator_WhenGivenMatchingVersions_ReturnsFalse()
        {
            Assert.False(ZeroPointSeventeen != ZeroPointSeventeenClone);
        }
        #endregion

        #region GreaterThanOperator
        [Theory]
        [MemberData(nameof(Comparison_RightLowerData))]
        public void GreaterThanOperator_WhenRightIsLowerVersion_ReturnsTrue(String left, String right)
        {
            Assert.True(FactorioVersion.For(left) > FactorioVersion.For(right));
        }
        
        [Theory]
        [MemberData(nameof(Comparison_EqualData))]
        public void GreaterThanOperator_WhenVersionsAreEqual_ReturnsFalse(String left, String right)
        {
            Assert.False(FactorioVersion.For(left) > FactorioVersion.For(right));
        }

        [Theory]
        [MemberData(nameof(Comparison_LeftLowerData))]
        public void GreaterThanOperator_WhenLeftIsLowerVersion_ReturnsFalse(String left, String right)
        {
            Assert.False(FactorioVersion.For(left) > FactorioVersion.For(right));
        }
        #endregion

        #region GreaterThanEqualsOperator
        [Theory]
        [MemberData(nameof(Comparison_RightLowerData))]
        public void GreaterThanEqualsOperator_WhenRightIsLowerVersion_ReturnsTrue(String left, String right)
        {
            Assert.True(FactorioVersion.For(left) >= FactorioVersion.For(right));
        }
        
        [Theory]
        [MemberData(nameof(Comparison_EqualData))]
        public void GreaterThanEqualsOperator_WhenVersionsAreEqual_ReturnsTrue(String left, String right)
        {
            Assert.True(FactorioVersion.For(left) >= FactorioVersion.For(right));
        }

        [Theory]
        [MemberData(nameof(Comparison_LeftLowerData))]
        public void GreaterThanEqualsOperator_WhenLeftIsLowerVersion_ReturnsFalse(String left, String right)
        {
            Assert.False(FactorioVersion.For(left) >= FactorioVersion.For(right));
        }
        #endregion

        #region LessThanOperator
        [Theory]
        [MemberData(nameof(Comparison_RightLowerData))]
        public void LessThanOperator_WhenRightIsLowerVersion_ReturnsFalse(String left, String right)
        {
            Assert.False(FactorioVersion.For(left) < FactorioVersion.For(right));
        }
        
        [Theory]
        [MemberData(nameof(Comparison_EqualData))]
        public void LessThanOperator_WhenVersionsAreEqual_ReturnsFalse(String left, String right)
        {
            Assert.False(FactorioVersion.For(left) < FactorioVersion.For(right));
        }

        [Theory]
        [MemberData(nameof(Comparison_LeftLowerData))]
        public void LessThanOperator_WhenLeftIsLowerVersion_ReturnsTrue(String left, String right)
        {
            Assert.True(FactorioVersion.For(left) < FactorioVersion.For(right));
        }
        #endregion

        #region LessThanEqualsOperator
        [Theory]
        [MemberData(nameof(Comparison_RightLowerData))]
        public void LessThanEqualsOperator_WhenRightIsLowerVersion_ReturnsFalse(String left, String right)
        {
            Assert.False(FactorioVersion.For(left) <= FactorioVersion.For(right));
        }
        
        [Theory]
        [MemberData(nameof(Comparison_EqualData))]
        public void LessThanEqualsOperator_WhenVersionsAreEqual_ReturnsTrue(String left, String right)
        {
            Assert.True(FactorioVersion.For(left) <= FactorioVersion.For(right));
        }

        [Theory]
        [MemberData(nameof(Comparison_LeftLowerData))]
        public void LessThanEqualsOperator_WhenLeftIsLowerVersion_ReturnsTrue(String left, String right)
        {
            Assert.True(FactorioVersion.For(left) <= FactorioVersion.For(right));
        }
        #endregion

        #region CompareTo
        
        [Theory]
        [MemberData(nameof(Comparison_LeftLowerData))]
        public void CompareTo_WhenProvidedLargerVersion_ReturnsNegativeOne(String left, String right)
        {
            Assert.Equal(-1, FactorioVersion.For(left).CompareTo(FactorioVersion.For(right)));
        }

        [Theory]
        [MemberData(nameof(Comparison_EqualData))]
        public void CompareTo_WhenProvidedEqualVersion_ReturnsZero(String left, String right)
        {
            Assert.Equal(0, FactorioVersion.For(left).CompareTo(FactorioVersion.For(right)));
        }

        [Theory]
        [MemberData(nameof(Comparison_RightLowerData))]
        public void CompareTo_WhenProvidedSmallerVersion_ReturnsOne(String left, String right)
        {
            Assert.Equal(1, FactorioVersion.For(left).CompareTo(FactorioVersion.For(right)));
        }
        
        [Theory]
        [MemberData(nameof(Equals_InvalidTypesData))]
        public void CompareTo_WhenProvidedDifferentType_ThrowsArgumentException(Object right)
        {
            Assert.Throws<ArgumentException>(() => ZeroPointSeventeen.CompareTo(right));
        }
        #endregion

        #region ToString
        [Fact]
        public void ToString_WhenGivenFactorioVersion_ReturnsCorrectString()
        {
            Assert.Equal("0.17", ZeroPointSeventeen.ToString());
        }
        #endregion

        #region GetAtomicValues
        [Fact]
        public void GetAtomicValues_WhenGivenFactorioVersion_ReturnsCorrectString()
        {
            var atomicValues = ZeroPointSeventeen.GetAtomicValues();
            Assert.Equal(0, atomicValues.ElementAt(0));
            Assert.Equal(17, atomicValues.ElementAt(1));
        }
        #endregion

        public static IEnumerable<object[]> ToFactorioVersionOperatorData =>
            new List<object[]>
            {
                new object[] {"0.17", FactorioVersion.For("0.17")}
            };

        public static IEnumerable<object[]> Equals_InvalidTypesData =>
            new List<object[]>
            {
                new object[] {14},
                new object[] {"String"},
                new object[] {Guid.NewGuid()}
            };

        public static IEnumerable<object[]> Comparison_RightLowerData() =>
            from leftMajor in integerValues
            from leftMinor in integerValues
            from rightMajor in integerValues
            from rightMinor in integerValues
            where leftMajor > rightMajor || (leftMajor == rightMajor && leftMinor > rightMinor)
            select new object[] { $"{leftMajor.ToString()}.{leftMinor.ToString()}", $"{rightMajor.ToString()}.{rightMinor.ToString()}" };

        public static IEnumerable<object[]> Comparison_EqualData() =>
            from leftMajor in integerValues
            from leftMinor in integerValues
            from rightMajor in integerValues
            from rightMinor in integerValues
            where leftMajor == rightMajor && leftMinor == rightMinor
            select new object[] { $"{leftMajor.ToString()}.{leftMinor.ToString()}", $"{rightMajor.ToString()}.{rightMinor.ToString()}" };

        public static IEnumerable<object[]> Comparison_LeftLowerData() =>
            from leftMajor in integerValues
            from leftMinor in integerValues
            from rightMajor in integerValues
            from rightMinor in integerValues
            where leftMajor < rightMajor || (leftMajor == rightMajor && leftMinor < rightMinor)
            select new object[] { $"{leftMajor.ToString()}.{leftMinor.ToString()}", $"{rightMajor.ToString()}.{rightMinor.ToString()}" };
    }
}
