using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FactorioProductionCells.Domain.ValueObjects;
using FactorioProductionCells.TestData.Domain.Entities;
using FactorioProductionCells.TestData.Common;

namespace FactorioProductionCells.TestData.Domain.ValueObjects
{
    public static class ReleaseDownloadUrlTestData
    {
        #region Static Data
        public static ReleaseDownloadUrlTestDataPoint NotATestModDownloadUrlTestDataPoint = CreateTestDataPointFromProperties(
            ModName: "NotATestMod",
            ReleaseToken: "1234567890abcd1234567890");
        public static ReleaseDownloadUrl NotATestModDownloadUrl = NotATestModDownloadUrlTestDataPoint.ObjectFromFor;
        
        // Test Mod
        public static ReleaseDownloadUrlTestDataPoint TestModDownloadUrlTestDataPoint = CreateTestDataPointFromProperties(
            ModName: ModTestData.TestModName,
            ReleaseToken: "1234567890abcd1234567890");
        public static ReleaseDownloadUrl TestModDownloadUrl = TestModDownloadUrlTestDataPoint.ObjectFromFor;

        // Bob's Functions Library Mod
        public static ReleaseDownloadUrlTestDataPoint BobsFunctionsLibraryFirstReleaseDownloadUrlTestDataPoint = CreateTestDataPointFromProperties(
            ModName: ModTestData.BobsFunctionsLibraryModName,
            ReleaseToken: "5a5f1ae6adcc441024d72427");
        public static ReleaseDownloadUrl BobsFunctionsLibraryFirstReleaseDownloadUrl = BobsFunctionsLibraryFirstReleaseDownloadUrlTestDataPoint.ObjectFromFor;

        public static ReleaseDownloadUrlTestDataPoint BobsFunctionsLibraryMiddleReleaseDownloadUrlTestDataPoint = CreateTestDataPointFromProperties(
            ModName: ModTestData.BobsFunctionsLibraryModName,
            ReleaseToken: "5a5f1ae6adcc441024d74414");
        public static ReleaseDownloadUrl BobsFunctionsLibraryMiddleReleaseDownloadUrl = BobsFunctionsLibraryMiddleReleaseDownloadUrlTestDataPoint.ObjectFromFor;
        
        public static ReleaseDownloadUrlTestDataPoint BobsFunctionsLibraryLatestReleaseDownloadUrlTestDataPoint = CreateTestDataPointFromProperties(
            ModName: ModTestData.BobsFunctionsLibraryModName,
            ReleaseToken: "5e9daef69e85eb000cee2549");
        public static ReleaseDownloadUrl BobsFunctionsLibraryLatestReleaseDownloadUrl = BobsFunctionsLibraryLatestReleaseDownloadUrlTestDataPoint.ObjectFromFor;
        
        // Bob's Logistics Mod
        public static ReleaseDownloadUrlTestDataPoint BobsLogisticsFirstReleaseDownloadUrlTestDataPoint = CreateTestDataPointFromProperties(
            ModName: ModTestData.BobsLogisticsModName,
            ReleaseToken: "5a5f1ae6adcc441024d72978");
        public static ReleaseDownloadUrl BobsLogisticsFirstReleaseDownloadUrl = BobsLogisticsFirstReleaseDownloadUrlTestDataPoint.ObjectFromFor;
        
        public static ReleaseDownloadUrlTestDataPoint BobsLogisticsMiddleReleaseDownloadUrlTestDataPoint = CreateTestDataPointFromProperties(
            ModName: ModTestData.BobsLogisticsModName,
            ReleaseToken: "5a5f1ae6adcc441024d73d85");
        public static ReleaseDownloadUrl BobsLogisticsMiddleReleaseDownloadUrl = BobsLogisticsMiddleReleaseDownloadUrlTestDataPoint.ObjectFromFor;
        
