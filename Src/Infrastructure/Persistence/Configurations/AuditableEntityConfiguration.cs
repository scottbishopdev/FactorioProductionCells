using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.Common;

namespace FactorioProductionCells.Infrastructure.Persistence.Configurations
{
    public abstract class AuditableEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : AuditableEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            // TODO: I'm not a fan of the fact that our EF code is responsible for setting these, rather than a default value or a database trigger.
            // Columns
            builder.Property(ae => ae.AddedBy).IsRequired();
            builder.Property(ae => ae.AddedDate).IsRequired();
            builder.Property(ae => ae.LastModifiedBy);
            builder.Property(ae => ae.LastModified);
            
            
            //builder.Property(ae => ae.AddedByUser).IsRequired();


            //builder.Property(ae => ae.LastModifiedByUser);
            // Indexes
            //builder.HasOne(ae => ae.AddedByUser).WithMany().IsRequired();
            //builder.HasOne(ae => ae.LastModifiedByUser).WithMany();
            //builder.HasOne(ae => ae.LastModifiedByUser);


            builder.HasOne(r => r.AddedByUser).WithMany();
            builder.HasOne(r => r.LastModifiedByUser).WithMany();
            /*
            // Navigation Properties
            public User AddedByUser { get; set; }
            public User LastModifiedByUser { get; set; }
            */
        }
    }
}
