using System;
using System.Collections.Generic;
using System.Linq;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.ValueObjects;
using FactorioProductionCells.TestData.Domain.ValueObjects;
using FactorioProductionCells.TestData.Common;

namespace FactorioProductionCells.TestData.Domain.Entities
{
    public static class ReleaseTestData
    {
        #region Static Data
        // Test Mod
        public static DateTime TestModAlphaReleaseDate = DateTime.Parse("6/1/2020 3:24:52 AM");
        public const String TestModAlphaReleaseSha1String = "b53320182d7e53b81b1009d8353492614f9a5cac";
        public static ReleaseTestDataPoint TestModAlphaReleaseTestDataPoint = CreateTestDataPointFromProperties(
            ReleasedAt: TestModAlphaReleaseDate,
            Sha1String: TestModAlphaReleaseSha1String,
            ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
            ReleaseFileName: ReleaseFileNameTestData.TestModAlphaReleaseFileName,
            ModVersion: ModVersionTestData.TestModAlphaReleaseVersion,
            FactorioVersion: FactorioVersionTestData.ZeroPointSeventeen,
            Dependencies: DependencyTestData.TestModAlphaReleaseDependencies);
        public static Release TestModAlphaRelease = TestModAlphaReleaseTestDataPoint.ObjectFromConstructor;
        
        public static DateTime TestModBetaReleaseDate = DateTime.Parse("7/3/2020 6:12:03 PM");
        public const String TestModBetaReleaseSha1String = "4f6cf41a4a419c601580f967603f0d3977dab0b7";
        public static ReleaseTestDataPoint TestModBetaReleaseTestDataPoint = CreateTestDataPointFromProperties(
            ReleasedAt: TestModBetaReleaseDate,
            Sha1String: TestModBetaReleaseSha1String,
            ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
            ReleaseFileName: ReleaseFileNameTestData.TestModBetaReleaseFileName,
            ModVersion: ModVersionTestData.TestModBetaReleaseVersion,
            FactorioVersion: FactorioVersionTestData.ZeroPointEighteen,
            Dependencies: DependencyTestData.TestModBetaReleaseDependencies);
        public static Release TestModBetaRelease = TestModBetaReleaseTestDataPoint.ObjectFromConstructor;

        public static DateTime TestModGammaReleaseDate = DateTime.Parse("11/25/2019 6:00:00 PM");
        public const String TestModGammaReleaseSha1String = "3ca7285d4afaf29e85e838bf85fc0844d970ca6a";
        public static ReleaseTestDataPoint TestModGammaReleaseTestDataPoint = CreateTestDataPointFromProperties(
            ReleasedAt: TestModGammaReleaseDate,
            Sha1String: TestModGammaReleaseSha1String,
            ReleaseDownloadUrl: ReleaseDownloadUrlTestData.TestModDownloadUrl,
            ReleaseFileName: ReleaseFileNameTestData.TestModGammaReleaseFileName,
            ModVersion: ModVersionTestData.TestModGammaReleaseVersion,
            FactorioVersion: FactorioVersionTestData.ZeroPointEighteen,
            Dependencies: DependencyTestData.TestModGammaReleaseDependencies);
        public static Release TestModGammaRelease = TestModGammaReleaseTestDataPoint.ObjectFromConstructor;

        public static List<Release> TestModReleases = new List<Release>()
        {
            TestModAlphaRelease,
            TestModBetaRelease
        };

        // Bob's Functions Library Mod
        public static DateTime BobsFunctionsLibraryFirstReleaseDate = DateTime.Parse("2016-06-27T23:24:50.634000Z");
        public const String BobsFunctionsLibraryFirstReleaseSha1String = "ac301d43a1a30d9986ce43393e4489f601a90cec";
        public static ReleaseTestDataPoint BobsFunctionsLibraryFirstReleaseTestDataPoint = CreateTestDataPointFromProperties(
            ReleasedAt: BobsFunctionsLibraryFirstReleaseDate,
            Sha1String: BobsFunctionsLibraryFirstReleaseSha1String,
            ReleaseDownloadUrl: ReleaseDownloadUrlTestData.BobsFunctionsLibraryFirstReleaseDownloadUrl,
            ReleaseFileName: ReleaseFileNameTestData.BobsFunctionsLibraryFirstReleaseFileName,
            ModVersion: ModVersionTestData.BobsFunctionsLibraryFirstReleaseVersion,
            FactorioVersion: FactorioVersionTestData.ZeroPointThirteen,
            Dependencies: DependencyTestData.BobsFunctionsLibraryFirstReleaseDependencies);
        public static Release BobsFunctionsLibraryFirstRelease = BobsFunctionsLibraryFirstReleaseTestDataPoint.ObjectFromConstructor;

