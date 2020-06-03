using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Infrastructure.Persistence.Configurations
{
    public class DependencyConfiguration : AuditableEntityConfiguration<Dependency>
    {
        public override void Configure(EntityTypeBuilder<Dependency> builder)
        {
            // Primary Key
            builder.HasKey(d => new {d.ReleaseId, d.DependentModId});
            // Columns
            builder.Property(d => d.ReleaseId).IsRequired();
            builder.Property(d => d.DependentModId).IsRequired();
            builder.Property(d => d.DependencyTypeId).IsRequired();
            builder.Property(d => d.DependencyComparisonTypeId).IsRequired();
            // Value Objects
            builder.OwnsOne(d => d.DependentModVersion);
            // Indexes
            builder.HasOne(d => d.Release).WithMany(r => r.Dependencies);
            builder.HasOne(d => d.DependentMod);
            builder.HasOne(d => d.DependencyType);
            builder.HasOne(d => d.DependencyComparisonType);

            base.Configure(builder);
        }
    }
}