        public static ReleaseDownloadUrlTestDataPoint BobsLogisticsLatestReleaseDownloadUrlTestDataPoint = CreateTestDataPointFromProperties(
            ModName: ModTestData.BobsLogisticsModName,
            ReleaseToken: "5e989546446e1d000e494797");
        public static ReleaseDownloadUrl BobsLogisticsLatestReleaseDownloadUrl = BobsLogisticsLatestReleaseDownloadUrlTestDataPoint.ObjectFromFor;
        #endregion

        #region Static Test Data Collections
        private static List<ReleaseDownloadUrlTestDataPoint> StaticTestData = new List<ReleaseDownloadUrlTestDataPoint>()
        {
            NotATestModDownloadUrlTestDataPoint,
            TestModDownloadUrlTestDataPoint,
            BobsFunctionsLibraryFirstReleaseDownloadUrlTestDataPoint,
            BobsFunctionsLibraryMiddleReleaseDownloadUrlTestDataPoint,
            BobsFunctionsLibraryLatestReleaseDownloadUrlTestDataPoint,
            BobsLogisticsFirstReleaseDownloadUrlTestDataPoint,
            BobsLogisticsMiddleReleaseDownloadUrlTestDataPoint,
            BobsLogisticsLatestReleaseDownloadUrlTestDataPoint
        };

        public static IEnumerable<object[]> ValidStaticReleaseDownloadUrlsWithModName() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.ModName };

