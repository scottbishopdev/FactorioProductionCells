using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.ValueObjects;
using FactorioProductionCells.Domain.Enums;
using FactorioProductionCells.TestData.Domain.ValueObjects;

namespace FactorioProductionCells.TestData.Domain.Entities
{
    public static class DependencyTestData
    {
        #region Static Data
        // Base Mod Dependencies
        public static DependencyTestDataPoint BaseModRequiredGreaterThanEqualZeroThirteenZeroTestDataPoint = CreateTestDataPointFromProperties(
            DependencyType: new DependencyType(DependencyTypeId.Required),
            DependentModName: "base",
            DependencyComparisonType: new DependencyComparisonType(DependencyComparisonTypeId.GreaterThanOrEqualTo),
            DependentModVersion: ModVersion.For("0.13.0"));
        public static Dependency BaseModRequiredGreaterThanEqualZeroThirteenZero = BaseModRequiredGreaterThanEqualZeroThirteenZeroTestDataPoint.ObjectFromConstructor;

        public static DependencyTestDataPoint BaseModRequiredGreaterThanEqualZeroFifteenZeroTestDataPoint = CreateTestDataPointFromProperties(
            DependencyType: new DependencyType(DependencyTypeId.Required),
            DependentModName: "base",
            DependencyComparisonType: new DependencyComparisonType(DependencyComparisonTypeId.GreaterThanOrEqualTo),
            DependentModVersion: ModVersion.For("0.15.0"));
        public static Dependency BaseModRequiredGreaterThanEqualZeroFifteenZero = BaseModRequiredGreaterThanEqualZeroFifteenZeroTestDataPoint.ObjectFromConstructor;

        public static DependencyTestDataPoint BaseModRequiredGreaterThanEqualZeroSixteenZeroTestDataPoint = CreateTestDataPointFromProperties(
            DependencyType: new DependencyType(DependencyTypeId.Required),
            DependentModName: "base",
            DependencyComparisonType: new DependencyComparisonType(DependencyComparisonTypeId.GreaterThanOrEqualTo),
            DependentModVersion: ModVersion.For("0.16.0"));
        public static Dependency BaseModRequiredGreaterThanEqualZeroSixteenZero = BaseModRequiredGreaterThanEqualZeroSixteenZeroTestDataPoint.ObjectFromConstructor;

        public static DependencyTestDataPoint BaseModRequiredGreaterThanEqualZeroEighteenZeroTestDataPoint = CreateTestDataPointFromProperties(
            DependencyType: new DependencyType(DependencyTypeId.Required),
            DependentModName: "base",
            DependencyComparisonType: new DependencyComparisonType(DependencyComparisonTypeId.GreaterThanOrEqualTo),
            DependentModVersion: ModVersion.For("0.18.0"));
        public static Dependency BaseModRequiredGreaterThanEqualZeroEighteenZero = BaseModRequiredGreaterThanEqualZeroEighteenZeroTestDataPoint.ObjectFromConstructor;

        public static DependencyTestDataPoint BaseModRequiredGreaterThanEqualZeroEighteenThirteenTestDataPoint = CreateTestDataPointFromProperties(
            DependencyType: new DependencyType(DependencyTypeId.Required),
            DependentModName: "base",
            DependencyComparisonType: new DependencyComparisonType(DependencyComparisonTypeId.GreaterThanOrEqualTo),
            DependentModVersion: ModVersion.For("0.18.13"));
        public static Dependency BaseModRequiredGreaterThanEqualZeroEighteenThirteen = BaseModRequiredGreaterThanEqualZeroEighteenThirteenTestDataPoint.ObjectFromConstructor;
        
