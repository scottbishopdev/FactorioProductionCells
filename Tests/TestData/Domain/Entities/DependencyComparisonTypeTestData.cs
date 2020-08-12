using System;
using System.Collections.Generic;
using System.Linq;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.Enums;
using FactorioProductionCells.TestData.Domain.Enums;

namespace FactorioProductionCells.TestData.Domain.Entities
{
    public static class DependencyComparisonTypeTestData
    {
        #region Static Test Data Collections
        private static List<DependencyComparisonTypeTestDataPoint> StaticTestData =>
            Enum.GetValues(typeof(DependencyComparisonTypeId))
                .Cast<DependencyComparisonTypeId>()
                .Select(dti => CreateDependencyComparisonTypeTestDataPointFromEnum(dti)).ToList();

        public static IEnumerable<object[]> ValidStaticStringsWithEnumValue() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ExpectedString, dataPoint.EnumValue };

        public static IEnumerable<object[]> ValidStaticStringsWithEnumName() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ExpectedString, dataPoint.EnumName };

        public static IEnumerable<object[]> ValidStaticDependencyComparisonTypeIdsWithEnumValues() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.EnumValue, dataPoint.EnumValue };

        public static IEnumerable<object[]> ValidStaticDependencyComparisonTypeIdsWithNames() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.EnumValue, dataPoint.EnumName };
        
        public static IEnumerable<object[]> ValidStaticDependencyComparisonTypeIntIdsWithIds() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.EnumInt, dataPoint.EnumValue };

        public static IEnumerable<object[]> ValidStaticDependencyComparisonTypeIntIdsWithNames() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.EnumInt, dataPoint.EnumName };

        public static IEnumerable<object[]> ValidStaticDependencyComparisonTypesWithId() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.Id };

        public static IEnumerable<object[]> ValidStaticDependencyComparisonTypesWithName() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.Name };

        public static IEnumerable<object[]> ValidStaticDependencyComparisonTypesFromForWithStrings() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.ExpectedString };

        public static IEnumerable<object[]> ValidStaticEqualDependencyComparisonTypePairs() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, new DependencyComparisonType(dataPoint.ObjectFromFor) };

        public static IEnumerable<object[]> ValidStaticNonEqualDependencyComparisonTypePairs()
        {
            for (int i = 0; i < StaticTestData.Count; i++)
            {
                var secondDataPoint = i != StaticTestData.Count - 1 ? StaticTestData[i+1] : StaticTestData[0];
                yield return new object[] { StaticTestData[i].ObjectFromFor, secondDataPoint.ObjectFromFor };
            }
        }
        #endregion
        
        #region Helper Methods
        private struct DependencyComparisonTypeTestDataPoint
        {
            public String ExpectedString;
            public DependencyComparisonTypeId EnumValue;
            public Int32 EnumInt;
            public String EnumName;
            public DependencyComparisonTypeId Id;
            public String Name;
            public DependencyComparisonType ObjectFromFor;
        }

        private static DependencyComparisonTypeTestDataPoint CreateDependencyComparisonTypeTestDataPointFromEnum(DependencyComparisonTypeId enumId)
        {
            DependencyComparisonType fromFor = new DependencyComparisonType(enumId);

            return new DependencyComparisonTypeTestDataPoint
            {
                ExpectedString = fromFor.ToString(),
                EnumValue = enumId,
                EnumInt = (Int32)enumId,
                EnumName = Enum.GetName(typeof(DependencyComparisonTypeId), enumId),
                Id = fromFor.Id,
                Name = fromFor.Name,
                ObjectFromFor = fromFor
            };
        }

        public static String GenerateValidRandomizedDependencyComparisonTypeString()
        {
            return GenerateValidRandomizedDependencyComparisonType().ToString();
        }

        public static DependencyComparisonType GenerateValidRandomizedDependencyComparisonType()
        {
            return new DependencyComparisonType(DependencyComparisonTypeIdTestData.GenerateRandomizedDependencyComparisonTypeId());
        }
        #endregion
    }
}
