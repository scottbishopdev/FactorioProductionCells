using System;
using System.Collections.Generic;
using System.Linq;
using FactorioProductionCells.Domain.ValueObjects;
using FactorioProductionCells.TestData.Common;

namespace FactorioProductionCells.TestData.Domain.ValueObjects
{
    public static class ModVersionTestData
    {
        #region Static Data
        public static ModVersionTestDataPoint ZeroPointFourPointThreeTestDataPoint = CreateTestDataPointFromProperties(0, 4, 3);
        public static ModVersion ZeroPointFourPointThree = ZeroPointFourPointThreeTestDataPoint.ObjectFromFor;

        // Test Mod
        public static ModVersionTestDataPoint TestModAlphaReleaseVersionTestDataPoint = CreateTestDataPointFromProperties(1, 4, 16);
        public static ModVersion TestModAlphaReleaseVersion = TestModAlphaReleaseVersionTestDataPoint.ObjectFromFor;

        public static ModVersionTestDataPoint TestModBetaReleaseVersionTestDataPoint = CreateTestDataPointFromProperties(1, 5, 1);
        public static ModVersion TestModBetaReleaseVersion = TestModBetaReleaseVersionTestDataPoint.ObjectFromFor;

        public static ModVersionTestDataPoint TestModGammaReleaseVersionTestDataPoint = CreateTestDataPointFromProperties(1, 6, 0);
        public static ModVersion TestModGammaReleaseVersion = TestModGammaReleaseVersionTestDataPoint.ObjectFromFor;

        // Bob's Functions Library Mod
        public static ModVersionTestDataPoint BobsFunctionsLibraryFirstReleaseVersionTestDataPoint = CreateTestDataPointFromProperties(0, 13, 0);
        public static ModVersion BobsFunctionsLibraryFirstReleaseVersion = BobsFunctionsLibraryFirstReleaseVersionTestDataPoint.ObjectFromFor;

        public static ModVersionTestDataPoint BobsFunctionsLibraryMiddleReleaseVersionTestDataPoint = CreateTestDataPointFromProperties(0, 16, 1);
        public static ModVersion BobsFunctionsLibraryMiddleReleaseVersion = BobsFunctionsLibraryMiddleReleaseVersionTestDataPoint.ObjectFromFor;
        
        public static ModVersionTestDataPoint BobsFunctionsLibraryLatestReleaseVersionTestDataPoint = CreateTestDataPointFromProperties(0, 18, 9);
        public static ModVersion BobsFunctionsLibraryLatestReleaseVersion = BobsFunctionsLibraryLatestReleaseVersionTestDataPoint.ObjectFromFor;
        
        // Bob's Logistics Mod
        public static ModVersionTestDataPoint BobsLogisticsFirstReleaseVersionTestDataPoint = CreateTestDataPointFromProperties(0, 13, 1);
        public static ModVersion BobsLogisticsFirstReleaseVersion = BobsLogisticsFirstReleaseVersionTestDataPoint.ObjectFromFor;

        public static ModVersionTestDataPoint BobsLogisticsMiddleReleaseVersionTestDataPoint = CreateTestDataPointFromProperties(0, 15, 5);
        public static ModVersion BobsLogisticsMiddleReleaseVersion = BobsLogisticsMiddleReleaseVersionTestDataPoint.ObjectFromFor;
        
        public static ModVersionTestDataPoint BobsLogisticsLatestReleaseVersionTestDataPoint = CreateTestDataPointFromProperties(0, 18, 8);
        public static ModVersion BobsLogisticsLatestReleaseVersion = BobsLogisticsLatestReleaseVersionTestDataPoint.ObjectFromFor;
        #endregion