        // Test Mod
        public static DependencyTestDataPoint AngelsConfigOptionalGreaterThanEqualZeroOneTwoTestDataPoint = CreateTestDataPointFromProperties(
            DependencyType: new DependencyType(DependencyTypeId.Optional),
            DependentModName: "angelsConfig",
            DependencyComparisonType: new DependencyComparisonType(DependencyComparisonTypeId.GreaterThanOrEqualTo),
            DependentModVersion: ModVersion.For("0.1.2"));
        public static Dependency AngelsConfigOptionalGreaterThanEqualZeroOneTwo = AngelsConfigOptionalGreaterThanEqualZeroOneTwoTestDataPoint.ObjectFromConstructor;

        public static DependencyTestDataPoint AngelsOresRequiredNotEqualToZeroFourTwoTestDataPoint = CreateTestDataPointFromProperties(
            DependencyType: new DependencyType(DependencyTypeId.Required),
            DependentModName: "angelsOres",
            DependencyComparisonType: new DependencyComparisonType(DependencyComparisonTypeId.NotEqualTo),
            DependentModVersion: ModVersion.For("0.4.2"));
        public static Dependency AngelsOresRequiredNotEqualToZeroFourTwo = AngelsOresRequiredNotEqualToZeroFourTwoTestDataPoint.ObjectFromConstructor;

        public static DependencyTestDataPoint AngelsOreHiddenOptionalGreaterThanOneZeroOneTestDataPoint = CreateTestDataPointFromProperties(
            DependencyType: new DependencyType(DependencyTypeId.HiddenOptional),
            DependentModName: "angelsOres",
            DependencyComparisonType: new DependencyComparisonType(DependencyComparisonTypeId.GreaterThan),
            DependentModVersion: ModVersion.For("1.0.1"));
        public static Dependency AngelsOreHiddenOptionalGreaterThanOneZeroOne = AngelsOreHiddenOptionalGreaterThanOneZeroOneTestDataPoint.ObjectFromConstructor;

        public static DependencyTestDataPoint ColorblindCircuitNetworkIncompatibilityTestDataPoint = CreateTestDataPointFromProperties(
            DependencyType: new DependencyType(DependencyTypeId.Incompatibility),
            DependentModName: "ColorblindCircuitNetwork",
            DependencyComparisonType: null,
            DependentModVersion: null);
        public static Dependency ColorblindCircuitNetworkIncompatibility = ColorblindCircuitNetworkIncompatibilityTestDataPoint.ObjectFromConstructor;

        public static List<Dependency> TestModAlphaReleaseDependencies = new List<Dependency>
        {
            BaseModRequiredGreaterThanEqualZeroThirteenZero,
            AngelsConfigOptionalGreaterThanEqualZeroOneTwo,
            AngelsOresRequiredNotEqualToZeroFourTwo
        };
        public static List<Dependency> TestModBetaReleaseDependencies = new List<Dependency>
        {
            BaseModRequiredGreaterThanEqualZeroThirteenZero,
            AngelsConfigOptionalGreaterThanEqualZeroOneTwo,
            AngelsOresRequiredNotEqualToZeroFourTwo,
            AngelsOreHiddenOptionalGreaterThanOneZeroOne,
            ColorblindCircuitNetworkIncompatibility
        };
        public static List<Dependency> TestModGammaReleaseDependencies = new List<Dependency>
        {
            BaseModRequiredGreaterThanEqualZeroThirteenZero,
            AngelsConfigOptionalGreaterThanEqualZeroOneTwo,
            AngelsOresRequiredNotEqualToZeroFourTwo,
            AngelsOreHiddenOptionalGreaterThanOneZeroOne,
            ColorblindCircuitNetworkIncompatibility
        };

        // Bob's Functions Library Mod
        public static List<Dependency> BobsFunctionsLibraryFirstReleaseDependencies = new List<Dependency>
        {
            BaseModRequiredGreaterThanEqualZeroThirteenZero
        };

        public static List<Dependency> BobsFunctionsLibraryMiddleReleaseDependencies = new List<Dependency>
        {
            BaseModRequiredGreaterThanEqualZeroSixteenZero
        };

