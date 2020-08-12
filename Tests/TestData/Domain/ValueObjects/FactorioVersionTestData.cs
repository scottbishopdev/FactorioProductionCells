using System;
using System.Collections.Generic;
using System.Linq;
using FactorioProductionCells.Domain.ValueObjects;
using FactorioProductionCells.TestData.Common;

namespace FactorioProductionCells.TestData.Domain.ValueObjects
{
    public static class FactorioVersionTestData
    {
        #region Static Data
        public static FactorioVersionTestDataPoint ZeroPointThirteenTestDataPoint = CreateTestDataPointFromProperties(0, 13);
        public static FactorioVersion ZeroPointThirteen = ZeroPointThirteenTestDataPoint.ObjectFromFor;

        public static FactorioVersionTestDataPoint ZeroPointFourteenTestDataPoint = CreateTestDataPointFromProperties(0, 14);
        public static FactorioVersion ZeroPointFourteen = ZeroPointFourteenTestDataPoint.ObjectFromFor;
        
        public static FactorioVersionTestDataPoint ZeroPointFifteenTestDataPoint = CreateTestDataPointFromProperties(0, 15);
        public static FactorioVersion ZeroPointFifteen = ZeroPointFifteenTestDataPoint.ObjectFromFor;
        
        public static FactorioVersionTestDataPoint ZeroPointSixteenTestDataPoint = CreateTestDataPointFromProperties(0, 16);
        public static FactorioVersion ZeroPointSixteen = ZeroPointSixteenTestDataPoint.ObjectFromFor;

        public static FactorioVersionTestDataPoint ZeroPointSeventeenTestDataPoint = CreateTestDataPointFromProperties(0, 17);
        public static FactorioVersion ZeroPointSeventeen = ZeroPointSeventeenTestDataPoint.ObjectFromFor;

        public static FactorioVersionTestDataPoint ZeroPointEighteenTestDataPoint = CreateTestDataPointFromProperties(0, 18);
        public static FactorioVersion ZeroPointEighteen = ZeroPointEighteenTestDataPoint.ObjectFromFor;

        public static FactorioVersionTestDataPoint OnePointZeroTestDataPoint = CreateTestDataPointFromProperties(1, 0);
        public static FactorioVersion OnePointZero = OnePointZeroTestDataPoint.ObjectFromFor; // Just in case they ever get there. :P
        #endregion

        #region Static Test Data Collections
        private static List<FactorioVersionTestDataPoint> StaticTestData = new List<FactorioVersionTestDataPoint>()
        {
            ZeroPointThirteenTestDataPoint,
            ZeroPointFourteenTestDataPoint,
            ZeroPointFifteenTestDataPoint,
            ZeroPointSixteenTestDataPoint,
            ZeroPointSeventeenTestDataPoint,
            ZeroPointEighteenTestDataPoint,
            OnePointZeroTestDataPoint
        };

        public static IEnumerable<object[]> ValidStaticFactorioVersionsWithMajor() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.MajorVersion };