        #region Static Test Data Collections
        private static List<ModVersionTestDataPoint> StaticTestData = new List<ModVersionTestDataPoint>()
        {
            ZeroPointFourPointThreeTestDataPoint,
            TestModAlphaReleaseVersionTestDataPoint,
            TestModBetaReleaseVersionTestDataPoint,
            BobsFunctionsLibraryFirstReleaseVersionTestDataPoint,
            BobsFunctionsLibraryMiddleReleaseVersionTestDataPoint,
            BobsFunctionsLibraryLatestReleaseVersionTestDataPoint,
            BobsLogisticsFirstReleaseVersionTestDataPoint,
            BobsLogisticsMiddleReleaseVersionTestDataPoint,
            BobsLogisticsLatestReleaseVersionTestDataPoint
        };

        public static IEnumerable<object[]> ValidStaticModVersionsWithMajor() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.MajorVersion };

        public static IEnumerable<object[]> ValidStaticModVersionsWithMinor() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.MinorVersion };

        public static IEnumerable<object[]> ValidStaticModVersionsWithPatch() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.PatchVersion };

        public static IEnumerable<object[]> ValidStaticStringsWithMajorInt() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.CreationString, dataPoint.MajorVersion };

        public static IEnumerable<object[]> ValidStaticStringsWithMinorInt() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.CreationString, dataPoint.MinorVersion };

        public static IEnumerable<object[]> ValidStaticStringsWithPatchInt() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.CreationString, dataPoint.PatchVersion };

        public static IEnumerable<object[]> ValidStaticEqualModVersionPairs() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, new ModVersion(dataPoint.ObjectFromFor) };

        public static IEnumerable<object[]> ValidStaticNonEqualModVersionPairs()
        {
            List<object[]> returnValues = new List<object[]>();

            for (int i = 0; i < StaticTestData.Count; i++)
            {
                var secondDataPoint = i != StaticTestData.Count - 1 ? StaticTestData[i+1] : StaticTestData[0];
                yield return new object[] { StaticTestData[i].ObjectFromFor, secondDataPoint.ObjectFromFor };
            }
        }

        public static IEnumerable<object[]> ValidStaticModVersionsFromForWithStrings() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.CreationString };

        public static IEnumerable<object[]> ValidStaticModVersionsFromForWithInts() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.MajorVersion, dataPoint.MinorVersion, dataPoint.PatchVersion };

        public static IEnumerable<object[]> Comparison_RightLowerData() =>
            from leftMajor in CommonTestData.PositiveIntegerValues
            from leftMinor in CommonTestData.PositiveIntegerValues
            from leftPatch in CommonTestData.PositiveIntegerValues
            from rightMajor in CommonTestData.PositiveIntegerValues
            from rightMinor in CommonTestData.PositiveIntegerValues
            from rightPatch in CommonTestData.PositiveIntegerValues
            where leftMajor > rightMajor || (leftMajor == rightMajor && leftMinor > rightMinor) || (leftMajor == rightMajor && leftMinor == rightMinor && leftPatch > rightPatch)
            select new object[] { $"{leftMajor.ToString()}.{leftMinor.ToString()}.{leftPatch.ToString()}", $"{rightMajor.ToString()}.{rightMinor.ToString()}.{rightPatch.ToString()}" };

        public static IEnumerable<object[]> Comparison_EqualData() =>
            from leftMajor in CommonTestData.PositiveIntegerValues
            from leftMinor in CommonTestData.PositiveIntegerValues
            from leftPatch in CommonTestData.PositiveIntegerValues
            from rightMajor in CommonTestData.PositiveIntegerValues
            from rightMinor in CommonTestData.PositiveIntegerValues
            from rightPatch in CommonTestData.PositiveIntegerValues
            where leftMajor == rightMajor && leftMinor == rightMinor && leftPatch == rightPatch
            select new object[] { $"{leftMajor.ToString()}.{leftMinor.ToString()}.{leftPatch.ToString()}", $"{rightMajor.ToString()}.{rightMinor.ToString()}.{rightPatch.ToString()}" };

        public static IEnumerable<object[]> Comparison_LeftLowerData() =>
            from leftMajor in CommonTestData.PositiveIntegerValues
            from leftMinor in CommonTestData.PositiveIntegerValues
            from leftPatch in CommonTestData.PositiveIntegerValues
            from rightMajor in CommonTestData.PositiveIntegerValues
            from rightMinor in CommonTestData.PositiveIntegerValues
            from rightPatch in CommonTestData.PositiveIntegerValues
            where leftMajor < rightMajor || (leftMajor == rightMajor && leftMinor < rightMinor) || (leftMajor == rightMajor && leftMinor == rightMinor && leftPatch < rightPatch)
            select new object[] { $"{leftMajor.ToString()}.{leftMinor.ToString()}.{leftPatch.ToString()}", $"{rightMajor.ToString()}.{rightMinor.ToString()}.{rightPatch.ToString()}" };
        #endregion

        #region Random Test Data Collections
        private static List<ModVersionTestDataPoint> RandomTestData = GetRandomizedTestData(10);

        public static IEnumerable<object[]> ValidRandomModVersionsWithMajor() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.MajorVersion };

        public static IEnumerable<object[]> ValidRandomModVersionsWithMinor() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.MinorVersion };

        public static IEnumerable<object[]> ValidRandomModVersionsWithPatch() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.PatchVersion };

        public static IEnumerable<object[]> ValidRandomStringsWithMajorInt() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.CreationString, dataPoint.MajorVersion };

        public static IEnumerable<object[]> ValidRandomStringsWithMinorInt() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.CreationString, dataPoint.MinorVersion };

        public static IEnumerable<object[]> ValidRandomStringsWithPatchInt() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.CreationString, dataPoint.PatchVersion };

        public static IEnumerable<object[]> ValidRandomEqualModVersionPairs() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, new ModVersion(dataPoint.ObjectFromFor) };

        public static IEnumerable<object[]> ValidRandomModVersionsFromForWithStrings() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.CreationString };

        public static IEnumerable<object[]> ValidRandomModVersionsFromForWithInts() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.MajorVersion, dataPoint.MinorVersion, dataPoint.PatchVersion };
        #endregion

        #region Helper Methods and Struct
        public struct ModVersionTestDataPoint
        {
            public String CreationString;
            public Int32 MajorVersion;
            public Int32 MinorVersion;
            public Int32 PatchVersion;
            public ModVersion ObjectFromFor;
        }

        public static String GenerateValidRandomizedModVersionString()
        {
            var random = new Random();
            return $"{random.Next(0, Int32.MaxValue)}.{random.Next(0, Int32.MaxValue)}.{random.Next(0, Int32.MaxValue)}";
        }

        public static ModVersion GenerateValidRandomizedModVersion()
        {
            return ModVersion.For(GenerateValidRandomizedModVersionString());
        }

        private static ModVersionTestDataPoint CreateTestDataPointFromProperties(Int32 Major, Int32 Minor, Int32 Patch)
        {
            String creationString = $"{Major.ToString()}.{Minor.ToString()}.{Patch.ToString()}";
            
            return new ModVersionTestDataPoint
            {
                CreationString = creationString,
                MajorVersion = Major,
                MinorVersion = Minor,
                PatchVersion = Patch,
                ObjectFromFor = ModVersion.For(creationString)
            };
        }

        private static List<ModVersionTestDataPoint> GetRandomizedTestData(Int32 count = 10)
        {
            var random = new Random();
            List<ModVersionTestDataPoint> randomTestData = new List<ModVersionTestDataPoint>();
            for (int i = 0; i < count; i++)
            {
                randomTestData.Add(CreateTestDataPointFromProperties(random.Next(0, Int32.MaxValue), random.Next(0, Int32.MaxValue), random.Next(0, Int32.MaxValue)));
            }

            return randomTestData;
        }
        #endregion
    }
}
