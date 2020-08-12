using System;
using Xunit;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.ValueObjects;
using FactorioProductionCells.TestData.Common;
using FactorioProductionCells.TestData.Domain.Entities;

namespace FactorioProductionCells.Domain.UnitTests.Entities
{
    public class DependencyTests
    {
        #region Copy Constructor
        [Theory]
        [MemberData(nameof(DependencyTestData.ValidStaticDependenciesWithDependencyType), MemberType=typeof(DependencyTestData))]
        [MemberData(nameof(DependencyTestData.ValidRandomDependenciesWithDependencyType), MemberType=typeof(DependencyTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectDependencyType(Dependency dependency, DependencyType expectedDependencyType)
        {
            var testDependency = new Dependency(dependency);
            Assert.Equal(expectedDependencyType, testDependency.DependencyType);
        }

        [Theory]
        [MemberData(nameof(DependencyTestData.ValidStaticDependenciesWithDependentModName), MemberType=typeof(DependencyTestData))]
        [MemberData(nameof(DependencyTestData.ValidRandomDependenciesWithDependentModName), MemberType=typeof(DependencyTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectDependentModName(Dependency dependency, String expectedDependentModName)
        {
            var testDependency = new Dependency(dependency);
            Assert.Equal(expectedDependentModName, testDependency.DependentModName);
        }

        [Theory]
        [MemberData(nameof(DependencyTestData.ValidStaticDependenciesWithDependencyComparisonType), MemberType=typeof(DependencyTestData))]
        [MemberData(nameof(DependencyTestData.ValidRandomDependenciesWithDependencyComparisonType), MemberType=typeof(DependencyTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectDependencyComparisonType(Dependency dependency, DependencyComparisonType expectedDependencyComparisonType)
        {
            var testDependency = new Dependency(dependency);
            Assert.Equal(expectedDependencyComparisonType, testDependency.DependencyComparisonType);
        }

        [Theory]
        [MemberData(nameof(DependencyTestData.ValidStaticDependenciesWithDependentModVersion), MemberType=typeof(DependencyTestData))]
        [MemberData(nameof(DependencyTestData.ValidRandomDependenciesWithDependentModVersion), MemberType=typeof(DependencyTestData))]
        public void CopyConstructor_WhenValidParameters_ReturnsCorrectDependentModVersion(Dependency dependency, ModVersion expectedDependentModVersion)
        {
            var testDependency = new Dependency(dependency);
            Assert.Equal(expectedDependentModVersion, testDependency.DependentModVersion);
        }

        [Fact]
        public void CopyConstructor_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Dependency(null));
            Assert.Equal("original is required. (Parameter 'original')", exception.Message);
        }
        #endregion
        
        #region Individual Value Constructor
        [Theory]
        [MemberData(nameof(DependencyTestData.ValidStaticDependenciesCreationProperties), MemberType=typeof(DependencyTestData))]
        [MemberData(nameof(DependencyTestData.ValidRandomDependenciesCreationProperties), MemberType=typeof(DependencyTestData))]
        public void DependencyConstructor_WhenValidParameters_ReturnsCorrectDependencyType(DependencyType dependencyType, String dependentModName, DependencyComparisonType dependencyComparisonType, ModVersion dependentModVersion)
        {
            var testDependency = new Dependency(dependencyType, dependentModName, dependencyComparisonType, dependentModVersion);
            Assert.Equal(dependencyType, testDependency.DependencyType);
        }

        [Theory]
        [MemberData(nameof(DependencyTestData.ValidStaticDependenciesCreationProperties), MemberType=typeof(DependencyTestData))]
        [MemberData(nameof(DependencyTestData.ValidRandomDependenciesCreationProperties), MemberType=typeof(DependencyTestData))]
        public void DependencyConstructor_WhenValidParameters_ReturnsCorrectDependentModName(DependencyType dependencyType, String dependentModName, DependencyComparisonType dependencyComparisonType, ModVersion dependentModVersion)
        {
            var testDependency = new Dependency(dependencyType, dependentModName, dependencyComparisonType, dependentModVersion);
            Assert.Equal(dependentModName, testDependency.DependentModName);
        }

        [Theory]
        [MemberData(nameof(DependencyTestData.ValidStaticDependenciesCreationProperties), MemberType=typeof(DependencyTestData))]
        [MemberData(nameof(DependencyTestData.ValidRandomDependenciesCreationProperties), MemberType=typeof(DependencyTestData))]
        public void DependencyConstructor_WhenValidParameters_ReturnsCorrectDependencyComparisonType(DependencyType dependencyType, String dependentModName, DependencyComparisonType dependencyComparisonType, ModVersion dependentModVersion)
        {
            var testDependency = new Dependency(dependencyType, dependentModName, dependencyComparisonType, dependentModVersion);
            Assert.Equal(dependencyComparisonType, testDependency.DependencyComparisonType);
        }

        [Theory]
        [MemberData(nameof(DependencyTestData.ValidStaticDependenciesCreationProperties), MemberType=typeof(DependencyTestData))]
        [MemberData(nameof(DependencyTestData.ValidRandomDependenciesCreationProperties), MemberType=typeof(DependencyTestData))]
        public void DependencyConstructor_WhenValidParameters_ReturnsCorrectDependentModVersion(DependencyType dependencyType, String dependentModName, DependencyComparisonType dependencyComparisonType, ModVersion dependentModVersion)
        {
            var testDependency = new Dependency(dependencyType, dependentModName, dependencyComparisonType, dependentModVersion);
            Assert.Equal(dependentModVersion, testDependency.DependentModVersion);
        }
        #endregion

        #region For
        [Theory]
        [MemberData(nameof(DependencyTestData.ValidStaticStringsWithDependencyType), MemberType=typeof(DependencyTestData))]
        [MemberData(nameof(DependencyTestData.ValidRandomStringsWithDependencyType), MemberType=typeof(DependencyTestData))]
        public void For_WhenGivenValidString_ReturnsCorrectDependencyType(String dependencyString, DependencyType expectedDependencyType)
        {
            var dependency = Dependency.For(dependencyString);
            Assert.Equal(expectedDependencyType, dependency.DependencyType);
        }

        [Theory]
        [MemberData(nameof(DependencyTestData.ValidStaticStringsWithDependentModName), MemberType=typeof(DependencyTestData))]
        [MemberData(nameof(DependencyTestData.ValidRandomStringsWithDependentModName), MemberType=typeof(DependencyTestData))]
        public void For_WhenGivenValidString_ReturnsCorrectDependentModName(String dependencyString, String expectedDependentModName)
        {
            var dependency = Dependency.For(dependencyString);
            Assert.Equal(expectedDependentModName, dependency.DependentModName);
        }

        [Theory]
        [MemberData(nameof(DependencyTestData.ValidStaticStringsWithDependencyComparisonType), MemberType=typeof(DependencyTestData))]
        [MemberData(nameof(DependencyTestData.ValidRandomStringsWithDependencyComparisonType), MemberType=typeof(DependencyTestData))]
        public void For_WhenGivenValidString_ReturnsCorrectDependencyComparisonType(String dependencyString, DependencyComparisonType expectedDependencyComparisonType)
        {
            var dependency = Dependency.For(dependencyString);
            Assert.Equal(expectedDependencyComparisonType, dependency.DependencyComparisonType);
        }

        [Theory]
        [MemberData(nameof(DependencyTestData.ValidStaticStringsWithDependentModVersion), MemberType=typeof(DependencyTestData))]
        [MemberData(nameof(DependencyTestData.ValidRandomStringsWithDependentModVersion), MemberType=typeof(DependencyTestData))]
        [MemberData(nameof(DependencyTestData.ValidDependencyStringWithFactorioVersionFormattedModVersion), MemberType=typeof(DependencyTestData))]
        [MemberData(nameof(DependencyTestData.RandomDependencyStringsWithExpectedModVersion), parameters: 10, MemberType=typeof(DependencyTestData))]
        public void For_WhenGivenValidString_ReturnsCorrectDependentModVersion(String dependencyString, ModVersion expectedModVersion)
        {
            var dependency = Dependency.For(dependencyString);
            Assert.Equal(expectedModVersion, dependency.DependentModVersion);
        }

        [Fact]
        public void For_WhenGivenNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => Dependency.For(null));
            Assert.Equal("dependencyString is required. (Parameter 'dependencyString')", exception.Message);
        }

        [Theory]
        [MemberData(nameof(CommonTestData.EmptyAndWhitespaceStrings), MemberType=typeof(CommonTestData))]
        public void For_WhenGivenEmptyStringOrWhitespace_ThrowsArgumentException(String dependencyString)
        {
            var exception = Assert.Throws<ArgumentException>(() => Dependency.For(dependencyString));
            Assert.Equal("dependencyString may not be empty. (Parameter 'dependencyString')", exception.Message);
        }

        [Theory]
        [InlineData("?? base >= 0.14.0")]
        [InlineData("? ? base >= 0.14.0")]
        [InlineData(">= base ! 0.14.0")]
        [InlineData(" >= 0.14.0")]
        [InlineData(">= 0.14.0")]
        [InlineData("base  0.14.0")]
        [InlineData("base 0.14.0")]
        [InlineData("base0.14.0")]
        [InlineData("base >= ")]
        [InlineData("base >=")]
        [InlineData("base >= 0.14.0.1.2.3.4.5")]
        [InlineData("? b!as%e >= 0.14.0")]
        [InlineData("? base XX 0.14.0")]
        [InlineData("? base => 0.14.0")]
        [InlineData("What're you going to do, bleed on me?   I'm *INVINCIBLE*!!!")]
        public void For_WhenGivenInvalidFormat_ThrowsArgumentException(String dependencyString)
        {
            var exception = Assert.Throws<ArgumentException>(() => Dependency.For(dependencyString));
            Assert.Equal($"Unable to parse \"{dependencyString}\" to a valid Dependency due to formatting. (Parameter 'dependencyString')", exception.Message);
        }

        [Theory]
        [MemberData(nameof(DependencyTestData.RandomDependenciesWithModNameTooLong), parameters: 6, MemberType=typeof(DependencyTestData))]
        public void For_WhenGivenModNameTooLong_ThrowsArgumentException(String dependencyString)
        {
            var exception = Assert.Throws<ArgumentException>(() => Dependency.For(dependencyString));
            Assert.Equal($"The mod name specified exceeds the maximum length of {Mod.NameLength}. (Parameter 'dependencyString')", exception.Message);
        }
        #endregion

        #region ToString
        [Theory]
        [MemberData(nameof(DependencyTestData.ValidStaticDependenciesFromForWithStrings), MemberType=typeof(DependencyTestData))]
        [MemberData(nameof(DependencyTestData.ValidRandomDependenciesFromForWithStrings), MemberType=typeof(DependencyTestData))]
        public void ToString_WhenGivenReleaseFileName_ReturnsCorrectString(Dependency dependency, String expected)
        {
            Assert.Equal(expected, dependency.ToString());
        }
        #endregion

        #region Equals
        [Theory]
        [MemberData(nameof(DependencyTestData.ValidStaticEqualDependencyPairs), MemberType=typeof(DependencyTestData))]
        [MemberData(nameof(DependencyTestData.ValidRandomEqualDependencyPairs), MemberType=typeof(DependencyTestData))]
        public void Equals_WhenProvidedEqualDependencies_ReturnsTrue(Dependency left, Dependency right)
        {
            Assert.True(left.Equals(right));
        }

        [Theory]
        [MemberData(nameof(DependencyTestData.ValidStaticNonEqualDependencyPairs), MemberType=typeof(DependencyTestData))]
        public void Equals_WhenProvidedNotEqualDependencies_ReturnsFalse(Dependency left, Dependency right)
        {
            Assert.False(left.Equals(right));
        }

        [Fact]
        public void Equals_WhenGivenNull_ReturnsFalse()
        {
            Assert.False(DependencyTestData.BaseModRequiredGreaterThanEqualZeroThirteenZero.Equals(null));
        }
        #endregion
    }
}
