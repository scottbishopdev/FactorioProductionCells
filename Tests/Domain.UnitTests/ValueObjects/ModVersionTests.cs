using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FactorioProductionCells.Domain.ValueObjects;


namespace FactorioProductionCells.Domain.UnitTests.ValueObjects
{
    public class ModVersionTests
    {
        internal static ModVersion OnePointFourPointSixteen = ModVersion.For("1.4.16");
        internal static ModVersion OnePointFivePointOne = ModVersion.For("1.5.1");
        internal static ModVersion OnePointFourPointSixteenClone = ModVersion.For("1.4.16");
        internal static ModVersion ZeroPointFourPointThree = ModVersion.For("0.4.3");
        private static List<Int32> integerValues = new List<Int32> { 0, 1, 2, Int32.MaxValue };

        #region For
        [Theory]
        [InlineData("1.4.16", 1)]
        [InlineData("   1.4.16 ", 1)]
        [InlineData("1.0.0", 1)]
        [InlineData("43.883.34567", 43)]
        [InlineData("2147483647.14.0", Int32.MaxValue)]
        public void For_WhenGivenValidString_ReturnsCorrectMajorVersion(String modVersionString, Int32 major)
        {
            var modVersion = ModVersion.For(modVersionString);
            Assert.Equal(major, modVersion.Major);
        }

        [Theory]
        [InlineData("1.4.16", 4)]
        [InlineData("   1.4.16 ", 4)]
        [InlineData("1.0.0", 0)]
        [InlineData("43.883.34567", 883)]
        [InlineData("14.2147483647.0", Int32.MaxValue)]
        public void For_WhenGivenValidString_ReturnsCorrectMinorVersion(String modVersionString, Int32 minor)
        {
            var modVersion = ModVersion.For(modVersionString);
            Assert.Equal(minor, modVersion.Minor);
        }

        [Theory]
        [InlineData("1.4.16", 16)]
        [InlineData("   1.4.16 ", 16)]
        [InlineData("1.0.0", 0)]
        [InlineData("43.883.34567", 34567)]
        [InlineData("14.0.2147483647", Int32.MaxValue)]
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

        [Theory]
        [InlineData("1416")]
        [InlineData("0-416")]
        [InlineData("0-4-16")]
        [InlineData("1.4")]
        [InlineData("")]
        [InlineData("What're you going to do, bleed on me?   I'm *INVINCIBLE*!!!")]
        [InlineData("foo.bar.baz")]
        public void For_WhenGivenInvalidFormat_ThrowsArgumentException(String modVersionString)
        {
            var exception = Assert.Throws<ArgumentException>(() => ModVersion.For(modVersionString));
            Assert.Equal($"Unable to parse \"{modVersionString}\" to a valid ReleaseFileName due to formatting. (Parameter 'modVersionString')", exception.Message);
        }

