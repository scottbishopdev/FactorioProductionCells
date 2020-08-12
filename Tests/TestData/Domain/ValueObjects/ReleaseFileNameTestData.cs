using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FactorioProductionCells.Domain.ValueObjects;
using FactorioProductionCells.TestData.Domain.Entities;

namespace FactorioProductionCells.TestData.Domain.ValueObjects
{
    public static class ReleaseFileNameTestData
    {
        #region Static Data
        private static ReleaseFileNameTestDataPoint NotATestModReleaseFileNameTestDataPoint = CreateTestDataPointFromProperties("NotATestMod", ModVersion.For("2.5.17"));
        public static ReleaseFileName NotATestModReleaseFileName = NotATestModReleaseFileNameTestDataPoint.ObjectFromFor;
        
        // Test Mod
        private static ReleaseFileNameTestDataPoint TestModAlphaReleaseFileNameTestDataPoint = CreateTestDataPointFromProperties(
            ModName: ModTestData.TestModName,
            ModVersion: ModVersionTestData.TestModAlphaReleaseVersion);
        public static ReleaseFileName TestModAlphaReleaseFileName = TestModAlphaReleaseFileNameTestDataPoint.ObjectFromFor;

        private static ReleaseFileNameTestDataPoint TestModBetaReleaseFileNameTestDataPoint = CreateTestDataPointFromProperties(
            ModName: ModTestData.TestModName,
            ModVersion: ModVersionTestData.TestModBetaReleaseVersion);
        public static ReleaseFileName TestModBetaReleaseFileName = TestModBetaReleaseFileNameTestDataPoint.ObjectFromFor;

        private static ReleaseFileNameTestDataPoint TestModGammaReleaseFileNameTestDataPoint = CreateTestDataPointFromProperties(
            ModName: ModTestData.TestModName,
            ModVersion: ModVersionTestData.TestModGammaReleaseVersion);
        public static ReleaseFileName TestModGammaReleaseFileName = TestModGammaReleaseFileNameTestDataPoint.ObjectFromFor;

        // Bob's Functions Library Mod
        private static ReleaseFileNameTestDataPoint BobsFunctionsLibraryFirstReleaseFileNameTestDataPoint = CreateTestDataPointFromProperties(
            ModName: ModTestData.BobsFunctionsLibraryModName,
            ModVersion: ModVersionTestData.BobsFunctionsLibraryFirstReleaseVersion);
        public static ReleaseFileName BobsFunctionsLibraryFirstReleaseFileName = BobsFunctionsLibraryFirstReleaseFileNameTestDataPoint.ObjectFromFor;

        private static ReleaseFileNameTestDataPoint BobsFunctionsLibraryMiddleReleaseFileNameTestDataPoint = CreateTestDataPointFromProperties(
            ModName: ModTestData.BobsFunctionsLibraryModName,
            ModVersion: ModVersionTestData.BobsFunctionsLibraryMiddleReleaseVersion);
        public static ReleaseFileName BobsFunctionsLibraryMiddleReleaseFileName = BobsFunctionsLibraryMiddleReleaseFileNameTestDataPoint.ObjectFromFor;
        
        private static ReleaseFileNameTestDataPoint BobsFunctionsLibraryLatestReleaseFileNameTestDataPoint = CreateTestDataPointFromProperties(
            ModName: ModTestData.BobsFunctionsLibraryModName,
            ModVersion: ModVersionTestData.BobsFunctionsLibraryLatestReleaseVersion);
        public static ReleaseFileName BobsFunctionsLibraryLatestReleaseFileName = BobsFunctionsLibraryLatestReleaseFileNameTestDataPoint.ObjectFromFor;
        
        // Bob's Logistics Mod
        private static ReleaseFileNameTestDataPoint BobsLogisticsFirstReleaseFileNameTestDataPoint = CreateTestDataPointFromProperties(
            ModName: ModTestData.BobsLogisticsModName,
            ModVersion: ModVersionTestData.BobsLogisticsFirstReleaseVersion);
        public static ReleaseFileName BobsLogisticsFirstReleaseFileName = BobsLogisticsFirstReleaseFileNameTestDataPoint.ObjectFromFor;

        private static ReleaseFileNameTestDataPoint BobsLogisticsMiddleReleaseFileNameTestDataPoint = CreateTestDataPointFromProperties(
            ModName: ModTestData.BobsLogisticsModName,
            ModVersion: ModVersionTestData.BobsLogisticsMiddleReleaseVersion);
        public static ReleaseFileName BobsLogisticsMiddleReleaseFileName = BobsLogisticsMiddleReleaseFileNameTestDataPoint.ObjectFromFor;
        
        private static ReleaseFileNameTestDataPoint BobsLogisticsLatestReleaseFileNameTestDataPoint = CreateTestDataPointFromProperties(
            ModName: ModTestData.BobsLogisticsModName,
            ModVersion: ModVersionTestData.BobsLogisticsLatestReleaseVersion);
        public static ReleaseFileName BobsLogisticsLatestReleaseFileName = BobsLogisticsLatestReleaseFileNameTestDataPoint.ObjectFromFor;
        #endregion
        
