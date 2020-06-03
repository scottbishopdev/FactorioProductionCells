using System;
using FactorioProductionCells.Domain.Enums;
using FactorioProductionCells.Domain.Validators;

namespace FactorioProductionCells.Domain.Entities
{
    public class DependencyType
    {
        private DependencyType() {}

        public DependencyType(int Id)
        {
            EnumValidator.ValidateIdInEnumRange<DependencyTypeId>(Id, nameof(Id));

            this.Id = (DependencyTypeId)Id;
            this.Name = Enum.GetName(this.GetType(), Id);
        }

        public DependencyType(DependencyTypeId Id) : this((int)Id) {}

        public const Int32 NameLength = 20;

        public DependencyTypeId Id { get; private set; }
        public String Name { get; private set; }
    }
}
