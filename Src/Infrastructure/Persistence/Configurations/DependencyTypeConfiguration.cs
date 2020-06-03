using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.Enums;

namespace FactorioProductionCells.Infrastructure.Persistence.Configurations
{
    public class DependencyTypeConfiguration : IEntityTypeConfiguration<DependencyType>
    {
        public override void Configure(EntityTypeBuilder<DependencyType> builder)
        {
            // Primary Key
            builder.HasKey(dt => dt.Id);
            // Columns
            builder.Property(dt => dt.Id).IsRequired();
            builder.Property(dt => dt.Name).HasMaxLength(DependencyType.NameLength).IsRequired();
            // Ignored Columns
            builder.Ignore(dt => dt.NameLength);
            // Indexes
            // TODO: I don't think we need to do anything here to make indexes. That should be handled by the .HasOne() references in DependencyConfiguration.cs, right?
            // Table Data
            builder.HasData(Enum.GetValues(typeof(DependencyType))
                .Cast<DependencyTypeId>()
                .Select(dti => new DependencyTypeId()
                {
                    DependencyTypeId = dti,
                    Name = dti.ToString()
                })
            );
        }
    }
}
