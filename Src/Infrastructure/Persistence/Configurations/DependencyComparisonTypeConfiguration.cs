using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.Enums;

namespace FactorioProductionCells.Infrastructure.Persistence.Configurations
{
    public class DependencyComparisonTypeConfiguration : IEntityTypeConfiguration<DependencyComparisonType>
    {
        public void Configure(EntityTypeBuilder<DependencyComparisonType> builder)
        {
            // Primary Key
            builder.HasKey(dt => dt.Id);
            // Columns
            builder.Property(dt => dt.Id).HasConversion<int>().IsRequired();
            builder.Property(dt => dt.Name).HasMaxLength(DependencyComparisonType.NameLength).IsRequired();
            // Ignored Columns
            // TODO: Determine if we need to ignore static properties. We can't reference them like this, but also, EF might decide it wants to store it.
            //builder.Ignore(dt => dt.NameLength);
            // Indexes
            // TODO: I don't think we need to do anything here to make indexes. That should be handled by the .HasOne() references in DependencyConfiguration.cs, right?
            // Table Data
            builder.HasData(Enum.GetValues(typeof(DependencyComparisonTypeId))
                .Cast<DependencyComparisonTypeId>()
                .Select(dcti => new DependencyComparisonType(dcti))
            );
        }
    }
}