        #region Static Test Data Collections
        private static List<ReleaseFileNameTestDataPoint> StaticTestData = new List<ReleaseFileNameTestDataPoint>()
        {
            NotATestModReleaseFileNameTestDataPoint,
            TestModAlphaReleaseFileNameTestDataPoint,
            TestModBetaReleaseFileNameTestDataPoint,
            BobsFunctionsLibraryFirstReleaseFileNameTestDataPoint,
            BobsFunctionsLibraryMiddleReleaseFileNameTestDataPoint,
            BobsFunctionsLibraryLatestReleaseFileNameTestDataPoint,
            BobsLogisticsFirstReleaseFileNameTestDataPoint,
            BobsLogisticsMiddleReleaseFileNameTestDataPoint,
            BobsLogisticsLatestReleaseFileNameTestDataPoint
        };

        public static IEnumerable<object[]> ValidStaticReleaseFileNamesWithModName() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.ModName };

        public static IEnumerable<object[]> ValidStaticReleaseFileNamesWithModVersion() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.ModVersion };

        public static IEnumerable<object[]> ValidStaticStringsWithModName() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.CreationString, dataPoint.ModName };

        public static IEnumerable<object[]> ValidStaticStringsWithModVersion() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.CreationString, dataPoint.ModVersion };

        public static IEnumerable<object[]> ValidStaticEqualReleaseFileNamePairs() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, new ReleaseFileName(dataPoint.ObjectFromFor) };

        public static IEnumerable<object[]> ValidStaticNonEqualReleaseFileNamePairs()
        {
            for (int i = 0; i < StaticTestData.Count; i++)
            {
                var secondDataPoint = i != StaticTestData.Count - 1 ? StaticTestData[i+1] : StaticTestData[0];
                yield return new object[] { StaticTestData[i].ObjectFromFor, secondDataPoint.ObjectFromFor };
            }
        }

        public static IEnumerable<object[]> ValidStaticReleaseFileNamesFromForWithStrings() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.CreationString };

        public static IEnumerable<object[]> ValidStaticReleaseFileNamesFromForWithModNameAndModVersion() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.ModName, dataPoint.ModVersion };
        #endregion

        #region Random Test Data Collections
        private static List<ReleaseFileNameTestDataPoint> RandomTestData = GetRandomizedTestData(10);

        public static IEnumerable<object[]> ValidRandomReleaseFileNamesWithModName() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.ModName };

        public static IEnumerable<object[]> ValidRandomReleaseFileNamesWithModVersion() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.ModVersion };

        public static IEnumerable<object[]> ValidRandomStringsWithModName() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.CreationString, dataPoint.ModName };

        public static IEnumerable<object[]> ValidRandomStringsWithModVersion() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.CreationString, dataPoint.ModVersion };

        public static IEnumerable<object[]> ValidRandomEqualReleaseFileNamePairs() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, new ReleaseFileName(dataPoint.ObjectFromFor) };

        public static IEnumerable<object[]> ValidRandomReleaseFileNamesFromForWithStrings() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.CreationString };

        public static IEnumerable<object[]> ValidRandomReleaseFileNamesFromForWithModNameAndModVersion() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.ModName, dataPoint.ModVersion };

        public static IEnumerable<object[]> RandomReleaseFileNamesWithModNameTooLong(Int32 count = 3)
        {
            for (int i = 0; i < count; i++)
            {
                var resultStringBuilder = new StringBuilder();
                resultStringBuilder.Append(ModTestData.GenerateRandomizedModNameTooLong());
                resultStringBuilder.Append("_");
                resultStringBuilder.Append(ModVersionTestData.GenerateValidRandomizedModVersion());
                resultStringBuilder.Append(".zip");

                yield return  new object[] { resultStringBuilder.ToString() };
            }
        }
        #endregion

        #region Helper Methods and Struct
        public struct ReleaseFileNameTestDataPoint
        {
            public String CreationString;
            public String ModName;
            public ModVersion ModVersion;
            public ReleaseFileName ObjectFromFor;
        }

        public static String GenerateValidRandomizedReleaseFileNameString()
        {
            return $"{ModTestData.GenerateValidRandomizedModName()}_{ModVersionTestData.GenerateValidRandomizedModVersionString()}.zip";
        }

        public static String GenerateValidRandomizedReleaseFileNameStringWithModName(String modName)
        {
            return $"{modName}_{ModVersionTestData.GenerateValidRandomizedModVersionString()}.zip";
        }

        public static ReleaseFileName GenerateValidRandomizedReleaseFileName()
        {
            return ReleaseFileName.For(GenerateValidRandomizedReleaseFileNameString());
        }

        private static ReleaseFileNameTestDataPoint CreateTestDataPointFromProperties(String ModName, ModVersion ModVersion)
        {
            String creationString = $"{ModName}_{ModVersion}.zip";
            
            return new ReleaseFileNameTestDataPoint
            {
                CreationString = creationString,
                ModName = ModName,
                ModVersion = ModVersion,
                ObjectFromFor = ReleaseFileName.For(creationString)
            };
        }

        private static List<ReleaseFileNameTestDataPoint> GetRandomizedTestData(Int32 count = 10)
        {
            var random = new Random();
            List<ReleaseFileNameTestDataPoint> randomTestData = new List<ReleaseFileNameTestDataPoint>();
            for (int i = 0; i < count; i++)
            {
                randomTestData.Add(CreateTestDataPointFromProperties(ModTestData.GenerateValidRandomizedModName(), ModVersionTestData.GenerateValidRandomizedModVersion()));
            }

            return randomTestData;
        }
        #endregion
    }
}
