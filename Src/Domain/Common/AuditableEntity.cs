using System;

namespace FactorioProductionCells.Domain.Common
{
    public abstract class AuditableEntity
    {
        public Guid AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
