using System;
using Xunit;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.Enums;

namespace FactorioProductionCells.Domain.UnitTests.Entities
{
    public class DependencyTypeTests
    {
        #region EnumConstructor
        [Theory]
        [InlineData(DependencyTypeId.Required, DependencyTypeId.Required)]
        [InlineData(DependencyTypeId.Incompatibility, DependencyTypeId.Incompatibility)]
        [InlineData(DependencyTypeId.Optional, DependencyTypeId.Optional)]
        [InlineData(DependencyTypeId.HiddenOptional, DependencyTypeId.HiddenOptional)]
        public void EnumConstructor_WhenGivenEnum_ReturnsCorrectEnum(DependencyTypeId enumId, DependencyTypeId type)
        {
            var dependencyType = new DependencyType(enumId);
            Assert.Equal(type, dependencyType.Id);
        }
        
        [Theory]
        [InlineData(DependencyTypeId.Required, "Required")]
        [InlineData(DependencyTypeId.Incompatibility, "Incompatibility")]
        [InlineData(DependencyTypeId.Optional, "Optional")]
        [InlineData(DependencyTypeId.HiddenOptional, "HiddenOptional")]
        public void EnumConstructor_WhenGivenEnum_ReturnsCorrectName(DependencyTypeId enumId, String name)
        {
            var dependencyType = new DependencyType(enumId);
            Assert.Equal(name, dependencyType.Name);
        }
        #endregion

        #region IntConstructor
        [Theory]
        [InlineData(0, DependencyTypeId.Required)]
        [InlineData(1, DependencyTypeId.Incompatibility)]
        [InlineData(2, DependencyTypeId.Optional)]
        [InlineData(3, DependencyTypeId.HiddenOptional)]
        public void IntConstructor_WhenGivenValidId_ReturnsCorrectEnum(int intId, DependencyTypeId type)
        {
            var dependencyType = new DependencyType(intId);
            Assert.Equal(type, dependencyType.Id);
        }

        [Theory]
        [InlineData(0, "Required")]
        [InlineData(1, "Incompatibility")]
        [InlineData(2, "Optional")]
        [InlineData(3, "HiddenOptional")]
        public void IntConstructor_WhenGivenValidId_ReturnsCorrectName(int intId, String name)
        {
            var dependencyType = new DependencyType(intId);
            Assert.Equal(name, dependencyType.Name);
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
        [InlineData("", DependencyTypeId.Required)]
        [InlineData("!", DependencyTypeId.Incompatibility)]
        [InlineData("?", DependencyTypeId.Optional)]
        [InlineData("(?)", DependencyTypeId.HiddenOptional)]
        [InlineData("   ? ", DependencyTypeId.Optional)]
        public void For_WhenGivenValidString_ReturnsCorrectEnum(String dependencyTypeString, DependencyTypeId type)
        {
            var dependencyType = DependencyType.For(dependencyTypeString);
            Assert.Equal(type, dependencyType.Id);
        }

        [Theory]
        [InlineData("", "Required")]
        [InlineData("!", "Incompatibility")]
        [InlineData("?", "Optional")]
        [InlineData("(?)", "HiddenOptional")]
        [InlineData("   ? ", "Optional")]
        public void For_WhenGivenValidString_ReturnsCorrectName(String dependencyTypeString, String name)
        {
            var dependencyType = DependencyType.For(dependencyTypeString);
            Assert.Equal(name, dependencyType.Name);
        }
        
        [Theory]
        [InlineData(">")]
        [InlineData("(")]
        [InlineData("Hes passed on! This parrot is no more! He has ceased to be!")]
        [InlineData("   `/*")]
        [InlineData(null)]
        public void For_WhenGivenInvalidString_ThrowsArgumentException(String dependencyTypeString)
        {
            var exception = Assert.Throws<ArgumentException>(() => DependencyType.For(dependencyTypeString));
            Assert.Equal($"The specified string \"{dependencyTypeString?.Trim()}\" could not be parsed into a valid DependencyType. (Parameter 'dependencyTypeString')", exception.Message);
        }
        #endregion

        #region ToString
        [Theory]
        [InlineData(DependencyTypeId.Required, "")]
        [InlineData(DependencyTypeId.Incompatibility, "!")]
        [InlineData(DependencyTypeId.Optional, "?")]
        [InlineData(DependencyTypeId.HiddenOptional, "(?)")]
        public void ToString_WhenGivenEnumId_ReturnsCorrectString(DependencyTypeId enumId, String dependencyTypeString)
        {
            var dependencyType = new DependencyType(enumId);
            Assert.Equal(dependencyTypeString, dependencyType.ToString());
        }
        #endregion
    }
}
