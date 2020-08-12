using System;
using FactorioProductionCells.Domain.Enums;

namespace FactorioProductionCells.TestData.Domain.Enums
{
    public static class DependencyTypeIdTestData
    {
        #region Helper Methods
        public static DependencyTypeId GenerateRandomizedDependencyTypeId()
        {
            var random = new Random();
            return (DependencyTypeId) random.Next(Enum.GetValues(typeof(DependencyTypeId)).Length);
        }
        #endregion
    }
}