        public static DateTime BobsFunctionsLibraryMiddleReleaseDate = DateTime.Parse("2017-12-22T21:43:58.593000Z");
        public const String BobsFunctionsLibraryMiddleReleaseSha1String = "5ea00fdab5f1863098b51b4640f31995a742ffcd";
        public static ReleaseTestDataPoint BobsFunctionsLibraryMiddleReleaseTestDataPoint = CreateTestDataPointFromProperties(
            ReleasedAt: BobsFunctionsLibraryMiddleReleaseDate,
            Sha1String: BobsFunctionsLibraryMiddleReleaseSha1String,
            ReleaseDownloadUrl: ReleaseDownloadUrlTestData.BobsFunctionsLibraryMiddleReleaseDownloadUrl,
            ReleaseFileName: ReleaseFileNameTestData.BobsFunctionsLibraryMiddleReleaseFileName,
            ModVersion: ModVersionTestData.BobsFunctionsLibraryMiddleReleaseVersion,
            FactorioVersion: FactorioVersionTestData.ZeroPointSixteen,
            Dependencies: DependencyTestData.BobsFunctionsLibraryMiddleReleaseDependencies);
        public static Release BobsFunctionsLibraryMiddleRelease = BobsFunctionsLibraryMiddleReleaseTestDataPoint.ObjectFromConstructor;

        public static DateTime BobsFunctionsLibraryLatestReleaseDate = DateTime.Parse("2020-04-20T14:17:26.290000Z");
        public const String BobsFunctionsLibraryLatestReleaseSha1String = "4e804310d21fc04a9c11bef2c9b049770a2cffd3";
        public static ReleaseTestDataPoint BobsFunctionsLibraryLatestReleaseTestDataPoint = CreateTestDataPointFromProperties(
            ReleasedAt: BobsFunctionsLibraryLatestReleaseDate,
            Sha1String: BobsFunctionsLibraryLatestReleaseSha1String,
            ReleaseDownloadUrl: ReleaseDownloadUrlTestData.BobsFunctionsLibraryLatestReleaseDownloadUrl,
            ReleaseFileName: ReleaseFileNameTestData.BobsFunctionsLibraryLatestReleaseFileName,
            ModVersion: ModVersionTestData.BobsFunctionsLibraryLatestReleaseVersion,
            FactorioVersion: FactorioVersionTestData.ZeroPointEighteen,
            Dependencies: DependencyTestData.BobsFunctionsLibraryLatestReleaseDependencies);
        public static Release BobsFunctionsLibraryLatestRelease = BobsFunctionsLibraryLatestReleaseTestDataPoint.ObjectFromConstructor;
        
        public static List<Release> BobsFunctionsLibraryModReleases = new List<Release>
        {
            BobsFunctionsLibraryFirstRelease,
            BobsFunctionsLibraryMiddleRelease,
            BobsFunctionsLibraryLatestRelease
        };

        // Bob's Logistics Mod
        public static DateTime BobsLogisticsFirstReleaseDate = DateTime.Parse("2016-06-29T19:16:24.857000Z");
        public const String BobsLogisticsFirstReleaseSha1String = "7a8d879305354f6bb5cb158dd5976519027af011";
        public static ReleaseTestDataPoint BobsLogisticsFirstReleaseTestDataPoint = CreateTestDataPointFromProperties(
            ReleasedAt: BobsLogisticsFirstReleaseDate,
            Sha1String: BobsLogisticsFirstReleaseSha1String,
            ReleaseDownloadUrl: ReleaseDownloadUrlTestData.BobsLogisticsFirstReleaseDownloadUrl,
            ReleaseFileName: ReleaseFileNameTestData.BobsLogisticsFirstReleaseFileName,
            ModVersion: ModVersionTestData.BobsLogisticsFirstReleaseVersion,
            FactorioVersion: FactorioVersionTestData.ZeroPointThirteen,
            Dependencies: DependencyTestData.BobsLogisticsFirstReleaseDependencies);
        public static Release BobsLogisticsFirstRelease = BobsLogisticsFirstReleaseTestDataPoint.ObjectFromConstructor;

