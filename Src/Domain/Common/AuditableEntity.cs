using System;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Domain.Common
{
    public abstract class AuditableEntity
    {
        public Guid AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }

        // Navigation Properties
        public User AddedByUser { get; set; }
        public User LastModifiedByUser { get; set; }
    }
}
