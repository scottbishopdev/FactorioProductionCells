using System;
using System.Collections.Generic;
using System.Linq;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.TestData.Common;

namespace FactorioProductionCells.TestData.Domain.Entities
{
    public static class ModTitleTestData
    {
        #region Static Data
        // Test Mod
        public static ModTitleTestDataPoint TestModEnglishTitleTestDataPoint = CreateTestDataPointFromProperties(
            Language: LanguageTestData.EnglishWithId,
            Title: "Test Mod");
        public static ModTitle TestModEnglishTitle = TestModEnglishTitleTestDataPoint.ObjectFromConstructor;

        public static ModTitleTestDataPoint TestModGermanTitleTestDataPoint = CreateTestDataPointFromProperties(
            Language: LanguageTestData.GermanWithId,
            Title: "Prüfung Modifikator");
        public static ModTitle TestModGermanTitle = TestModGermanTitleTestDataPoint.ObjectFromConstructor;
        
        public static List<ModTitle> TestModTitles = new List<ModTitle>
        {
            TestModEnglishTitle,
            TestModGermanTitle
        };

        // Bob's Functions Library Mod
        public static ModTitleTestDataPoint BobsFunctionsLibraryModEnglishTitleTestDataPoint = CreateTestDataPointFromProperties(
            Language: LanguageTestData.EnglishWithId,
            Title: "Bob's Functions Library Mod");
        public static ModTitle BobsFunctionsLibraryModEnglishTitle = BobsFunctionsLibraryModEnglishTitleTestDataPoint.ObjectFromConstructor;

        public static ModTitleTestDataPoint BobsFunctionsLibraryModGermanTitleTestDataPoint = CreateTestDataPointFromProperties(
            Language: LanguageTestData.GermanWithId,
            Title: "Bobs Funktionsbibliothek Mod");
        public static ModTitle BobsFunctionsLibraryModGermanTitle = BobsFunctionsLibraryModGermanTitleTestDataPoint.ObjectFromConstructor;

        public static List<ModTitle> BobsFunctionsLibraryModTitles = new List<ModTitle>
        {
            BobsFunctionsLibraryModEnglishTitle,
            BobsFunctionsLibraryModGermanTitle
        };

        // Bob's Logistics Mod
        public static ModTitleTestDataPoint BobsLogisticsModEnglishTitleTestDataPoint = CreateTestDataPointFromProperties(
            Language: LanguageTestData.EnglishWithId,
            Title: "Bob's Logistics Mod");
        public static ModTitle BobsLogisticsModEnglishTitle = BobsLogisticsModEnglishTitleTestDataPoint.ObjectFromConstructor;

        public static ModTitleTestDataPoint BobsLogisticsModGermanTitleTestDataPoint = CreateTestDataPointFromProperties(
            Language: LanguageTestData.GermanWithId,
            Title: "Bob's Logistik Mod");
        public static ModTitle BobsLogisticsModGermanTitle = BobsLogisticsModEnglishTitleTestDataPoint.ObjectFromConstructor;

        public static List<ModTitle> BobsLogisticsModTitles = new List<ModTitle>
        {
            BobsLogisticsModEnglishTitle,
            BobsLogisticsModGermanTitle
        };
        #endregion

        #region Static Test Data Collections
        private static List<ModTitleTestDataPoint> StaticTestData = new List<ModTitleTestDataPoint>()
        {
            TestModEnglishTitleTestDataPoint,
            TestModGermanTitleTestDataPoint,
            BobsFunctionsLibraryModEnglishTitleTestDataPoint,
            BobsFunctionsLibraryModGermanTitleTestDataPoint,
            BobsLogisticsModEnglishTitleTestDataPoint,
            BobsLogisticsModGermanTitleTestDataPoint
        };

        public static IEnumerable<object[]> ValidStaticModTitlesWithLanguage() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.Language };

        public static IEnumerable<object[]> ValidStaticModTitlesWithLanguageId() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.LanguageId };

        public static IEnumerable<object[]> ValidStaticModTitlesWithTitle() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.Title };

        public static IEnumerable<object[]> ValidStaticModTitlesCreationProperties() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.Language, dataPoint.Title };

        public static IEnumerable<object[]> ValidStaticEqualModTitlePairs() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromConstructor, new ModTitle(dataPoint.ObjectFromConstructor) };

        public static IEnumerable<object[]> ValidStaticNonEqualModTitlePairs()
        {
            for (int i = 0; i < StaticTestData.Count; i++)
            {
                var secondDataPoint = i != StaticTestData.Count - 1 ? StaticTestData[i+1] : StaticTestData[0];
                yield return new object[] { StaticTestData[i].ObjectFromConstructor, secondDataPoint.ObjectFromConstructor };
            }
        }
        #endregion

        #region Random Test Data Collections
        private static List<ModTitleTestDataPoint> RandomTestData = GetRandomizedTestData(10);

        public static IEnumerable<object[]> ValidRandomModTitlesWithLanguage() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.Language };

        public static IEnumerable<object[]> ValidRandomModTitlesWithLanguageId() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.LanguageId };

        public static IEnumerable<object[]> ValidRandomModTitlesWithTitle() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.Title };

        public static IEnumerable<object[]> ValidRandomModTitlesCreationProperties() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.Language, dataPoint.Title };

        public static IEnumerable<object[]> ValidRandomEqualModTitlePairs() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromConstructor, new ModTitle(dataPoint.ObjectFromConstructor) };
        #endregion

        #region Helper Methods and Struct
        public struct ModTitleTestDataPoint
        {
            public String Title;
            public Language Language;
            public Guid LanguageId;
            public ModTitle ObjectFromConstructor;
        }

        private static ModTitleTestDataPoint CreateTestDataPointFromProperties(Language Language, String Title)
        {
            return new ModTitleTestDataPoint
            {
                Title = Title,
                Language = Language,
                LanguageId = Language.Id,
                ObjectFromConstructor = new ModTitle(Language, Title)
            };
        }

        public static String GenerateRandomizedModTitleString()
        {
            var random = new Random();
            return TestDataHelpers.GetRandomizedUnicodeCharacterString(random.Next(1, ModTitle.TitleLength));
        }

        private static ModTitleTestDataPoint GenerateValidRandomizedTestDataPoint()
        {
            Language randomLanguage = LanguageTestData.GenerateValidRandomizedLanguage();

            return CreateTestDataPointFromProperties(
                Title: GenerateRandomizedModTitleString(),
                Language: randomLanguage
            );
        }

        public static ModTitle GenerateValidRandomizedModTitle()
        {
            return GenerateValidRandomizedTestDataPoint().ObjectFromConstructor;
        }

        private static List<ModTitleTestDataPoint> GetRandomizedTestData(Int32 count = 10)
        {
            List<ModTitleTestDataPoint> randomTestData = new List<ModTitleTestDataPoint>();
            while (randomTestData.Count < count)
            {
                ModTitleTestDataPoint dataPoint = GenerateValidRandomizedTestDataPoint();
                if (!randomTestData.Any(l => l.Equals(dataPoint))) randomTestData.Add(dataPoint);
            }

            return randomTestData;
        }
        #endregion
    }
}