        public static IEnumerable<object[]> ValidStaticReleaseDownloadUrlsWithReleaseToken() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.ReleaseToken };

        public static IEnumerable<object[]> ValidStaticStringsWithModName() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.CreationString, dataPoint.ModName };

        public static IEnumerable<object[]> ValidStaticStringsWithReleaseToken() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.CreationString, dataPoint.ReleaseToken };

        public static IEnumerable<object[]> ValidStaticEqualReleaseDownloadUrlPairs() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, new ReleaseDownloadUrl(dataPoint.ObjectFromFor) };

        public static IEnumerable<object[]> ValidStaticNonEqualReleaseDownloadUrlPairs()
        {
            for (int i = 0; i < StaticTestData.Count; i++)
            {
                var secondDataPoint = i != StaticTestData.Count - 1 ? StaticTestData[i+1] : StaticTestData[0];
                yield return new object[] { StaticTestData[i].ObjectFromFor, secondDataPoint.ObjectFromFor };
            }
        }

        public static IEnumerable<object[]> ValidStaticReleaseDownloadUrlsFromForWithStrings() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.CreationString };

        public static IEnumerable<object[]> ValidStaticReleaseDownloadUrlsFromForWithModNameAndReleaseToken() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.ModName, dataPoint.ReleaseToken };
        #endregion

        #region Random Test Data Collections
        private static List<ReleaseDownloadUrlTestDataPoint> RandomTestData = GetRandomizedTestData(10);

        public static IEnumerable<object[]> ValidRandomReleaseDownloadUrlsWithModName() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.ModName };

        public static IEnumerable<object[]> ValidRandomReleaseDownloadUrlsWithReleaseToken() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.ReleaseToken };

        public static IEnumerable<object[]> ValidRandomStringsWithModName() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.CreationString, dataPoint.ModName };

        public static IEnumerable<object[]> ValidRandomStringsWithReleaseToken() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.CreationString, dataPoint.ReleaseToken };

        public static IEnumerable<object[]> ValidRandomEqualReleaseDownloadUrlPairs() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, new ReleaseDownloadUrl(dataPoint.ObjectFromFor) };

        public static IEnumerable<object[]> ValidRandomReleaseDownloadUrlsFromForWithStrings() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.CreationString };

        public static IEnumerable<object[]> ValidRandomReleaseDownloadUrlsFromForWithModNameAndReleaseToken() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.ModName, dataPoint.ReleaseToken };

        public static IEnumerable<object[]> RandomReleaseDownloadUrlsWithModNameTooLong(Int32 count = 3)
        {
            for (int i = 0; i < count; i++)
            {
                var resultStringBuilder = new StringBuilder();
                resultStringBuilder.Append("/download/");
                resultStringBuilder.Append(ModTestData.GenerateRandomizedModNameTooLong().Replace(" ", "%20"));
                resultStringBuilder.Append("/");
                resultStringBuilder.Append(ReleaseDownloadUrlTestData.GenerateValidRandomizedReleaseDownloadUrlToken());

                yield return  new object[] { resultStringBuilder.ToString() };
            }
        }

        public static IEnumerable<object[]> RandomReleaseDownloadUrlsWithReleaseTokenTooLong(Int32 count = 3)
        {
            for (int i = 0; i < count; i++)
            {
                var resultStringBuilder = new StringBuilder();
                resultStringBuilder.Append("/download/");
                resultStringBuilder.Append(ReleaseDownloadUrlTestData.GenerateValidRandomizedReleaseDownloadUrlModName());
                resultStringBuilder.Append("/");
                resultStringBuilder.Append(ReleaseDownloadUrlTestData.GenerateRandomizedReleaseDownloadUrlTokenTooLong());

                yield return  new object[] { resultStringBuilder.ToString() };
            }
        }
        #endregion

        #region Helper Methods and Struct
        public struct ReleaseDownloadUrlTestDataPoint
        {
            public String CreationString;
            public String ModName;
            public String ReleaseToken;
            public ReleaseDownloadUrl ObjectFromFor;
        }

        public static String GenerateValidRandomizedReleaseDownloadUrlModName()
        {
            return ModTestData.GenerateValidRandomizedModName().Replace(" ", "%20");
        }

        public static String GenerateValidRandomizedReleaseDownloadUrlToken()
        {
            return TestDataHelpers.GetRandomCharacterStringFromSet(ReleaseDownloadUrl.ValidReleaseTokenCharacters, ReleaseDownloadUrl.ReleaseTokenLength);
        }

        public static String GenerateRandomizedReleaseDownloadUrlTokenTooLong()
        {
            var random = new Random();
            return TestDataHelpers.GetRandomCharacterStringFromSet(ReleaseDownloadUrl.ValidReleaseTokenCharacters, ReleaseDownloadUrl.ReleaseTokenLength + random.Next(1, 100));
        }

        public static String GenerateValidRandomizedReleaseDownloadUrlString()
        {
            var random = new Random();
            return $"/download/{GenerateValidRandomizedReleaseDownloadUrlModName()}/{GenerateValidRandomizedReleaseDownloadUrlToken()}";
        }

        public static String GenerateValidRandomizedReleaseDownloadUrlStringWithModName(String modName)
        {
            var random = new Random();
            return $"/download/{modName.Replace(" ", "%20")}/{GenerateValidRandomizedReleaseDownloadUrlToken()}";
        }

        public static ReleaseDownloadUrl GenerateValidRandomizedReleaseDownloadUrl()
        {
            return ReleaseDownloadUrl.For(GenerateValidRandomizedReleaseDownloadUrlString());
        }

        private static ReleaseDownloadUrlTestDataPoint CreateTestDataPointFromProperties(String ModName, String ReleaseToken)
        {
            String creationString = $"/download/{ModName}/{ReleaseToken}";
            
            return new ReleaseDownloadUrlTestDataPoint
            {
                CreationString = creationString,
                ModName = ModName,
                ReleaseToken = ReleaseToken,
                ObjectFromFor = ReleaseDownloadUrl.For(creationString)
            };
        }

        private static List<ReleaseDownloadUrlTestDataPoint> GetRandomizedTestData(Int32 count = 10)
        {
            var random = new Random();
            List<ReleaseDownloadUrlTestDataPoint> randomTestData = new List<ReleaseDownloadUrlTestDataPoint>();
            for (int i = 0; i < count; i++)
            {
                randomTestData.Add(CreateTestDataPointFromProperties(GenerateValidRandomizedReleaseDownloadUrlModName(), GenerateValidRandomizedReleaseDownloadUrlToken()));
            }

            return randomTestData;
        }
        #endregion
    }
}
