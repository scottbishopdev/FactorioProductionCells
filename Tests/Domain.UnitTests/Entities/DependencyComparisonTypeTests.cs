using System;
using Xunit;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.Enums;
using FactorioProductionCells.TestData.Common;
using FactorioProductionCells.TestData.Domain.Entities;

namespace FactorioProductionCells.Domain.UnitTests.Entities
{
    public class DependencyComparisonTypeTests
    {
        #region Copy Constructor
        [Theory]
        [MemberData(nameof(DependencyComparisonTypeTestData.ValidStaticDependencyComparisonTypesWithId), MemberType=typeof(DependencyComparisonTypeTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectId(DependencyComparisonType dependencyType, DependencyComparisonTypeId expectedDependencyComparisonTypeId)
        {
            var testDependencyComparisonType = new DependencyComparisonType(dependencyType);
            Assert.Equal(expectedDependencyComparisonTypeId, testDependencyComparisonType.Id);
        }

        [Theory]
        [MemberData(nameof(DependencyComparisonTypeTestData.ValidStaticDependencyComparisonTypesWithName), MemberType=typeof(DependencyComparisonTypeTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectName(DependencyComparisonType dependencyType, String expectedName)
        {
            var testDependencyComparisonType = new DependencyComparisonType(dependencyType);
            Assert.Equal(expectedName, testDependencyComparisonType.Name);
        }

        [Fact]
        public void CopyConstructor_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Dependency(original: null));
            Assert.Equal("original is required. (Parameter 'original')", exception.Message);
        }
        #endregion

        #region Enum Constructor
        [Theory]
        [MemberData(nameof(DependencyComparisonTypeTestData.ValidStaticDependencyComparisonTypeIdsWithEnumValues), MemberType=typeof(DependencyComparisonTypeTestData))]
        public void EnumConstructor_WhenGivenEnum_ReturnsCorrectId(DependencyComparisonTypeId enumId, DependencyComparisonTypeId expectedEnumId)
        {
            var testDependencyComparisonType = new DependencyComparisonType(enumId);
            Assert.Equal(expectedEnumId, testDependencyComparisonType.Id);
        }

        [Theory]
        [MemberData(nameof(DependencyComparisonTypeTestData.ValidStaticDependencyComparisonTypeIdsWithNames), MemberType=typeof(DependencyComparisonTypeTestData))]
        public void EnumConstructor_WhenGivenEnum_ReturnsCorrectName(DependencyComparisonTypeId enumId, String expectedName)
        {
            var testDependencyComparisonType = new DependencyComparisonType(enumId);
            Assert.Equal(expectedName, testDependencyComparisonType.Name);
        }
        #endregion

        #region Int Constructor
        [Theory]
        [MemberData(nameof(DependencyComparisonTypeTestData.ValidStaticDependencyComparisonTypeIntIdsWithIds), MemberType=typeof(DependencyComparisonTypeTestData))]
        public void IntConstructor_WhenGivenValidInt_ReturnsCorrectId(Int32 intId, DependencyComparisonTypeId expectedId)
        {
            var testDependencyComparisonType = new DependencyComparisonType(intId);
            Assert.Equal(expectedId, testDependencyComparisonType.Id);
        }

        [Theory]
        [MemberData(nameof(DependencyComparisonTypeTestData.ValidStaticDependencyComparisonTypeIntIdsWithNames), MemberType=typeof(DependencyComparisonTypeTestData))]
        public void IntConstructor_WhenGivenValidInt_ReturnsCorrectName(Int32 intId, String expectedName)
        {
            var testDependencyComparisonType = new DependencyComparisonType(intId);
            Assert.Equal(expectedName, testDependencyComparisonType.Name);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(6)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void IntConstructor_WhenGivenInvalidId_ThrowsArgumentOutOfRangeException(int intId)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new DependencyComparisonType(intId));
            Assert.Equal($"Unable to parse the supplied id {intId} into a DependencyComparisonType. (Parameter 'intId')", exception.Message);
        }
        #endregion
        
        #region For
        [Theory]
        [MemberData(nameof(DependencyComparisonTypeTestData.ValidStaticStringsWithEnumValue), MemberType=typeof(DependencyComparisonTypeTestData))]
        public void For_WhenGivenValidString_ReturnsCorrectId(String dependencyComparisonTypeString, DependencyComparisonTypeId expectedId)
        {
            var testDependencyComparisonType = DependencyComparisonType.For(dependencyComparisonTypeString);
            Assert.Equal(expectedId, testDependencyComparisonType.Id);
        }

        [Theory]
        [MemberData(nameof(DependencyComparisonTypeTestData.ValidStaticStringsWithEnumName), MemberType=typeof(DependencyComparisonTypeTestData))]
        public void For_WhenGivenValidString_ReturnsCorrectName(String dependencyComparisonTypeString, String expectedName)
        {
            var testDependencyComparisonType = DependencyComparisonType.For(dependencyComparisonTypeString);
            Assert.Equal(expectedName, testDependencyComparisonType.Name);
        }

        [Fact]
        public void For_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => DependencyComparisonType.For(null));
            Assert.Equal("dependencyComparisonTypeString is required. (Parameter 'dependencyComparisonTypeString')", exception.Message);
        }

        [Theory]
        [MemberData(nameof(CommonTestData.EmptyAndWhitespaceStrings), MemberType=typeof(CommonTestData))]
        public void For_WhenGivenEmptyStringOrWhitespace_ThrowsArgumentException(String dependencyComparisonTypeString)
        {
            var exception = Assert.Throws<ArgumentException>(() => DependencyComparisonType.For(dependencyComparisonTypeString));
            Assert.Equal($"dependencyComparisonTypeString may not be empty. (Parameter 'dependencyComparisonTypeString')", exception.Message);
        }

        [Theory]
        [InlineData("?")]
        [InlineData("Hes passed on! This parrot is no more! He has ceased to be!")]
        [InlineData("`/*")]
        public void For_WhenGivenInvalidString_ThrowsArgumentException(String dependencyComparisonTypeString)
        {
            var exception = Assert.Throws<ArgumentException>(() => DependencyComparisonType.For(dependencyComparisonTypeString));
            Assert.Equal($"The specified string \"{dependencyComparisonTypeString}\" could not be parsed into a valid DependencyComparisonType. (Parameter 'dependencyComparisonTypeString')", exception.Message);
        }
        #endregion

        #region ToString
        [Theory]
        [MemberData(nameof(DependencyComparisonTypeTestData.ValidStaticDependencyComparisonTypesFromForWithStrings), MemberType=typeof(DependencyComparisonTypeTestData))]
        public void ToString_WhenGivenEnumId_ReturnsCorrectString(DependencyComparisonType dependencyComparisonType, String expectedString)
        {
            Assert.Equal(expectedString, dependencyComparisonType.ToString());
        }
        #endregion

        #region Equals

        [Theory]
        [MemberData(nameof(DependencyComparisonTypeTestData.ValidStaticEqualDependencyComparisonTypePairs), MemberType=typeof(DependencyComparisonTypeTestData))]
        public void Equals_WhenProvidedEqualDependencyComparisonTypes_ReturnsTrue(DependencyComparisonType left, DependencyComparisonType right)
        {
            Assert.True(left.Equals(right));
        }

        [Theory]
        [MemberData(nameof(DependencyComparisonTypeTestData.ValidStaticNonEqualDependencyComparisonTypePairs), MemberType=typeof(DependencyComparisonTypeTestData))]
        public void Equals_WhenProvidedNotEqualDependencyComparisonTypes_ReturnsFalse(DependencyComparisonType left, DependencyComparisonType right)
        {
            Assert.False(left.Equals(right));
        }

        [Fact]
        public void Equals_WhenGivenNull_ReturnsFalse()
        {
            var greaterThan = new DependencyComparisonType(DependencyComparisonTypeId.GreaterThan);
            Assert.False(greaterThan.Equals(null));
        }
        #endregion
    }
}