        public static DateTime BobsLogisticsMiddleReleaseDate = DateTime.Parse("2017-05-13T23:56:12.025000Z");
        public const String BobsLogisticsMiddleReleaseSha1String = "107a837e790f905c3c9b4e4c5fcdfc4f2f7a4a04";
        public static ReleaseTestDataPoint BobsLogisticsMiddleReleaseTestDataPoint = CreateTestDataPointFromProperties(
            ReleasedAt: BobsLogisticsMiddleReleaseDate,
            Sha1String: BobsLogisticsMiddleReleaseSha1String,
            ReleaseDownloadUrl: ReleaseDownloadUrlTestData.BobsLogisticsMiddleReleaseDownloadUrl,
            ReleaseFileName: ReleaseFileNameTestData.BobsLogisticsMiddleReleaseFileName,
            ModVersion: ModVersionTestData.BobsLogisticsMiddleReleaseVersion,
            FactorioVersion: FactorioVersionTestData.ZeroPointFifteen,
            Dependencies: DependencyTestData.BobsLogisticsMiddleReleaseDependencies);
        public static Release BobsLogisticsMiddleRelease = BobsLogisticsMiddleReleaseTestDataPoint.ObjectFromConstructor;

        public static DateTime BobsLogisticsLatestReleaseDate = DateTime.Parse("2020-04-16T17:26:30.919000Z");
        public const String BobsLogisticsLatestReleaseSha1String = "2a01c763d97cb14884dbdacbd3c2d89e8a988d9b";
        public static ReleaseTestDataPoint BobsLogisticsLatestReleaseTestDataPoint = CreateTestDataPointFromProperties(
            ReleasedAt: BobsLogisticsLatestReleaseDate,
            Sha1String: BobsLogisticsLatestReleaseSha1String,
            ReleaseDownloadUrl: ReleaseDownloadUrlTestData.BobsLogisticsLatestReleaseDownloadUrl,
            ReleaseFileName: ReleaseFileNameTestData.BobsLogisticsLatestReleaseFileName,
            ModVersion: ModVersionTestData.BobsLogisticsLatestReleaseVersion,
            FactorioVersion: FactorioVersionTestData.ZeroPointEighteen,
            Dependencies: DependencyTestData.BobsLogisticsLatestReleaseDependencies);
        public static Release BobsLogisticsLatestRelease = BobsLogisticsLatestReleaseTestDataPoint.ObjectFromConstructor;

        public static List<Release> BobsLogisticsModReleases = new List<Release>
        {
            BobsLogisticsFirstRelease,
            BobsLogisticsMiddleRelease,
            BobsLogisticsLatestRelease
        };
        #endregion

        #region Static Test Data Collections
        private static List<ReleaseTestDataPoint> StaticTestData = new List<ReleaseTestDataPoint>()
        {
            TestModAlphaReleaseTestDataPoint,
            TestModBetaReleaseTestDataPoint,
            BobsFunctionsLibraryFirstReleaseTestDataPoint,
            BobsFunctionsLibraryMiddleReleaseTestDataPoint,
            BobsFunctionsLibraryLatestReleaseTestDataPoint,
            BobsLogisticsFirstReleaseTestDataPoint,
            BobsLogisticsMiddleReleaseTestDataPoint,
            BobsLogisticsLatestReleaseTestDataPoint,
        };

        public static IEnumerable<object[]> ValidStaticEqualReleasePairs() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromConstructor, new Release(dataPoint.ObjectFromConstructor) };

        public static IEnumerable<object[]> ValidStaticNonEqualReleasePairs()
        {
            for (int i = 0; i < StaticTestData.Count; i++)
            {
                var secondDataPoint = i != StaticTestData.Count - 1 ? StaticTestData[i+1] : StaticTestData[0];
                yield return new object[] { StaticTestData[i].ObjectFromConstructor, secondDataPoint.ObjectFromConstructor };
            }
        }

