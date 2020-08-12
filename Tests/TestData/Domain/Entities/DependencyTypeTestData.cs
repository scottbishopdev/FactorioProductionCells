using System;
using System.Collections.Generic;
using System.Linq;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.Enums;
using FactorioProductionCells.TestData.Domain.Enums;

namespace FactorioProductionCells.TestData.Domain.Entities
{
    public static class DependencyTypeTestData
    {
        #region Static Test Data Collections
        private static List<DependencyTypeTestDataPoint> StaticTestData =>
            Enum.GetValues(typeof(DependencyTypeId))
                .Cast<DependencyTypeId>()
                .Select(dti => CreateDependencyTypeTestDataPointFromEnum(dti)).ToList();

        public static IEnumerable<object[]> ValidStaticStringsWithEnumValue() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ExpectedString, dataPoint.EnumValue };

        public static IEnumerable<object[]> ValidStaticStringsWithEnumName() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ExpectedString, dataPoint.EnumName };

        public static IEnumerable<object[]> ValidStaticDependencyTypeIdsWithEnumValues() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.EnumValue, dataPoint.EnumValue };

        public static IEnumerable<object[]> ValidStaticDependencyTypeIdsWithNames() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.EnumValue, dataPoint.EnumName };
        
        public static IEnumerable<object[]> ValidStaticDependencyTypeIntIdsWithIds() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.EnumInt, dataPoint.EnumValue };

        public static IEnumerable<object[]> ValidStaticDependencyTypeIntIdsWithNames() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.EnumInt, dataPoint.EnumName };

        public static IEnumerable<object[]> ValidStaticDependencyTypesWithId() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.Id };

        public static IEnumerable<object[]> ValidStaticDependencyTypesWithName() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.Name };

        public static IEnumerable<object[]> ValidStaticDependencyTypesFromForWithStrings() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, dataPoint.ExpectedString };

        public static IEnumerable<object[]> ValidStaticEqualDependencyTypePairs() =>
            from dataPoint in StaticTestData
            select new object[] { dataPoint.ObjectFromFor, new DependencyType(dataPoint.ObjectFromFor) };

        public static IEnumerable<object[]> ValidStaticNonEqualDependencyTypePairs()
        {
            for (int i = 0; i < StaticTestData.Count; i++)
            {
                var secondDataPoint = i != StaticTestData.Count - 1 ? StaticTestData[i+1] : StaticTestData[0];
                yield return new object[] { StaticTestData[i].ObjectFromFor, secondDataPoint.ObjectFromFor };
            }
        }
        #endregion

        #region Helper Methods and Struct
        private struct DependencyTypeTestDataPoint
        {
            public String ExpectedString;
            public DependencyTypeId EnumValue;
            public Int32 EnumInt;
            public String EnumName;
            public DependencyTypeId Id;
            public String Name;
            public DependencyType ObjectFromFor;
        }

        private static DependencyTypeTestDataPoint CreateDependencyTypeTestDataPointFromEnum(DependencyTypeId enumId)
        {
            DependencyType fromFor = new DependencyType(enumId);

            return new DependencyTypeTestDataPoint
            {
                ExpectedString = fromFor.ToString(),
                EnumValue = enumId,
                EnumInt = (Int32)enumId,
                EnumName = Enum.GetName(typeof(DependencyTypeId), enumId),
                Id = fromFor.Id,
                Name = fromFor.Name,
                ObjectFromFor = fromFor
            };
        }

        public static String GenerateValidRandomizedDependencyTypeString()
        {
            return GenerateValidRandomizedDependencyType().ToString();
        }

        public static DependencyType GenerateValidRandomizedDependencyType()
        {
            return new DependencyType(DependencyTypeIdTestData.GenerateRandomizedDependencyTypeId());
        }
        #endregion
    }
}