        public static List<Dependency> BobsFunctionsLibraryLatestReleaseDependencies = new List<Dependency>
        {
            BaseModRequiredGreaterThanEqualZeroEighteenZero
        };

        // Bob's Logistics Mod
        private static DependencyTestDataPoint BobLibraryRequiredGreaterThanEqualZeroThirteenZeroTestDataPoint = CreateTestDataPointFromProperties(
            DependencyType: new DependencyType(DependencyTypeId.Required),
            DependentModName: "boblibrary",
            DependencyComparisonType: new DependencyComparisonType(DependencyComparisonTypeId.GreaterThanOrEqualTo),
            DependentModVersion: ModVersion.For("0.13.0"));
        public static Dependency BobLibraryRequiredGreaterThanEqualZeroThirteenZero = BobLibraryRequiredGreaterThanEqualZeroThirteenZeroTestDataPoint.ObjectFromConstructor;
        
        private static DependencyTestDataPoint BobConfigOptionalGreaterThanEqualZeroThirteenZeroTestDataPoint = CreateTestDataPointFromProperties(
            DependencyType: new DependencyType(DependencyTypeId.Optional),
            DependentModName: "bobconfig",
            DependencyComparisonType: new DependencyComparisonType(DependencyComparisonTypeId.GreaterThanOrEqualTo),
            DependentModVersion: ModVersion.For("0.13.0"));
        public static Dependency BobConfigOptionalGreaterThanEqualZeroThirteenZero = BobConfigOptionalGreaterThanEqualZeroThirteenZeroTestDataPoint.ObjectFromConstructor;

        private static DependencyTestDataPoint BobPlatesOptionalGreaterThanEqualZeroThirteenZeroTestDataPoint = CreateTestDataPointFromProperties(
            DependencyType: new DependencyType(DependencyTypeId.Optional),
            DependentModName: "bobplates",
            DependencyComparisonType: new DependencyComparisonType(DependencyComparisonTypeId.GreaterThanOrEqualTo),
            DependentModVersion: ModVersion.For("0.13.0"));
        public static Dependency BobPlatesOptionalGreaterThanEqualZeroThirteenZero = BobPlatesOptionalGreaterThanEqualZeroThirteenZeroTestDataPoint.ObjectFromConstructor;

        public static List<Dependency> BobsLogisticsFirstReleaseDependencies = new List<Dependency>
        {
            BaseModRequiredGreaterThanEqualZeroThirteenZero,
            BobLibraryRequiredGreaterThanEqualZeroThirteenZero,
            BobConfigOptionalGreaterThanEqualZeroThirteenZero,
            BobPlatesOptionalGreaterThanEqualZeroThirteenZero
        };

        private static DependencyTestDataPoint BobLibraryRequiredGreaterThanEqualZeroFifteenTwoTestDataPoint = CreateTestDataPointFromProperties(
            DependencyType: new DependencyType(DependencyTypeId.Required),
            DependentModName: "boblibrary",
            DependencyComparisonType: new DependencyComparisonType(DependencyComparisonTypeId.GreaterThanOrEqualTo),
            DependentModVersion: ModVersion.For("0.15.2"));
        public static Dependency BobLibraryRequiredGreaterThanEqualZeroFifteenTwo = BobLibraryRequiredGreaterThanEqualZeroFifteenTwoTestDataPoint.ObjectFromConstructor;
        
        private static DependencyTestDataPoint BobPlatesOptionalGreaterThanEqualZeroFifteenZeroTestDataPoint = CreateTestDataPointFromProperties(
            DependencyType: new DependencyType(DependencyTypeId.Optional),
            DependentModName: "bobplates",
            DependencyComparisonType: new DependencyComparisonType(DependencyComparisonTypeId.GreaterThanOrEqualTo),
            DependentModVersion: ModVersion.For("0.15.0"));
        public static Dependency BobPlatesOptionalGreaterThanEqualZeroFifteenZero = BobPlatesOptionalGreaterThanEqualZeroFifteenZeroTestDataPoint.ObjectFromConstructor;
        
