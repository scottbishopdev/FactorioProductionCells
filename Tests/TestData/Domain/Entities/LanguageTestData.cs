using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.TestData.Domain.Entities
{
    public static class LanguageTestData
    {
        #region Static Data
        // English
        public static LanguageTestDataPoint EnglishTestDataPoint = CreateTestDataPointFromProperties(
            Id: new Guid("0e557edc-ae73-4799-8731-80761d775c8c"),
            EnglishName: "English",
            LanguageTag: "en",
            ExpectedCulture: new CultureInfo("en"),
            IsDefault: true);
        public static Language EnglishWithId = EnglishTestDataPoint.ObjectWithId;
        public static Language EnglishWithoutId = EnglishTestDataPoint.ObjectWithoutId;
        
        // English - United States
        public static LanguageTestDataPoint EnglishUnitedStatesTestDataPoint = CreateTestDataPointFromProperties(
            Id: new Guid("2095a205-d17b-4ab9-bb6e-3aad0d467d19"),
            EnglishName: "English - United States",
            LanguageTag: "en-US",
            ExpectedCulture: new CultureInfo("en-us"),
            IsDefault: false);
        public static Language EnglishUnitedStatesWithId = EnglishUnitedStatesTestDataPoint.ObjectWithId;
        public static Language EnglishUnitedStatesWithoutId = EnglishUnitedStatesTestDataPoint.ObjectWithoutId;

        // English - Great Britian
        public static LanguageTestDataPoint EnglishGreatBritianWithTestDataPoint = CreateTestDataPointFromProperties(
            Id: new Guid("00b674f1-9824-4326-86d2-a9674203493f"),
            EnglishName: "English - Great Britian",
            LanguageTag: "en-GB",
            ExpectedCulture: new CultureInfo("en-gb"),
            IsDefault: false);
        public static Language EnglishGreatBritianWithId = EnglishGreatBritianWithTestDataPoint.ObjectWithId;
        public static Language EnglishGreatBritianWithoutId = EnglishGreatBritianWithTestDataPoint.ObjectWithoutId;

        // German
        public static LanguageTestDataPoint GermanTestDataPoint = CreateTestDataPointFromProperties(
            Id: new Guid("6f7bd7a7-eecb-4a30-9731-59182d25bcf2"),
            EnglishName: "German",
            LanguageTag: "de",
            ExpectedCulture: new CultureInfo("de"),
            IsDefault: false);
        public static Language GermanWithId = GermanTestDataPoint.ObjectWithId;
        public static Language GermanWithoutId = GermanTestDataPoint.ObjectWithoutId;

        // German - Germany
        public static LanguageTestDataPoint GermanGermanyTestDataPoint = CreateTestDataPointFromProperties(
            Id: new Guid("f7d737ba-13ae-41c6-84b4-d2d9fdf30dea"),
            EnglishName: "German - Germany",
            LanguageTag: "de-DE",
            ExpectedCulture: new CultureInfo("de-de"),
            IsDefault: false);
        public static Language GermanGermanyWithId = GermanGermanyTestDataPoint.ObjectWithId;
        public static Language GermanGermanyWithoutId = GermanGermanyTestDataPoint.ObjectWithoutId;

        // German - Austria
        public static LanguageTestDataPoint GermanAustriaTestDataPoint = CreateTestDataPointFromProperties(
            Id: new Guid("efe67ffb-4073-4934-b94c-a5dd2c140132"),
            EnglishName: "German - Austria",
            LanguageTag: "de-AT",
            ExpectedCulture: new CultureInfo("de-at"),
            IsDefault: false);
        public static Language GermanAustriaWithId = GermanAustriaTestDataPoint.ObjectWithId;
        public static Language GermanAustriaWithoutId = GermanAustriaTestDataPoint.ObjectWithoutId;

        // Collections
        public static List<Language> LanguagesWithIds = new List<Language>()
        {
            EnglishWithId,
            EnglishUnitedStatesWithId,
            EnglishGreatBritianWithId,
            GermanWithId,
            GermanGermanyWithId,
            GermanAustriaWithId
        };

        public static List<Language> LanguagesWithoutIds = new List<Language>()
        {
            EnglishWithoutId,
            EnglishUnitedStatesWithoutId,
            EnglishGreatBritianWithoutId,
            GermanWithoutId,
            GermanGermanyWithoutId,
            GermanAustriaWithoutId
        };
        #endregion

        #region Static Test Data Collections
        private static List<LanguageTestDataPoint> StaticTestData = new List<LanguageTestDataPoint>()
        {
            EnglishTestDataPoint,
            EnglishUnitedStatesTestDataPoint,
            EnglishGreatBritianWithTestDataPoint,
            GermanTestDataPoint,
            GermanGermanyTestDataPoint,
            GermanAustriaTestDataPoint
        };

        public static IEnumerable<object[]> ValidStaticLanguagesWithId() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectWithId, dataPoint.Id };

        public static IEnumerable<object[]> ValidStaticLanguagesWithEnglishName() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectWithId, dataPoint.EnglishName };
        
        public static IEnumerable<object[]> ValidStaticLanguagesWithLanguageTag() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectWithId, dataPoint.LanguageTag };

        public static IEnumerable<object[]> ValidStaticLanguagesWithIsDefault() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectWithId, dataPoint.IsDefault };

        public static IEnumerable<object[]> ValidStaticLanguagesWithExpectedCulture() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectWithId, dataPoint.ExpectedCulture };

        public static IEnumerable<object[]> ValidStaticLanguagesCreationProperties() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.EnglishName, dataPoint.LanguageTag, dataPoint.IsDefault };

        public static IEnumerable<object[]> ValidStaticLanguagesCreationPropertiesWithExpectedCulture() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.EnglishName, dataPoint.LanguageTag, dataPoint.IsDefault, dataPoint.ExpectedCulture };

        public static IEnumerable<object[]> ValidStaticLanguagesCreationPropertiesWithId() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.EnglishName, dataPoint.LanguageTag, dataPoint.IsDefault, dataPoint.Id };

        public static IEnumerable<object[]> ValidStaticEqualLanguagePairs() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectWithId, new Language(dataPoint.ObjectWithId) };

        public static IEnumerable<object[]> ValidStaticNonEqualLanguagePairs()
        {
            for (int i = 0; i < StaticTestData.Count; i++)
            {
                var secondDataPoint = i != StaticTestData.Count - 1 ? StaticTestData[i+1] : StaticTestData[0];
                yield return new object[] { StaticTestData[i].ObjectWithId, secondDataPoint.ObjectWithId };
            }
        }
        #endregion

        #region Random Test Data Collections
        private static List<LanguageTestDataPoint> RandomTestData = GetRandomizedTestData(10);

        public static IEnumerable<object[]> ValidRandomLanguagesWithId() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectWithId, dataPoint.Id };

        public static IEnumerable<object[]> ValidRandomLanguagesWithEnglishName() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectWithId, dataPoint.EnglishName };
        
        public static IEnumerable<object[]> ValidRandomLanguagesWithLanguageTag() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectWithId, dataPoint.LanguageTag };

        public static IEnumerable<object[]> ValidRandomLanguagesWithIsDefault() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectWithId, dataPoint.IsDefault };

        public static IEnumerable<object[]> ValidRandomLanguagesWithExpectedCulture() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectWithId, dataPoint.ExpectedCulture };

        public static IEnumerable<object[]> ValidRandomLanguagesCreationProperties() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.EnglishName, dataPoint.LanguageTag, dataPoint.IsDefault };
        
        public static IEnumerable<object[]> ValidRandomLanguagesCreationPropertiesWithExpectedCulture() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.EnglishName, dataPoint.LanguageTag, dataPoint.IsDefault, dataPoint.ExpectedCulture };

        public static IEnumerable<object[]> ValidRandomLanguagesCreationPropertiesWithId() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.EnglishName, dataPoint.LanguageTag, dataPoint.IsDefault, dataPoint.Id };

        public static IEnumerable<object[]> ValidRandomEqualLanguagePairs() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectWithId, new Language(dataPoint.ObjectWithId) };
        #endregion

        #region Helper Methods and Struct
        public struct LanguageTestDataPoint
        {
            public Guid Id;
            public String EnglishName;
            public String LanguageTag;
            public Boolean IsDefault;
            public CultureInfo ExpectedCulture;
            public Language ObjectWithId;
            public Language ObjectWithoutId;
        }

        private static LanguageTestDataPoint CreateTestDataPointFromProperties(Guid Id, String EnglishName, String LanguageTag, CultureInfo ExpectedCulture, Boolean IsDefault)
        {
            return new LanguageTestDataPoint
            {
                Id = Id,
                EnglishName = EnglishName,
                LanguageTag = LanguageTag,
                IsDefault = IsDefault,
                ExpectedCulture = ExpectedCulture,
                ObjectWithId = new Language(Id, EnglishName, LanguageTag, IsDefault),
                ObjectWithoutId = new Language(EnglishName, LanguageTag, IsDefault)
            };
        }

        private static LanguageTestDataPoint GenerateValidRandomizedTestDataPoint()
        {
            var random = new Random();
            CultureInfo[] allCultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures);
            allCultures.Concat(CultureInfo.GetCultures(CultureTypes.SpecificCultures)).ToArray();
            allCultures = allCultures.Where(c => !c.EnglishName.Contains("Invariant Language")).ToArray();
            CultureInfo randomCulture = allCultures[random.Next(allCultures.Length)];

            return CreateTestDataPointFromProperties(
                Id: new Guid(),
                EnglishName: randomCulture.EnglishName,
                LanguageTag: randomCulture.Name,
                ExpectedCulture: randomCulture,
                IsDefault: false
            );
        }

        public static Language GenerateValidRandomizedLanguage()
        {
            return GenerateValidRandomizedTestDataPoint().ObjectWithId;
        }

        private static List<LanguageTestDataPoint> GetRandomizedTestData(Int32 count = 10)
        {
            List<LanguageTestDataPoint> randomTestData = new List<LanguageTestDataPoint>();
            while (randomTestData.Count < count)
            {
                LanguageTestDataPoint dataPoint = GenerateValidRandomizedTestDataPoint();
                if (!randomTestData.Any(l => l.LanguageTag == dataPoint.LanguageTag)) randomTestData.Add(dataPoint);
            }

            return randomTestData;
        }
        #endregion
    }
}
