using System;
using FactorioProductionCells.Domain.Enums;
using FactorioProductionCells.Domain.Validators;

namespace FactorioProductionCells.Domain.Entities
{
    public class DependencyComparisonType
    {
        private DependencyComparisonType() {}

        public DependencyComparisonType(int Id)
        {
            EnumValidator.ValidateIdInEnumRange<DependencyComparisonTypeId>(Id, nameof(Id));

            this.Id = (DependencyComparisonTypeId)Id;
            this.Name = Enum.GetName(this.GetType(), Id);
        }

        public DependencyComparisonType(DependencyComparisonTypeId Id) : this((int)Id) {}

        public const Int32 NameLength = 25;

        public DependencyComparisonTypeId Id { get; private set; }
        public String Name { get; private set; }
    }
}
