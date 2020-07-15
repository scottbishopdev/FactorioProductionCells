using System;
using Xunit;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.Enums;

namespace FactorioProductionCells.Domain.UnitTests.Entities
{
    public class DependencyComparisonTypeTests
    {
        #region EnumConstructor
        [Theory]
        [InlineData(DependencyComparisonTypeId.LessThan, DependencyComparisonTypeId.LessThan)]
        [InlineData(DependencyComparisonTypeId.LessThanOrEqualTo, DependencyComparisonTypeId.LessThanOrEqualTo)]
        [InlineData(DependencyComparisonTypeId.EqualTo, DependencyComparisonTypeId.EqualTo)]
        [InlineData(DependencyComparisonTypeId.GreaterThan, DependencyComparisonTypeId.GreaterThan)]
        [InlineData(DependencyComparisonTypeId.GreaterThanOrEqualTo, DependencyComparisonTypeId.GreaterThanOrEqualTo)]
        public void EnumConstructor_WhenGivenEnum_ReturnsCorrectEnum(DependencyComparisonTypeId enumId, DependencyComparisonTypeId type)
        {
            var dependencyComparisonType = new DependencyComparisonType(enumId);
            Assert.Equal(type, dependencyComparisonType.Id);
        }
        
        [Theory]
        [InlineData(DependencyComparisonTypeId.LessThan, "LessThan")]
        [InlineData(DependencyComparisonTypeId.LessThanOrEqualTo, "LessThanOrEqualTo")]
        [InlineData(DependencyComparisonTypeId.EqualTo, "EqualTo")]
        [InlineData(DependencyComparisonTypeId.GreaterThan, "GreaterThan")]
        [InlineData(DependencyComparisonTypeId.GreaterThanOrEqualTo, "GreaterThanOrEqualTo")]
        public void EnumConstructor_WhenGivenEnum_ReturnsCorrectName(DependencyComparisonTypeId enumId, String name)
        {
            var dependencyComparisonType = new DependencyComparisonType(enumId);
            Assert.Equal(name, dependencyComparisonType.Name);
        }
        #endregion

        #region IntConstructor
        [Theory]
        [InlineData(0, DependencyComparisonTypeId.LessThan)]
        [InlineData(1, DependencyComparisonTypeId.LessThanOrEqualTo)]
        [InlineData(2, DependencyComparisonTypeId.EqualTo)]
        [InlineData(3, DependencyComparisonTypeId.GreaterThan)]
        [InlineData(4, DependencyComparisonTypeId.GreaterThanOrEqualTo)]
        public void IntConstructor_WhenGivenValidId_ReturnsCorrectEnum(int intId, DependencyComparisonTypeId type)
        {
            var dependencyComparisonType = new DependencyComparisonType(intId);
            Assert.Equal(type, dependencyComparisonType.Id);
        }

        [Theory]
        [InlineData(0, "LessThan")]
        [InlineData(1, "LessThanOrEqualTo")]
        [InlineData(2, "EqualTo")]
        [InlineData(3, "GreaterThan")]
        [InlineData(4, "GreaterThanOrEqualTo")]
        public void IntConstructor_WhenGivenValidId_ReturnsCorrectName(int intId, String name)
        {
            var dependencyComparisonType = new DependencyComparisonType(intId);
            Assert.Equal(name, dependencyComparisonType.Name);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(5)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void IntConstructor_WhenGivenInvalidId_ThrowsArgumentOutOfRangeException(int intId)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DependencyComparisonType(intId));
        }
        #endregion
        
        #region For
        [Theory]
        [InlineData("<", DependencyComparisonTypeId.LessThan)]
        [InlineData("<=", DependencyComparisonTypeId.LessThanOrEqualTo)]
        [InlineData("=", DependencyComparisonTypeId.EqualTo)]
        [InlineData(">", DependencyComparisonTypeId.GreaterThan)]
        [InlineData(">=", DependencyComparisonTypeId.GreaterThanOrEqualTo)]
        [InlineData(" >=    ", DependencyComparisonTypeId.GreaterThanOrEqualTo)]
        public void For_WhenGivenValidString_ReturnsCorrectEnum(String dependencyComparisonTypeString, DependencyComparisonTypeId type)
        {
            var dependencyComparisonType = DependencyComparisonType.For(dependencyComparisonTypeString);
            Assert.Equal(type, dependencyComparisonType.Id);
        }

        [Theory]
        [InlineData("<", "LessThan")]
        [InlineData("<=", "LessThanOrEqualTo")]
        [InlineData("=", "EqualTo")]
        [InlineData(">", "GreaterThan")]
        [InlineData(">=", "GreaterThanOrEqualTo")]
        [InlineData(" >=    ", "GreaterThanOrEqualTo")]
        public void For_WhenGivenValidString_ReturnsCorrectName(String dependencyComparisonTypeString, String name)
        {
            var dependencyComparisonType = DependencyComparisonType.For(dependencyComparisonTypeString);
            Assert.Equal(name, dependencyComparisonType.Name);
        }
        
        [Theory]
        [InlineData("?")]
        [InlineData("!=")]
        [InlineData("")]
        [InlineData("Hes passed on! This parrot is no more! He has ceased to be!")]
        [InlineData("   `/*")]
        [InlineData(null)]
        public void For_WhenGivenInvalidString_ThrowsArgumentException(String dependencyComparisonTypeString)
        {
            Assert.Throws<ArgumentException>(() => DependencyComparisonType.For(dependencyComparisonTypeString));
        }
        #endregion

        #region ToString
        [Theory]
        [InlineData(DependencyComparisonTypeId.LessThan, "<")]
        [InlineData(DependencyComparisonTypeId.LessThanOrEqualTo, "<=")]
        [InlineData(DependencyComparisonTypeId.EqualTo, "=")]
        [InlineData(DependencyComparisonTypeId.GreaterThan, ">")]
        [InlineData(DependencyComparisonTypeId.GreaterThanOrEqualTo, ">=")]
        public void ToString_WhenGivenEnumId_ReturnsCorrectString(DependencyComparisonTypeId enumId, String dependencyComparisonTypeString)
        {
            var dependencyComparisonType = new DependencyComparisonType(enumId);
            Assert.Equal(dependencyComparisonTypeString, dependencyComparisonType.ToString());
        }
        #endregion
    }
}