        [Fact]
        public void For_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => ModVersion.For(null));
            Assert.Equal("A value for the mod version must be provided. (Parameter 'modVersionString')", exception.Message);
        }
        #endregion

        #region ToStringOperator
        [Fact]
        public void ToStringOperator_WhenGivenModVersion_ReturnsCorrectString()
        {
            String modVersionString = OnePointFourPointSixteen;
            Assert.Equal("1.4.16", modVersionString);
        }
        #endregion

        #region ToModVersionOperator
        [Theory]
        [MemberData(nameof(ToModVersionOperatorData))]
        public void ToModVersionOperator_WhenGivenString_ReturnsCorrectModVersion(String modVersionString, ModVersion modVersion)
        {
            ModVersion newModVersion = (ModVersion)modVersionString;
            Assert.Equal(modVersion, newModVersion);
        }
        #endregion

        #region Equals
        [Fact]
        public void Equals_WhenProvidedEqualVersion_ReturnsTrue()
        {
            Assert.True(OnePointFourPointSixteen.Equals((Object)OnePointFourPointSixteenClone));
        }

        [Fact]
        public void Equals_WhenProvidedNotEqualVersion_ReturnsFalse()
        {
            Assert.False(OnePointFourPointSixteen.Equals((Object)ZeroPointFourPointThree));
        }
        
        [Theory]
        [MemberData(nameof(Equals_InvalidTypesData))]
        public void Equals_WhenProvidedDifferentType_ThrowsArgumentException(Object right)
        {
            var exception = Assert.Throws<ArgumentException>(() => OnePointFourPointSixteen.Equals(right));
            Assert.Equal("Unable to compare the specified object to a ModVersion. (Parameter 'obj')", exception.Message);
        }
        #endregion

        #region GetHashCode
        [Fact]
        public void GetHashCode_MatchingVersions_ReturnSameHashCode()
        {
            Assert.Equal(OnePointFourPointSixteen.GetHashCode(), OnePointFourPointSixteenClone.GetHashCode());
        }

        [Fact]
        public void GetHashCode_NonMatchingVersions_ReturnDifferentHashCode()
        {
            Assert.NotEqual(OnePointFourPointSixteen.GetHashCode(), ZeroPointFourPointThree.GetHashCode());
        }
        #endregion

        #region EqualsOperator
        [Fact]
        public void EqualsOperator_WhenGivenMatchingVersions_ReturnsTrue()
        {
            Assert.True(OnePointFourPointSixteen == OnePointFourPointSixteenClone);
        }

        [Fact]
        public void EqualsOperator_WhenGivenNotMatchingVersions_ReturnsFalse()
        {
            Assert.False(OnePointFourPointSixteen == ZeroPointFourPointThree);
        }
        #endregion

        #region NotEqualsOperator
        [Fact]
        public void NotEqualsOperator_WhenGivenNotMatchingVersions_ReturnsTrue()
        {
            Assert.True(OnePointFourPointSixteen != ZeroPointFourPointThree);
        }
        
        [Fact]
        public void NotEqualsOperator_WhenGivenMatchingVersions_ReturnsFalse()
        {
            Assert.False(OnePointFourPointSixteen != OnePointFourPointSixteenClone);
        }
        #endregion

        #region GreaterThanOperator
        [Theory]
        [MemberData(nameof(Comparison_RightLowerData))]
        public void GreaterThanOperator_WhenRightIsLowerVersion_ReturnsTrue(String left, String right)
        {
            Assert.True(ModVersion.For(left) > ModVersion.For(right));
        }
        
        [Theory]
        [MemberData(nameof(Comparison_EqualData))]
        public void GreaterThanOperator_WhenVersionsAreEqual_ReturnsFalse(String left, String right)
        {
            Assert.False(ModVersion.For(left) > ModVersion.For(right));
        }

        [Theory]
        [MemberData(nameof(Comparison_LeftLowerData))]
        public void GreaterThanOperator_WhenLeftIsLowerVersion_ReturnsFalse(String left, String right)
        {
            Assert.False(ModVersion.For(left) > ModVersion.For(right));
        }
        #endregion

        #region GreaterThanEqualsOperator
        [Theory]
        [MemberData(nameof(Comparison_RightLowerData))]
        public void GreaterThanEqualsOperator_WhenRightIsLowerVersion_ReturnsTrue(String left, String right)
        {
            Assert.True(ModVersion.For(left) >= ModVersion.For(right));
        }
        
        [Theory]
        [MemberData(nameof(Comparison_EqualData))]
        public void GreaterThanEqualsOperator_WhenVersionsAreEqual_ReturnsTrue(String left, String right)
        {
            Assert.True(ModVersion.For(left) >= ModVersion.For(right));
        }

        [Theory]
        [MemberData(nameof(Comparison_LeftLowerData))]
        public void GreaterThanEqualsOperator_WhenLeftIsLowerVersion_ReturnsFalse(String left, String right)
        {
            Assert.False(ModVersion.For(left) >= ModVersion.For(right));
        }
        #endregion

        #region LessThanOperator
        [Theory]
        [MemberData(nameof(Comparison_RightLowerData))]
        public void LessThanOperator_WhenRightIsLowerVersion_ReturnsFalse(String left, String right)
        {
            Assert.False(ModVersion.For(left) < ModVersion.For(right));
        }
        
        [Theory]
        [MemberData(nameof(Comparison_EqualData))]
        public void LessThanOperator_WhenVersionsAreEqual_ReturnsFalse(String left, String right)
        {
            Assert.False(ModVersion.For(left) < ModVersion.For(right));
        }

        [Theory]
        [MemberData(nameof(Comparison_LeftLowerData))]
        public void LessThanOperator_WhenLeftIsLowerVersion_ReturnsTrue(String left, String right)
        {
            Assert.True(ModVersion.For(left) < ModVersion.For(right));
        }
        #endregion

        #region LessThanEqualsOperator
        [Theory]
        [MemberData(nameof(Comparison_RightLowerData))]
        public void LessThanEqualsOperator_WhenRightIsLowerVersion_ReturnsFalse(String left, String right)
        {
            Assert.False(ModVersion.For(left) <= ModVersion.For(right));
        }
        
        [Theory]
        [MemberData(nameof(Comparison_EqualData))]
        public void LessThanEqualsOperator_WhenVersionsAreEqual_ReturnsTrue(String left, String right)
        {
            Assert.True(ModVersion.For(left) <= ModVersion.For(right));
        }

        [Theory]
        [MemberData(nameof(Comparison_LeftLowerData))]
        public void LessThanEqualsOperator_WhenLeftIsLowerVersion_ReturnsTrue(String left, String right)
        {
            Assert.True(ModVersion.For(left) <= ModVersion.For(right));
        }
        #endregion

        #region CompareTo
        [Theory]
        [MemberData(nameof(Comparison_LeftLowerData))]
        public void CompareTo_WhenProvidedLargerVersion_ReturnsNegativeOne(String left, String right)
        {
            Assert.Equal(-1, ModVersion.For(left).CompareTo(ModVersion.For(right)));
        }

        [Theory]
        [MemberData(nameof(Comparison_EqualData))]
        public void CompareTo_WhenProvidedEqualVersion_ReturnsZero(String left, String right)
        {
            Assert.Equal(0, ModVersion.For(left).CompareTo(ModVersion.For(right)));
        }

        [Theory]
        [MemberData(nameof(Comparison_RightLowerData))]
        public void CompareTo_WhenProvidedSmallerVersion_ReturnsOne(String left, String right)
        {
            Assert.Equal(1, ModVersion.For(left).CompareTo(ModVersion.For(right)));
        }
        
        [Theory]
        [MemberData(nameof(Equals_InvalidTypesData))]
        public void CompareTo_WhenProvidedDifferentType_ThrowsArgumentException(Object right)
        {
            var exception = Assert.Throws<ArgumentException>(() => OnePointFourPointSixteen.CompareTo(right));
            Assert.Equal($"Unable to compare the specified object to a ModVersion. (Parameter 'obj')", exception.Message);
        }
        #endregion

        #region ToString
        [Fact]
        public void ToString_WhenGivenModVersion_ReturnsCorrectString()
        {
            Assert.Equal("1.4.16", OnePointFourPointSixteen.ToString());
        }
        #endregion

        #region GetAtomicValues
        [Fact]
        public void GetAtomicValues_WhenGivenModVersion_ReturnsCorrectValues()
        {
            var atomicValues = OnePointFourPointSixteen.GetAtomicValues();
            Assert.Equal(1, atomicValues.ElementAt(0));
            Assert.Equal(4, atomicValues.ElementAt(1));
            Assert.Equal(16, atomicValues.ElementAt(2));
        }
        #endregion
    
        public static IEnumerable<object[]> ToModVersionOperatorData =>
            new List<object[]>
            {
                new object[] {"1.4.16", ModVersion.For("1.4.16")}
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
            from leftPatch in integerValues
            from rightMajor in integerValues
            from rightMinor in integerValues
            from rightPatch in integerValues
            where leftMajor > rightMajor || (leftMajor == rightMajor && leftMinor > rightMinor) || (leftMajor == rightMajor && leftMinor == rightMinor && leftPatch > rightPatch)
            select new object[] { $"{leftMajor.ToString()}.{leftMinor.ToString()}.{leftPatch.ToString()}", $"{rightMajor.ToString()}.{rightMinor.ToString()}.{rightPatch.ToString()}" };

        public static IEnumerable<object[]> Comparison_EqualData() =>
            from leftMajor in integerValues
            from leftMinor in integerValues
            from leftPatch in integerValues
            from rightMajor in integerValues
            from rightMinor in integerValues
            from rightPatch in integerValues
            where leftMajor == rightMajor && leftMinor == rightMinor && leftPatch == rightPatch
            select new object[] { $"{leftMajor.ToString()}.{leftMinor.ToString()}.{leftPatch.ToString()}", $"{rightMajor.ToString()}.{rightMinor.ToString()}.{rightPatch.ToString()}" };

        public static IEnumerable<object[]> Comparison_LeftLowerData() =>
            from leftMajor in integerValues
            from leftMinor in integerValues
            from leftPatch in integerValues
            from rightMajor in integerValues
            from rightMinor in integerValues
            from rightPatch in integerValues
            where leftMajor < rightMajor || (leftMajor == rightMajor && leftMinor < rightMinor) || (leftMajor == rightMajor && leftMinor == rightMinor && leftPatch < rightPatch)
            select new object[] { $"{leftMajor.ToString()}.{leftMinor.ToString()}.{leftPatch.ToString()}", $"{rightMajor.ToString()}.{rightMinor.ToString()}.{rightPatch.ToString()}" };
    }
}