        private static DependencyTestDataPoint BobInsertersOptionalGreaterThanEqualZeroFifteenZeroTestDataPoint = CreateTestDataPointFromProperties(
            DependencyType: new DependencyType(DependencyTypeId.Optional),
            DependentModName: "bobinserters",
            DependencyComparisonType: new DependencyComparisonType(DependencyComparisonTypeId.GreaterThanOrEqualTo),
            DependentModVersion: ModVersion.For("0.15.0"));
        public static Dependency BobInsertersOptionalGreaterThanEqualZeroFifteenZero = BobInsertersOptionalGreaterThanEqualZeroFifteenZeroTestDataPoint.ObjectFromConstructor;

        public static List<Dependency> BobsLogisticsMiddleReleaseDependencies = new List<Dependency>
        {
            BaseModRequiredGreaterThanEqualZeroFifteenZero,
            BobLibraryRequiredGreaterThanEqualZeroFifteenTwo,
            BobPlatesOptionalGreaterThanEqualZeroFifteenZero,
            BobInsertersOptionalGreaterThanEqualZeroFifteenZero
        };

        private static DependencyTestDataPoint BobLibraryRequiredGreaterThanEqualZeroEighteenZeroTestDataPoint = CreateTestDataPointFromProperties(
            DependencyType: new DependencyType(DependencyTypeId.Required),
            DependentModName: "boblibrary",
            DependencyComparisonType: new DependencyComparisonType(DependencyComparisonTypeId.GreaterThanOrEqualTo),
            DependentModVersion: ModVersion.For("0.18.0"));
        public static Dependency BobLibraryRequiredGreaterThanEqualZeroEighteenZero = BobLibraryRequiredGreaterThanEqualZeroEighteenZeroTestDataPoint.ObjectFromConstructor;

        private static DependencyTestDataPoint BobPlatesOptionalGreaterThanEqualZeroEighteenZeroTestDataPoint = CreateTestDataPointFromProperties(
            DependencyType: new DependencyType(DependencyTypeId.Optional),
            DependentModName: "bobplates",
            DependencyComparisonType: new DependencyComparisonType(DependencyComparisonTypeId.GreaterThanOrEqualTo),
            DependentModVersion: ModVersion.For("0.18.0"));
        public static Dependency BobPlatesOptionalGreaterThanEqualZeroEighteenZero = BobPlatesOptionalGreaterThanEqualZeroEighteenZeroTestDataPoint.ObjectFromConstructor;

        private static DependencyTestDataPoint BobInsertersOptionalGreaterThanEqualZeroEighteenZeroTestDataPoint = CreateTestDataPointFromProperties(
            DependencyType: new DependencyType(DependencyTypeId.Optional),
            DependentModName: "bobinserters",
            DependencyComparisonType: new DependencyComparisonType(DependencyComparisonTypeId.GreaterThanOrEqualTo),
            DependentModVersion: ModVersion.For("0.18.0"));
        public static Dependency BobInsertersOptionalGreaterThanEqualZeroEighteenZero = BobInsertersOptionalGreaterThanEqualZeroEighteenZeroTestDataPoint.ObjectFromConstructor;

        public static List<Dependency> BobsLogisticsLatestReleaseDependencies = new List<Dependency>
        {
            BaseModRequiredGreaterThanEqualZeroEighteenThirteen,
            BobLibraryRequiredGreaterThanEqualZeroEighteenZero,
            BobPlatesOptionalGreaterThanEqualZeroEighteenZero,
            BobInsertersOptionalGreaterThanEqualZeroEighteenZero
        };
        #endregion