        public static IEnumerable<object[]> ValidStaticFactorioVersionsWithMinor() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.MinorVersion };

        public static IEnumerable<object[]> ValidStaticStringsWithMajorInt() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.CreationString, dataPoint.MajorVersion };

        public static IEnumerable<object[]> ValidStaticStringsWithMinorInt() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.CreationString, dataPoint.MinorVersion };

        public static IEnumerable<object[]> ValidStaticEqualFactorioVersionPairs() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, new FactorioVersion(dataPoint.ObjectFromFor) };

        public static IEnumerable<object[]> ValidStaticNonEqualFactorioVersionPairs()
        {
            List<object[]> returnValues = new List<object[]>();

            for (int i = 0; i < StaticTestData.Count; i++)
            {
                var secondDataPoint = i != StaticTestData.Count - 1 ? StaticTestData[i+1] : StaticTestData[0];
                yield return new object[] { StaticTestData[i].ObjectFromFor, secondDataPoint.ObjectFromFor };
            }
        }

        public static IEnumerable<object[]> ValidStaticFactorioVersionsFromForWithStrings() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.CreationString };

        public static IEnumerable<object[]> ValidStaticFactorioVersionsFromForWithInts() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.MajorVersion, dataPoint.MinorVersion };

        public static IEnumerable<object[]> Comparison_RightLowerData() =>
            from leftMajor in CommonTestData.PositiveIntegerValues
            from leftMinor in CommonTestData.PositiveIntegerValues
            from rightMajor in CommonTestData.PositiveIntegerValues
            from rightMinor in CommonTestData.PositiveIntegerValues
            where leftMajor > rightMajor || (leftMajor == rightMajor && leftMinor > rightMinor)
            select new object[] { $"{leftMajor.ToString()}.{leftMinor.ToString()}", $"{rightMajor.ToString()}.{rightMinor.ToString()}" };

        public static IEnumerable<object[]> Comparison_EqualData() =>
            from leftMajor in CommonTestData.PositiveIntegerValues
            from leftMinor in CommonTestData.PositiveIntegerValues
            from rightMajor in CommonTestData.PositiveIntegerValues
            from rightMinor in CommonTestData.PositiveIntegerValues
            where leftMajor == rightMajor && leftMinor == rightMinor
            select new object[] { $"{leftMajor.ToString()}.{leftMinor.ToString()}", $"{rightMajor.ToString()}.{rightMinor.ToString()}" };

        public static IEnumerable<object[]> Comparison_LeftLowerData() =>
            from leftMajor in CommonTestData.PositiveIntegerValues
            from leftMinor in CommonTestData.PositiveIntegerValues
            from rightMajor in CommonTestData.PositiveIntegerValues
            from rightMinor in CommonTestData.PositiveIntegerValues
            where leftMajor < rightMajor || (leftMajor == rightMajor && leftMinor < rightMinor)
            select new object[] { $"{leftMajor.ToString()}.{leftMinor.ToString()}", $"{rightMajor.ToString()}.{rightMinor.ToString()}" };
        #endregion

        #region Random Test Data Collections
        private static List<FactorioVersionTestDataPoint> RandomTestData = GetRandomizedTestData(10);

        public static IEnumerable<object[]> ValidRandomFactorioVersionsWithMajor() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.MajorVersion };

        public static IEnumerable<object[]> ValidRandomFactorioVersionsWithMinor() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.MinorVersion };

        public static IEnumerable<object[]> ValidRandomStringsWithMajorInt() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.CreationString, dataPoint.MajorVersion };

        public static IEnumerable<object[]> ValidRandomStringsWithMinorInt() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.CreationString, dataPoint.MinorVersion };

        public static IEnumerable<object[]> ValidRandomEqualFactorioVersionPairs() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, new FactorioVersion(dataPoint.ObjectFromFor) };

        public static IEnumerable<object[]> ValidRandomFactorioVersionsFromForWithStrings() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.CreationString };

        public static IEnumerable<object[]> ValidRandomFactorioVersionsFromForWithInts() =>
            from dataPoint in RandomTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.MajorVersion, dataPoint.MinorVersion };
        #endregion

        #region Helper Methods and Struct
        public struct FactorioVersionTestDataPoint
        {
            public String CreationString;
            public Int32 MajorVersion;
            public Int32 MinorVersion;
            public FactorioVersion ObjectFromFor;
        }

        public static String GenerateValidRandomizedFactorioVersionString()
        {
            var random = new Random();
            return $"{random.Next(0, Int32.MaxValue)}.{random.Next(0, Int32.MaxValue)}";
        }
        
        public static FactorioVersion GenerateValidRandomizedFactorioVersion()
        {
            return FactorioVersion.For(GenerateValidRandomizedFactorioVersionString());
        }

        private static FactorioVersionTestDataPoint CreateTestDataPointFromProperties(Int32 Major, Int32 Minor)
        {
            String creationString = $"{Major.ToString()}.{Minor.ToString()}";
            
            return new FactorioVersionTestDataPoint
            {
                CreationString = creationString,
                MajorVersion = Major,
                MinorVersion = Minor,
                ObjectFromFor = FactorioVersion.For(creationString)
            };
        }

        private static List<FactorioVersionTestDataPoint> GetRandomizedTestData(Int32 count = 10)
        {
            var random = new Random();
            List<FactorioVersionTestDataPoint> randomTestData = new List<FactorioVersionTestDataPoint>();
            for (int i = 0; i < count; i++)
            {
                randomTestData.Add(CreateTestDataPointFromProperties(random.Next(0, Int32.MaxValue), random.Next(0, Int32.MaxValue)));
            }

            return randomTestData;
        }
        #endregion
    }
}
