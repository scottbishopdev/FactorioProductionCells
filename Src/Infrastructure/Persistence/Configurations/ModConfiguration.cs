using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Infrastructure.Persistence.Configurations
{
    public class ModConfiguration : AuditableEntityConfiguration<Mod>
    {
        public override void Configure(EntityTypeBuilder<Mod> builder)
        {
            // Primary Key
            builder.HasKey(m => m.Id);

            // Columns
            builder.Property(m => m.Id).HasDefaultValueSql("uuid_generate_v4()").ValueGeneratedOnAdd().IsRequired();
            builder.Property(m => m.Name).HasMaxLength(Mod.NameLength).IsRequired();

            // Indexes
            builder.HasMany(m => m.Releases).WithOne(r => r.Mod);
            builder.HasMany(m => m.Titles).WithOne(mt => mt.Mod);
            builder.HasIndex(m => m.Name).IsUnique();

            base.Configure(builder);
        }
    }
}
