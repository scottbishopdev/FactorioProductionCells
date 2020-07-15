using System;
using Xunit;
using FactorioProductionCells.Domain.ValueObjects;


namespace FactorioProductionCells.Domain.UnitTests.ValueObjects
{
    public class ModVersionTests
    {

        /*
        public static implicit operator string(ModVersion version)
        public static explicit operator ModVersion(String versionString)
        public override bool Equals(Object obj)
        public override int GetHashCode()
        public static bool operator ==(ModVersion left, ModVersion right)
        public static bool operator !=(ModVersion left, ModVersion right)
        public static bool operator >(ModVersion left, ModVersion right)
        public static bool operator >=(ModVersion left, ModVersion right)
        public static bool operator <(ModVersion left, ModVersion right)
        public static bool operator <=(ModVersion left, ModVersion right)
        public int CompareTo(Object obj)
        public override string ToString()
        protected override IEnumerable<object> GetAtomicValues()
        */

        #region For
        /*
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
        */
        #endregion

        #region ToStringOperator
        /*
        [Fact]
        public void ToStringOperator_WhenGivenFactorioVersion_ReturnsCorrectString()
        {
            String factorioVersionString = ZeroPointSeventeen;
            Assert.Equal("0.17", factorioVersionString);
        }
        */
        #endregion

        #region ToFactorioVersionOperator
        /*
        [Theory]
        [MemberData(nameof(ToFactorioVersionOperatorData))]
        public void ToFactorioVersionOperator_WhenGivenString_ReturnsCorrectFactorioVersion(String factorioVersionString, FactorioVersion factorioVersion)
        {
            FactorioVersion newFactorioVersion = (FactorioVersion)factorioVersionString;
            Assert.Equal(factorioVersion, newFactorioVersion);
        }
        */
        #endregion

        #region Equals

        #endregion

        #region GetHashCode

        #endregion

        #region EqualsOperator

        #endregion

        #region NotEqualsOperator

        #endregion

        #region GreaterThanOperator

        #endregion

        #region GreaterThanEqualsOperator

        #endregion

        #region LessThanOperator

        #endregion

        #region LessThanEqualsOperator

        #endregion

        #region CompareTo

        #endregion

        #region ToString

        #endregion

        #region GetAtomicValues

        #endregion
    }
}
