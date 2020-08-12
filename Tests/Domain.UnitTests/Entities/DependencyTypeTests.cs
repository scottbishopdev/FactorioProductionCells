using System;
using Xunit;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.Enums;
using FactorioProductionCells.TestData.Domain.Entities;

namespace FactorioProductionCells.Domain.UnitTests.Entities
{
    public class DependencyTypeTests
    {
        #region Copy Constructor
        [Theory]
        [MemberData(nameof(DependencyTypeTestData.ValidStaticDependencyTypesWithId), MemberType=typeof(DependencyTypeTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectId(DependencyType dependencyType, DependencyTypeId expectedDependencyTypeId)
        {
            var testDependencyType = new DependencyType(dependencyType);
            Assert.Equal(expectedDependencyTypeId, testDependencyType.Id);
        }

        [Theory]
        [MemberData(nameof(DependencyTypeTestData.ValidStaticDependencyTypesWithName), MemberType=typeof(DependencyTypeTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectName(DependencyType dependencyType, String expectedName)
        {
            var testDependencyType = new DependencyType(dependencyType);
            Assert.Equal(expectedName, testDependencyType.Name);
        }

        [Fact]
        public void CopyConstructor_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new DependencyType(original: null));
            Assert.Equal("original is required. (Parameter 'original')", exception.Message);
        }
        #endregion

        #region Enum Constructor
        [Theory]
        [MemberData(nameof(DependencyTypeTestData.ValidStaticDependencyTypeIdsWithEnumValues), MemberType=typeof(DependencyTypeTestData))]
        public void EnumConstructor_WhenGivenEnum_ReturnsCorrectId(DependencyTypeId enumId, DependencyTypeId expectedEnumId)
        {
            var testDependencyType = new DependencyType(enumId);
            Assert.Equal(expectedEnumId, testDependencyType.Id);
        }

        [Theory]
        [MemberData(nameof(DependencyTypeTestData.ValidStaticDependencyTypeIdsWithNames), MemberType=typeof(DependencyTypeTestData))]
        public void EnumConstructor_WhenGivenEnum_ReturnsCorrectName(DependencyTypeId enumId, String expectedName)
        {
            var testDependencyType = new DependencyType(enumId);
            Assert.Equal(expectedName, testDependencyType.Name);
        }
        #endregion

        #region Int Constructor
        [Theory]
        [MemberData(nameof(DependencyTypeTestData.ValidStaticDependencyTypeIntIdsWithIds), MemberType=typeof(DependencyTypeTestData))]
        public void IntConstructor_WhenGivenValidInt_ReturnsCorrectId(Int32 intId, DependencyTypeId expectedId)
        {
            var testDependencyType = new DependencyType(intId);
            Assert.Equal(expectedId, testDependencyType.Id);
        }

        [Theory]
        [MemberData(nameof(DependencyTypeTestData.ValidStaticDependencyTypeIntIdsWithNames), MemberType=typeof(DependencyTypeTestData))]
        public void IntConstructor_WhenGivenValidInt_ReturnsCorrectName(Int32 intId, String expectedName)
        {
            var testDependencyType = new DependencyType(intId);
            Assert.Equal(expectedName, testDependencyType.Name);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(5)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void IntConstructor_WhenGivenInvalidId_ThrowsArgumentOutOfRangeException(int intId)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new DependencyType(intId));
            Assert.Equal($"Unable to parse the supplied id {intId} into a DependencyType. (Parameter 'intId')", exception.Message);
        }
        #endregion
        
        #region For
        [Theory]
        [MemberData(nameof(DependencyTypeTestData.ValidStaticStringsWithEnumValue), MemberType=typeof(DependencyTypeTestData))]
        public void For_WhenGivenValidString_ReturnsCorrectId(String dependencyTypeString, DependencyTypeId expectedId)
        {
            var testDependencyType = DependencyType.For(dependencyTypeString);
            Assert.Equal(expectedId, testDependencyType.Id);
        }

        [Theory]
        [MemberData(nameof(DependencyTypeTestData.ValidStaticStringsWithEnumName), MemberType=typeof(DependencyTypeTestData))]
        public void For_WhenGivenValidString_ReturnsCorrectName(String dependencyTypeString, String expectedName)
        {
            var testDependencyType = DependencyType.For(dependencyTypeString);
            Assert.Equal(expectedName, testDependencyType.Name);
        }

        [Fact]
        public void For_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => DependencyType.For(null));
            Assert.Equal("dependencyTypeString is required. (Parameter 'dependencyTypeString')", exception.Message);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData("\r")]
        [InlineData("\n")]
        [InlineData("\t")]
        [InlineData("\v")]
        public void For_WhenGivenEmptyStringOrWhitespace_ThrowsArgumentException(String dependencyTypeString)
        {
            var exception = Assert.Throws<ArgumentException>(() => DependencyType.For(dependencyTypeString));
            Assert.Equal($"dependencyTypeString may not be whitespace. (Parameter 'dependencyTypeString')", exception.Message);
        }

        [Theory]
        [InlineData(">")]
        [InlineData("(")]
        [InlineData("Hes passed on! This parrot is no more! He has ceased to be!")]
        [InlineData("`/*")]
        public void For_WhenGivenInvalidString_ThrowsArgumentException(String dependencyTypeString)
        {
            var exception = Assert.Throws<ArgumentException>(() => DependencyType.For(dependencyTypeString));
            Assert.Equal($"The specified string \"{dependencyTypeString}\" could not be parsed into a valid DependencyType. (Parameter 'dependencyTypeString')", exception.Message);
        }
        #endregion

        #region ToString
        [Theory]
        [MemberData(nameof(DependencyTypeTestData.ValidStaticDependencyTypesFromForWithStrings), MemberType=typeof(DependencyTypeTestData))]
        public void ToString_WhenGivenEnumId_ReturnsCorrectString(DependencyType dependencyType, String expectedString)
        {
            Assert.Equal(expectedString, dependencyType.ToString());
        }
        #endregion

        #region Equals
        [Theory]
        [MemberData(nameof(DependencyTypeTestData.ValidStaticEqualDependencyTypePairs), MemberType=typeof(DependencyTypeTestData))]
        public void Equals_WhenProvidedEqualDependencyTypes_ReturnsTrue(DependencyType left, DependencyType right)
        {
            Assert.True(left.Equals(right));
        }

        [Theory]
        [MemberData(nameof(DependencyTypeTestData.ValidStaticNonEqualDependencyTypePairs), MemberType=typeof(DependencyTypeTestData))]
        public void Equals_WhenProvidedNotEqualDependencyTypes_ReturnsFalse(DependencyType left, DependencyType right)
        {
            Assert.False(left.Equals(right));
        }

        [Fact]
        public void Equals_WhenGivenNull_ReturnsFalse()
        {
            var hiddenOptional = new DependencyType(DependencyTypeId.HiddenOptional);
            Assert.False(hiddenOptional.Equals(null));
        }
        #endregion
    }
}