        #region Static Test Data Collections
        private static List<DependencyTestDataPoint> StaticTestData = new List<DependencyTestDataPoint>()
        {
            BaseModRequiredGreaterThanEqualZeroThirteenZeroTestDataPoint,
            BaseModRequiredGreaterThanEqualZeroFifteenZeroTestDataPoint,
            BaseModRequiredGreaterThanEqualZeroSixteenZeroTestDataPoint,
            BaseModRequiredGreaterThanEqualZeroEighteenZeroTestDataPoint,
            BaseModRequiredGreaterThanEqualZeroEighteenThirteenTestDataPoint,
            AngelsConfigOptionalGreaterThanEqualZeroOneTwoTestDataPoint,
            AngelsOresRequiredNotEqualToZeroFourTwoTestDataPoint,
            AngelsOreHiddenOptionalGreaterThanOneZeroOneTestDataPoint,
            BobLibraryRequiredGreaterThanEqualZeroThirteenZeroTestDataPoint,
            BobConfigOptionalGreaterThanEqualZeroThirteenZeroTestDataPoint,
            BobPlatesOptionalGreaterThanEqualZeroThirteenZeroTestDataPoint,
            BobLibraryRequiredGreaterThanEqualZeroFifteenTwoTestDataPoint,
            BobPlatesOptionalGreaterThanEqualZeroFifteenZeroTestDataPoint,
            BobInsertersOptionalGreaterThanEqualZeroFifteenZeroTestDataPoint,
            BobLibraryRequiredGreaterThanEqualZeroEighteenZeroTestDataPoint,
            BobPlatesOptionalGreaterThanEqualZeroEighteenZeroTestDataPoint,
            BobInsertersOptionalGreaterThanEqualZeroEighteenZeroTestDataPoint,
            ColorblindCircuitNetworkIncompatibilityTestDataPoint
        };

        public static IEnumerable<object[]> ValidStaticStringsWithDependencyType() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.CreationString, dataPoint.DependencyType };

