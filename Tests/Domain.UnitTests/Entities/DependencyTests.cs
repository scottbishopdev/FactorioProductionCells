using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.Enums;
using FactorioProductionCells.Domain.ValueObjects;

namespace FactorioProductionCells.Domain.UnitTests.Entities
{
    public class DependencyTests
    {
        internal static String TestModBaseDependencyString = "base >= 0.14.0";
        internal static String TestModAngelsConfigDependencyString = "? angelsConfig >= 0.1.2";
        internal static Dependency TestModAngelsConfigFromConstructor = new Dependency(
            DependencyType: new DependencyType(DependencyTypeId.Optional),
            DependentModName: "angelsConfig",
            DependencyComparisonType: new DependencyComparisonType(DependencyComparisonTypeId.GreaterThanOrEqualTo),
            DependentModVersion: ModVersion.For("0.1.2"));

        internal static Dependency TestModBaseDependencyFromFor = Dependency.For(TestModBaseDependencyString);
        internal static Dependency TestModAngelsConfigDependencyFromFor = Dependency.For(TestModAngelsConfigDependencyString);
        private static Random Random = new Random();

        #region DependencyConstructor
        [Fact]
        public void DependencyConstructor_WhenValidParameters_ReturnsCorrectDependencyType()
        {
            // TODO: See if there's a way to perform this assertion without relying on the class's .ToString() method.
            Assert.Equal(new DependencyType(DependencyTypeId.Optional).ToString(), TestModAngelsConfigFromConstructor.DependencyType.ToString());
        }

        [Fact]
        public void DependencyConstructor_WhenValidParameters_ReturnsCorrectDependentModName()
        {
            Assert.Equal("angelsConfig", TestModAngelsConfigFromConstructor.DependentModName);
        }

        [Fact]
        public void DependencyConstructor_WhenValidParameters_ReturnsCorrectDependencyComparisonType()
        {
            // TODO: See if there's a way to perform this assertion without relying on the class's .ToString() method.
            Assert.Equal(new DependencyComparisonType(DependencyComparisonTypeId.GreaterThanOrEqualTo).ToString(), TestModAngelsConfigFromConstructor.DependencyComparisonType.ToString());
        }

        [Fact]
        public void DependencyConstructor_WhenValidParameters_ReturnsCorrectDependentModVersion()
        {
            Assert.Equal(ModVersion.For("0.1.2"), TestModAngelsConfigFromConstructor.DependentModVersion);
        }
        #endregion

        #region For
        [Theory]
        [InlineData("? angelsConfig >= 0.1.2", "?")]
        [InlineData(" ? angelsConfig >= 0.1.2   ", "?")]
        [InlineData("(?) angelsConfig >= 0.1.2", "(?)")]
        [InlineData("! angelsConfig >= 0.1.2", "!")]
        [InlineData("angelsConfig >= 0.1.2", "")]
        [InlineData("   angelsConfig >= 0.1.2 ", "")]
        public void For_WhenGivenValidString_ReturnsCorrectDependencyType(String dependencyString, String dependencyTypeString)
        {
            var dependency = Dependency.For(dependencyString);
            // TODO: See if there's a way to perform this assertion without relying on the class's .ToString() method.
            Assert.Equal(dependencyTypeString, dependency.DependencyType.ToString());
        }

        [Theory]
        [InlineData("base >= 0.14.0", "base")]
        [InlineData(" base >= 0.14.0    ", "base")]
        [InlineData("? angelsConfig >= 0.1.2", "angelsConfig")]
        public void For_WhenGivenValidString_ReturnsCorrectDependentModName(String dependencyString, String dependentModName)
        {
            var dependency = Dependency.For(dependencyString);
            Assert.Equal(dependentModName, dependency.DependentModName);
        }

        [Theory]
        [InlineData("   angelsConfig >= 0.1.2 ", ">=")]
        [InlineData("angelsConfig >= 0.1.2", ">=")]
        [InlineData("angelsConfig > 0.1.2", ">")]
        [InlineData("! angelsConfig > 0.1.2", ">")]
        [InlineData("angelsConfig = 0.1.2", "=")]
        [InlineData("angelsConfig < 0.1.2", "<")]
        [InlineData("angelsConfig <= 0.1.2", "<=")]
        public void For_WhenGivenValidString_ReturnsCorrectDependencyComparisonType(String dependencyString, String dependencyComparisonTypeString)
        {
            var dependency = Dependency.For(dependencyString);
            // TODO: See if there's a way to perform this assertion without relying on the class's .ToString() method.
            Assert.Equal(dependencyComparisonTypeString, dependency.DependencyComparisonType.ToString());
        }

        [Theory]
        [InlineData("base >= 0.14.0", "0.14.0")]
        [InlineData("angelsConfig >= 1.1.2", "1.1.2")]
        public void For_WhenGivenValidString_ReturnsCorrectDependentModVersion(String dependencyString, String dependentModVersionString)
        {
            var dependency = Dependency.For(dependencyString);
            Assert.Equal(ModVersion.For(dependentModVersionString), dependency.DependentModVersion);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("?? base >= 0.14.0")]
        [InlineData("? ? base >= 0.14.0")]
        [InlineData(">= base ! 0.14.0")]
        [InlineData("base >= 0.14")]
        [InlineData(" >= 0.14.0")]
        [InlineData(">= 0.14.0")]
        [InlineData("base  0.14.0")]
        [InlineData("base 0.14.0")]
        [InlineData("base0.14.0")]
        [InlineData("base >= ")]
        [InlineData("base >=")]
        [InlineData("base >= 0.140")]
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
        [MemberData(nameof(ReleaseFileNamesWithModNameTooLong))]
        public void For_WhenGivenModNameTooLong_ThrowsArgumentException(String dependencyString)
        {
            var exception = Assert.Throws<ArgumentException>(() => Dependency.For(dependencyString));
            Assert.Equal($"The mod name specified exceeds the maximum length of {Mod.NameLength}. (Parameter 'dependencyString')", exception.Message);
        }
        #endregion

        #region ToString
        [Fact]
        public void ToString_WhenGivenDependencyWithoutType_ReturnsCorrectString()
        {
            Assert.Equal(TestModBaseDependencyString, TestModBaseDependencyFromFor.ToString());
        }

        [Fact]
        public void ToString_WhenGivenDependencyWithType_ReturnsCorrectString()
        {
            Assert.Equal(TestModAngelsConfigDependencyString, TestModAngelsConfigDependencyFromFor.ToString());
        }
        #endregion

        public static IEnumerable<object[]> ReleaseFileNamesWithModNameTooLong =>
            new List<object[]>
            {
                new object[] {
                    new String("? " + GetRandomCharacterString(ReleaseFileName.ValidModNameCharacters, Mod.NameLength + 1) + " >= 0.1.2")
                },
                new object[] {
                    new String("? " + GetRandomCharacterString(ReleaseFileName.ValidModNameCharacters, Mod.NameLength + 100) + " >= 0.1.2")
                }
            };

        private static String GetRandomCharacterString(String characterSet, Int32 length)
        {
            return new String(Enumerable.Repeat(characterSet, length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
