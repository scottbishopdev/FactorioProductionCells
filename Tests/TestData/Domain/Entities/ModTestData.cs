
using System;
using System.Collections.Generic;
using System.Linq;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.TestData.Common;

namespace FactorioProductionCells.TestData.Domain.Entities
{
    public static class ModTestData
    {
        #region Static Data
        // Test Mod
        public const String TestModName = "testmod";
        public static ModTestDataPoint TestModTestDataPoint = CreateTestDataPointFromProperties(
            Name: TestModName,
            Titles: ModTitleTestData.TestModTitles,
            Releases: ReleaseTestData.TestModReleases);
        public static Mod TestMod = TestModTestDataPoint.ObjectFromConstructor;

        // Bob's Functions Library Mod
        public const String BobsFunctionsLibraryModName = "boblibrary";
        public static ModTestDataPoint BobsFunctionsLibraryModTestDataPoint = CreateTestDataPointFromProperties(
            Name: BobsFunctionsLibraryModName,
            Titles: ModTitleTestData.BobsFunctionsLibraryModTitles,
            Releases: ReleaseTestData.BobsFunctionsLibraryModReleases);
        public static Mod BobsFunctionsLibraryMod = BobsFunctionsLibraryModTestDataPoint.ObjectFromConstructor;

        // Bob's Logistics Mod
        public const String BobsLogisticsModName = "boblogistics";
        public static ModTestDataPoint BobsLogisticsModTestDataPoint = CreateTestDataPointFromProperties(
            Name: BobsLogisticsModName,
            Titles: ModTitleTestData.BobsLogisticsModTitles,
            Releases: ReleaseTestData.BobsLogisticsModReleases);
        public static Mod BobsLogisticsMod = BobsLogisticsModTestDataPoint.ObjectFromConstructor;

        public static List<Mod> AllMods = new List<Mod>()
        {
            TestMod,
            BobsFunctionsLibraryMod,
            BobsLogisticsMod
        };
        #endregion

        #region Static Test Data Collections
        private static List<ModTestDataPoint> StaticTestData = new List<ModTestDataPoint>()
        {
            TestModTestDataPoint,
            BobsFunctionsLibraryModTestDataPoint,
            BobsLogisticsModTestDataPoint
        };

        public static IEnumerable<object[]> ValidStaticModsWithName() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.Name };

        public static IEnumerable<object[]> ValidStaticModsWithTitles() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.Titles };

        public static IEnumerable<object[]> ValidStaticModsWithReleases() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.Releases };

        public static IEnumerable<object[]> ValidStaticModsCreationProperties() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.Name, dataPoint.Titles, dataPoint.Releases };

        public static IEnumerable<object[]> ValidStaticEqualModPairs() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromConstructor, new Mod(dataPoint.ObjectFromConstructor) };

        public static IEnumerable<object[]> ValidStaticNonEqualModPairs()
        {
            for (int i = 0; i < StaticTestData.Count; i++)
            {
                var secondDataPoint = i != StaticTestData.Count - 1 ? StaticTestData[i+1] : StaticTestData[0];
                yield return new object[] { StaticTestData[i].ObjectFromConstructor, secondDataPoint.ObjectFromConstructor };
            }
        }
        #endregion

        #region Random Test Data Collections
        private static List<ModTestDataPoint> RandomTestData = GetRandomizedTestData(10);

        public static IEnumerable<object[]> ValidRandomModsWithName() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.Name };

        public static IEnumerable<object[]> ValidRandomModsWithTitles() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.Titles };

        public static IEnumerable<object[]> ValidRandomModsWithReleases() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.Releases };

        public static IEnumerable<object[]> ValidRandomModsCreationProperties() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.Name, dataPoint.Titles, dataPoint.Releases };

        public static IEnumerable<object[]> ValidRandomEqualModPairs() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromConstructor, new Mod(dataPoint.ObjectFromConstructor) };
        #endregion

        #region Helper Methods and Struct
        public struct ModTestDataPoint
        {
            public String Name;
            public List<ModTitle> Titles;
            public List<Release> Releases;
            public Mod ObjectFromConstructor;
        }

        private static ModTestDataPoint CreateTestDataPointFromProperties(String Name, List<ModTitle> Titles, List<Release> Releases)
        {
            List<ModTitle> modTitles = new List<ModTitle>();
            List<Release> releases = new List<Release>();

            foreach (ModTitle title in Titles) modTitles.Add(title);
            foreach (Release release in Releases) releases.Add(release);
            
            return new ModTestDataPoint
            {
                Name = Name,
                Titles = modTitles,
                Releases = releases,
                ObjectFromConstructor = new Mod(Name, Titles, Releases)
            };
        }

        public static String GenerateValidRandomizedModName()
        {
            var random = new Random();
            return TestDataHelpers.GetRandomCharacterStringFromSet(Mod.ValidModNameCharacters, random.Next(1, Mod.NameLength)).Trim();
        }

        public static String GenerateRandomizedModNameTooLong()
        {
            var random = new Random();
            return TestDataHelpers.GetRandomCharacterStringFromSet(Mod.ValidModNameCharacters, Mod.NameLength + random.Next(1, 100)).Trim();
        }

        public static Mod GenerateValidRandomizedMod()
        {
            return GenerateValidRandomizedTestDataPoint().ObjectFromConstructor;
        }

        private static ModTestDataPoint GenerateValidRandomizedTestDataPoint()
        {
            var random = new Random();
            
            String randomName = GenerateValidRandomizedModName();

            List<ModTitle> randomizedModTitles = new List<ModTitle>();
            for (int i = 0; i < random.Next(1, 6); i++)
            {
                randomizedModTitles.Add(ModTitleTestData.GenerateValidRandomizedModTitle());
            }

            List<Release> randomizedReleases = new List<Release>();
            for (int i = 0; i < random.Next(1, 6); i++)
            {
                randomizedReleases.Add(ReleaseTestData.GenerateValidRandomizedReleaseWithModName(randomName));
            }

            return CreateTestDataPointFromProperties(
                Name: randomName,
                Titles: randomizedModTitles,
                Releases: randomizedReleases
            );
        }

        private static List<ModTestDataPoint> GetRandomizedTestData(Int32 count = 10)
        {
            List<ModTestDataPoint> randomTestData = new List<ModTestDataPoint>();
            for (int i = 0; i < count; i++)
            {
                randomTestData.Add(GenerateValidRandomizedTestDataPoint());
            }

            return randomTestData;
        }
        #endregion
    }
}