        public static IEnumerable<object[]> ValidStaticStringsWithDependentModName() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.CreationString, dataPoint.DependentModName };

        public static IEnumerable<object[]> ValidStaticStringsWithDependencyComparisonType() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.CreationString, dataPoint.DependencyComparisonType };

        public static IEnumerable<object[]> ValidStaticStringsWithDependentModVersion() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.CreationString, dataPoint.DependentModVersion };

        public static IEnumerable<object[]> ValidDependencyStringWithFactorioVersionFormattedModVersion() =>
            new List<object[]>
            {
                new object[] { "base >= 0.14", ModVersion.For("0.14.0") }
            };

        public static IEnumerable<object[]> ValidStaticEqualDependencyPairs() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, new Dependency(dataPoint.ObjectFromFor) };

        public static IEnumerable<object[]> ValidStaticNonEqualDependencyPairs()
        {
            for (int i = 0; i < StaticTestData.Count; i++)
            {
                var secondDataPoint = i != StaticTestData.Count - 1 ? StaticTestData[i+1] : StaticTestData[0];
                yield return new object[] { StaticTestData[i].ObjectFromFor, secondDataPoint.ObjectFromFor };
            }
        }

        public static IEnumerable<object[]> ValidStaticDependenciesFromForWithStrings() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.CreationString }; // TODO: should I be giving the expected string here???

        public static IEnumerable<object[]> ValidStaticDependenciesWithDependencyType() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.DependencyType};

        public static IEnumerable<object[]> ValidStaticDependenciesWithDependentModName() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.DependentModName};

        public static IEnumerable<object[]> ValidStaticDependenciesWithDependencyComparisonType() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.DependencyComparisonType};

        public static IEnumerable<object[]> ValidStaticDependenciesWithDependentModVersion() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.DependentModVersion};

        public static IEnumerable<object[]> ValidStaticDependenciesCreationProperties() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.DependencyType, dataPoint.DependentModName, dataPoint.DependencyComparisonType, dataPoint.DependentModVersion };
        #endregion

        #region Random Test Data Collections
        private static List<DependencyTestDataPoint> RandomTestData = GetRandomizedTestData(10);

        public static IEnumerable<object[]> ValidRandomStringsWithDependencyType() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.CreationString, dataPoint.DependencyType };

        public static IEnumerable<object[]> ValidRandomStringsWithDependentModName() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.CreationString, dataPoint.DependentModName };

        public static IEnumerable<object[]> ValidRandomStringsWithDependencyComparisonType() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.CreationString, dataPoint.DependencyComparisonType };

        public static IEnumerable<object[]> ValidRandomStringsWithDependentModVersion() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.CreationString, dataPoint.DependentModVersion };

        public static IEnumerable<object[]> ValidRandomEqualDependencyPairs() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, new Dependency(dataPoint.ObjectFromFor) };

        public static IEnumerable<object[]> ValidRandomDependenciesFromForWithStrings() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.CreationString };

        public static IEnumerable<object[]> ValidRandomDependenciesWithDependencyType() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.DependencyType};

        public static IEnumerable<object[]> ValidRandomDependenciesWithDependentModName() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.DependentModName};

        public static IEnumerable<object[]> ValidRandomDependenciesWithDependencyComparisonType() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.DependencyComparisonType};

        public static IEnumerable<object[]> ValidRandomDependenciesWithDependentModVersion() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.DependentModVersion};

        public static IEnumerable<object[]> ValidRandomDependenciesCreationProperties() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.DependencyType, dataPoint.DependentModName, dataPoint.DependencyComparisonType, dataPoint.DependentModVersion };

        public static IEnumerable<object[]> RandomDependenciesWithModNameTooLong(Int32 count = 6)
        {
            var random = new Random();
            var resultStringBuilder = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                resultStringBuilder.Clear();
                if (random.Next(6) == 0)
                {
                    resultStringBuilder.Append(DependencyTypeTestData.GenerateValidRandomizedDependencyTypeString());
                    if (resultStringBuilder.Length != 0) resultStringBuilder.Append(" ");
                }
                
                resultStringBuilder.Append(ModTestData.GenerateRandomizedModNameTooLong());

                if (random.Next(6) == 0)
                {
                    if (random.Next(6) == 0) resultStringBuilder.Append(" ");
                    resultStringBuilder.Append(DependencyComparisonTypeTestData.GenerateValidRandomizedDependencyComparisonTypeString());
                    if (random.Next(6) == 0) resultStringBuilder.Append(" ");
                    resultStringBuilder.Append(ModVersionTestData.GenerateValidRandomizedModVersionString());
                }

                yield return new object[] { resultStringBuilder.ToString() };
            }
        }

        public static IEnumerable<object[]> RandomDependencyStringsWithExpectedModVersion(Int32 count = 10)
        {
            // Quick Note: This class demonstrates the crazy range of cases we need to cover when parsing dependency strings.
            var random = new Random();
            var resultStringBuilder = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                resultStringBuilder.Clear();
                ModVersion expectedModVersion = null;
                
                if (random.Next(6) == 0)
                {
                    resultStringBuilder.Append(DependencyTypeTestData.GenerateValidRandomizedDependencyTypeString());
                    if (resultStringBuilder.Length != 0) resultStringBuilder.Append(" ");
                }
                
                String modNameString = random.Next(10) == 0 ? "base" : ModTestData.GenerateValidRandomizedModName();
                resultStringBuilder.Append(modNameString);

                if (random.Next(6) == 0)
                {
                    if (random.Next(6) == 0) resultStringBuilder.Append(" ");
                    resultStringBuilder.Append(DependencyComparisonTypeTestData.GenerateValidRandomizedDependencyComparisonTypeString());
                    if (random.Next(6) == 0) resultStringBuilder.Append(" ");
                    
                    if (modNameString == "base" && random.Next(2) != 0)
                    {
                        String randomFactorioVersionString = FactorioVersionTestData.GenerateValidRandomizedFactorioVersionString();
                        expectedModVersion = ModVersion.For(randomFactorioVersionString + ".0");
                        resultStringBuilder.Append(randomFactorioVersionString);
                    }
                    else
                    {
                        String randomModVersionString = ModVersionTestData.GenerateValidRandomizedModVersionString();
                        expectedModVersion = ModVersion.For(randomModVersionString);
                        resultStringBuilder.Append(randomModVersionString);
                    }
                }

                yield return new object[] { resultStringBuilder.ToString(), expectedModVersion };
            }
        }
        #endregion

        #region Helper Methods and Struct
        public struct DependencyTestDataPoint
        {
            public String CreationString;
            public String ExpectedString;
            public DependencyType DependencyType;
            public String DependentModName;
            public DependencyComparisonType DependencyComparisonType;
            public ModVersion DependentModVersion;
            public Dependency ObjectFromConstructor;
            public Dependency ObjectFromFor;
        }

        private static DependencyTestDataPoint CreateTestDataPointFromProperties(DependencyType DependencyType, String DependentModName, DependencyComparisonType DependencyComparisonType, ModVersion DependentModVersion)
        {
            var creationStringBuilder = new StringBuilder();
            if (DependencyType.Id != DependencyTypeId.Required) creationStringBuilder.Append($"{DependencyType} ");
            creationStringBuilder.Append(DependentModName);
            if (DependencyComparisonType != null) creationStringBuilder.Append($" {DependencyComparisonType} {DependentModVersion}");

            return new DependencyTestDataPoint
            {
                CreationString = creationStringBuilder.ToString(), // TODO: will this ever be given a string where the ModVersion is in FactorioVersion format?
                ExpectedString = creationStringBuilder.ToString(),
                DependencyType = DependencyType,
                DependentModName = DependentModName,
                DependencyComparisonType = DependencyComparisonType,
                DependentModVersion = DependentModVersion,
                ObjectFromConstructor = new Dependency(DependencyType, DependentModName, DependencyComparisonType, DependentModVersion),
                ObjectFromFor = Dependency.For(creationStringBuilder.ToString())
            };
        }

        private static DependencyTestDataPoint GenerateValidRandomizedTestDataPoint()
        {
            var random = new Random();

            DependencyType dependencyType = DependencyTypeTestData.GenerateValidRandomizedDependencyType();
            String dependentModName = random.Next(10) == 0 ? "base" : ModTestData.GenerateValidRandomizedModName();
            DependencyComparisonType dependencyComparisonType = null;
            ModVersion dependentModVersion = null;

            if (random.Next(6) == 0)
            {
                dependencyComparisonType = DependencyComparisonTypeTestData.GenerateValidRandomizedDependencyComparisonType();
                
                if(dependentModName == "base" && random.Next(2) == 0)
                {
                    dependentModVersion = ModVersion.For(FactorioVersionTestData.GenerateValidRandomizedFactorioVersionString() + ".0");
                }
                else
                {
                    dependentModVersion = ModVersionTestData.GenerateValidRandomizedModVersion();
                }
            }

            return CreateTestDataPointFromProperties(
                DependencyType: dependencyType,
                DependentModName: dependentModName,
                DependencyComparisonType: dependencyComparisonType,
                DependentModVersion: dependentModVersion
            );
        }

        public static Dependency GenerateValidRandomizedDependency()
        {
            return GenerateValidRandomizedTestDataPoint().ObjectFromConstructor;
        }

        private static List<DependencyTestDataPoint> GetRandomizedTestData(Int32 count = 10)
        {
            List<DependencyTestDataPoint> randomTestData = new List<DependencyTestDataPoint>();
            for (int i = 0; i < count; i++)
            {
                randomTestData.Add(GenerateValidRandomizedTestDataPoint());
            }

            return randomTestData;
        }
        #endregion
    }
}
