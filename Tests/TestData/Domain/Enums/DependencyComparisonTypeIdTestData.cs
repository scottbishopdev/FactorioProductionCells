using System;
using FactorioProductionCells.Domain.Enums;

namespace FactorioProductionCells.TestData.Domain.Enums
{
    public static class DependencyComparisonTypeIdTestData
    {
        #region Helper Methods
        public static DependencyComparisonTypeId GenerateRandomizedDependencyComparisonTypeId()
        {
            var random = new Random();
            return (DependencyComparisonTypeId) random.Next(Enum.GetValues(typeof(DependencyComparisonTypeId)).Length);
        }
        #endregion
    }
}
