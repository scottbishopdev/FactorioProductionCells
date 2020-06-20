using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Infrastructure.Identity;

namespace FactorioProductionCells.Infrastructure.Persistence.Configurations
{
    public abstract class AuditableEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : AuditableEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            // Columns
            builder.Property(ae => ae.AddedBy).IsRequired();
            builder.Property(ae => ae.AddedDate).IsRequired();
            builder.Property(ae => ae.LastModifiedBy);
            builder.Property(ae => ae.LastModified);

            // Indexes
            builder.HasOne<User>().WithMany().HasForeignKey(ae => ae.AddedBy);
            builder.HasOne<User>().WithMany().HasForeignKey(ae => ae.LastModifiedBy);
        }
    }
}