        public static IEnumerable<object[]> ValidStaticReleasesWithReleasedAt() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.ReleasedAt};

        public static IEnumerable<object[]> ValidStaticReleasesWithSha1() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.Sha1};

        public static IEnumerable<object[]> ValidStaticReleasesWithModVersion() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.ModVersion};

        public static IEnumerable<object[]> ValidStaticReleasesWithFactorioVersion() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.FactorioVersion};
        
        public static IEnumerable<object[]> ValidStaticReleasesWithReleaseDownloadUrl() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.ReleaseDownloadUrl};

        public static IEnumerable<object[]> ValidStaticReleasesWithReleaseFileName() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.ReleaseFileName};

        public static IEnumerable<object[]> ValidStaticReleasesWithDependencies() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.Dependencies};

        public static IEnumerable<object[]> ValidStaticReleasesCreationProperties() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ReleasedAt, dataPoint.Sha1, dataPoint.ReleaseDownloadUrl, dataPoint.ReleaseFileName, dataPoint.ModVersion, dataPoint.FactorioVersion, dataPoint.Dependencies };
        
        public static IEnumerable<object[]> Comparison_RightLowerData() =>
            from leftDateTime in CommonTestData.DateTimeValues
            from rightDateTime in CommonTestData.DateTimeValues
            where leftDateTime > rightDateTime && leftDateTime < DateTime.Now && rightDateTime < DateTime.Now
            select new object[] { leftDateTime, rightDateTime };

        public static IEnumerable<object[]> Comparison_EqualData() =>
            from leftDateTime in CommonTestData.DateTimeValues
            from rightDateTime in CommonTestData.DateTimeValues
            where leftDateTime == rightDateTime && leftDateTime < DateTime.Now && rightDateTime < DateTime.Now
            select new object[] { leftDateTime, rightDateTime };

        public static IEnumerable<object[]> Comparison_LeftLowerData() =>
            from leftDateTime in CommonTestData.DateTimeValues
            from rightDateTime in CommonTestData.DateTimeValues
            where leftDateTime < rightDateTime && leftDateTime < DateTime.Now && rightDateTime < DateTime.Now
            select new object[] { leftDateTime, rightDateTime };
        #endregion

        #region Random Test Data Collections
        private static List<ReleaseTestDataPoint> RandomTestData = GetRandomizedTestData(10);

        public static IEnumerable<object[]> ValidRandomEqualReleasePairs() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromConstructor, new Release(dataPoint.ObjectFromConstructor) };

        public static IEnumerable<object[]> ValidRandomReleasesWithReleasedAt() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.ReleasedAt};

        public static IEnumerable<object[]> ValidRandomReleasesWithSha1() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.Sha1};

        public static IEnumerable<object[]> ValidRandomReleasesWithModVersion() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.ModVersion};

        public static IEnumerable<object[]> ValidRandomReleasesWithFactorioVersion() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.FactorioVersion};
        
        public static IEnumerable<object[]> ValidRandomReleasesWithReleaseDownloadUrl() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.ReleaseDownloadUrl};

        public static IEnumerable<object[]> ValidRandomReleasesWithReleaseFileName() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.ReleaseFileName};

        public static IEnumerable<object[]> ValidRandomReleasesWithDependencies() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromConstructor, dataPoint.Dependencies};

        public static IEnumerable<object[]> ValidRandomReleasesCreationProperties() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ReleasedAt, dataPoint.Sha1, dataPoint.ReleaseDownloadUrl, dataPoint.ReleaseFileName, dataPoint.ModVersion, dataPoint.FactorioVersion, dataPoint.Dependencies };
        #endregion

        #region Helper Methods and Struct
        public struct ReleaseTestDataPoint
        {
            public DateTime ReleasedAt;
            public String Sha1;
            public ModVersion ModVersion;
            public FactorioVersion FactorioVersion;
            public ReleaseDownloadUrl ReleaseDownloadUrl;
            public ReleaseFileName ReleaseFileName;
            public List<Dependency> Dependencies;
            public Release ObjectFromConstructor;
        }

        private static ReleaseTestDataPoint CreateTestDataPointFromProperties(DateTime ReleasedAt, String Sha1String, ReleaseDownloadUrl ReleaseDownloadUrl, ReleaseFileName ReleaseFileName, ModVersion ModVersion, FactorioVersion FactorioVersion, List<Dependency> Dependencies)
        {
            return new ReleaseTestDataPoint
            {
                ReleasedAt = ReleasedAt,
                Sha1 = Sha1String,
                ReleaseDownloadUrl = ReleaseDownloadUrl,
                ReleaseFileName = ReleaseFileName,
                ModVersion = ModVersion,
                FactorioVersion = FactorioVersion,
                Dependencies = Dependencies,
                ObjectFromConstructor = new Release(ReleasedAt, Sha1String, ReleaseDownloadUrl, ReleaseFileName, ModVersion, FactorioVersion, Dependencies)
            };
        }

        public static DateTime GenerateValidRandomizedReleasedAt()
        {
            var random = new Random();
            TimeSpan range = DateTime.Today - DateTime.MinValue;
            TimeSpan randTimeSpan = new TimeSpan((Int64)(random.NextDouble() * range.Ticks)); 
            return DateTime.MinValue + randTimeSpan;
        }

        public static String GenerateValidRandomizedSha1String()
        {
            return TestDataHelpers.GetRandomCharacterStringFromSet(Release.ValidSha1Characters, Release.Sha1Length);
        }

        public static Release GenerateValidRandomizedRelease()
        {
            return GenerateValidRandomizedTestDataPoint().ObjectFromConstructor;
        }

        private static ReleaseTestDataPoint GenerateValidRandomizedTestDataPoint()
        {
            var random = new Random();
            List<Dependency> randomizedDependencies = new List<Dependency>();
            for (int i = 0; i < random.Next(1, 6); i++)
            {
                randomizedDependencies.Add(DependencyTestData.GenerateValidRandomizedDependency());
            }

            String releaseFileNameString = ReleaseFileNameTestData.GenerateValidRandomizedReleaseFileNameString();
            Int32 underscoreIndex = releaseFileNameString.LastIndexOf("_");
            String modVersionString = releaseFileNameString.Substring(underscoreIndex + 1, releaseFileNameString.LastIndexOf(".zip") - (underscoreIndex + 1) );

            return CreateTestDataPointFromProperties(
                ReleasedAt: ReleaseTestData.GenerateValidRandomizedReleasedAt(),
                Sha1String: ReleaseTestData.GenerateValidRandomizedSha1String(),
                ReleaseDownloadUrl: ReleaseDownloadUrlTestData.GenerateValidRandomizedReleaseDownloadUrl(),
                ReleaseFileName: ReleaseFileName.For(releaseFileNameString),
                ModVersion: ModVersion.For(modVersionString),
                FactorioVersion: FactorioVersionTestData.GenerateValidRandomizedFactorioVersion(),
                Dependencies: randomizedDependencies
            );
        }

        public static Release GenerateValidRandomizedReleaseWithModName(String modName)
        {
            return GenerateValidRandomizedTestDataPointWithModName(modName).ObjectFromConstructor;
        }

        private static ReleaseTestDataPoint GenerateValidRandomizedTestDataPointWithModName(String modName)
        {
            var random = new Random();
            List<Dependency> randomizedDependencies = new List<Dependency>();
            for (int i = 0; i < random.Next(1, 6); i++)
            {
                randomizedDependencies.Add(DependencyTestData.GenerateValidRandomizedDependency());
            }

            String releaseFileNameString = ReleaseFileNameTestData.GenerateValidRandomizedReleaseFileNameStringWithModName(modName);
            Int32 underscoreIndex = releaseFileNameString.LastIndexOf("_");
            String modVersionString = releaseFileNameString.Substring(underscoreIndex + 1, releaseFileNameString.LastIndexOf(".zip") - (underscoreIndex + 1) );

            return CreateTestDataPointFromProperties(
                ReleasedAt: ReleaseTestData.GenerateValidRandomizedReleasedAt(),
                Sha1String: ReleaseTestData.GenerateValidRandomizedSha1String(),
                ReleaseDownloadUrl: ReleaseDownloadUrl.For(ReleaseDownloadUrlTestData.GenerateValidRandomizedReleaseDownloadUrlStringWithModName(modName)),
                ReleaseFileName: ReleaseFileName.For(releaseFileNameString),
                ModVersion: ModVersion.For(modVersionString),
                FactorioVersion: FactorioVersionTestData.GenerateValidRandomizedFactorioVersion(),
                Dependencies: randomizedDependencies
            );
        }

        private static List<ReleaseTestDataPoint> GetRandomizedTestData(Int32 count = 10)
        {
            List<ReleaseTestDataPoint> randomTestData = new List<ReleaseTestDataPoint>();
            for (int i = 0; i < count; i++)
            {
                randomTestData.Add(GenerateValidRandomizedTestDataPoint());
            }

            return randomTestData;
        }
        #endregion
    }
}
