using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Infrastructure.Persistence.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            // Primary Key
            builder.HasKey(l => l.Id);

            // Columns
            builder.Property(l => l.Id).HasDefaultValueSql("uuid_generate_v4()").ValueGeneratedOnAdd().IsRequired();
            builder.Property(l => l.EnglishName).HasMaxLength(Language.EnglishNameLength).IsRequired();
            builder.Property(l => l.LanguageTag).HasMaxLength(Language.LanguageTagLength).IsRequired();
            builder.Property(l => l.IsDefault).IsRequired();
            builder.Ignore(l => l.Culture);

            // Constraints
            builder.HasIndex(l => l.IsDefault).IsUnique().HasFilter("\"IsDefault\" = true");
        }
    }
}
