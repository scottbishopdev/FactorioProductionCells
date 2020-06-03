using System;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Enums;
using FactorioProductionCells.Domain.Validators;
using FactorioProductionCells.Domain.ValueObjects;

namespace FactorioProductionCells.Domain.Entities
{
    public class Dependency : AuditableEntity
    {
        private Dependency() {}

        public Dependency(Guid ReleaseId, Guid DependentModId, int DependencyTypeId, int DependencyComparisonTypeId, ModVersion DependentModVersion)
        {
            ObjectValidator.ValidateRequiredObject(ReleaseId, nameof(ReleaseId));
            ObjectValidator.ValidateRequiredObject(DependentModId, nameof(DependentModId));
            ObjectValidator.ValidateRequiredObject(DependencyTypeId, nameof(DependencyTypeId));
            ObjectValidator.ValidateRequiredObject(DependencyComparisonTypeId, nameof(DependencyComparisonTypeId));
            ObjectValidator.ValidateRequiredObject(DependentModVersion, nameof(DependentModVersion));

            this.ReleaseId = ReleaseId;
            this.DependentModId = DependentModId;
            this.DependencyTypeId = DependencyTypeId;
            this.DependencyComparisonTypeId = DependencyComparisonTypeId;
            this.DependentModVersion = DependentModVersion;
        }

        public Dependency(Release Release, Mod DependentMod, DependencyType DependencyType, DependencyComparisonType DependencyComparisonType, ModVersion DependentModVersion) :
            this(Release.Id, DependentMod.Id, (int)DependencyType.Id, (int)DependencyComparisonType.Id, DependentModVersion) {}

        public Guid ReleaseId { get; private set; }
        public Guid DependentModId { get; private set; }
        public int DependencyTypeId { get; private set; }
        public int DependencyComparisonTypeId { get; private set; }
        public ModVersion DependentModVersion { get; private set; }

        // Navigation properties
        public Release Release { get; private set; }
        public Mod DependentMod { get; private set; }
        public DependencyType DependencyType { get; private set; }
        public DependencyComparisonType DependencyComparisonType { get; private set; }
    }
}
