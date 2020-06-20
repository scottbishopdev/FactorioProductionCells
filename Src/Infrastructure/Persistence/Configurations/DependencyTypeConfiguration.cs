using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.Enums;

namespace FactorioProductionCells.Infrastructure.Persistence.Configurations
{
    public class DependencyTypeConfiguration : IEntityTypeConfiguration<DependencyType>
    {
        public void Configure(EntityTypeBuilder<DependencyType> builder)
        {
            // Primary Key
            builder.HasKey(dt => dt.Id);

            // Columns
            builder.Property(dt => dt.Id).HasConversion<int>().IsRequired();
            builder.Property(dt => dt.Name).HasMaxLength(DependencyType.NameLength).IsRequired();

            // Table Data
            builder.HasData(Enum.GetValues(typeof(DependencyTypeId))
                .Cast<DependencyTypeId>()
                .Select(dti => new DependencyType(dti))
            );
        }
    }
}
