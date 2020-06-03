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
            builder.Property(l => l.LanguageCode).HasMaxLength(Language.LanguageCodeLength).IsRequired();
            builder.Property(l => l.IsDefault).IsRequired();
            // Ignored Columns
            builder.Ignore(l => l.EnglishNameLength);
            builder.Ignore(l => l.LanguageCodeLength);
            // Indexes
            builder.HasMany(l => l.ModTitles).WithOne(mt => mt.Language);
            // Constraints
            // This *should* ensure that we only have one row where IsDefault is set to true.
            builder.HasIndex(l => new {l.Id, l.IsDefault}).IsUnique().HasFilter("[IsDefault] = 1");

            // TODO: Ensure that we're loading seed data properly.
            builder.HasData(new Language("English", "en-US", true));
        }
    }
}
