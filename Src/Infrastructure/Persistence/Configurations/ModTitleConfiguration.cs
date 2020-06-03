using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Infrastructure.Persistence.Configurations
{
    public class ModTitleConfiguration : AuditableEntityConfiguration<ModTitle>
    {
        public override void Configure(EntityTypeBuilder<ModTitle> builder)
        {
            // Primary Key
            builder.HasKey(mt => new {mt.ModId, mt.LanguageId});
            // Columns
            builder.Property(mt => mt.ModId).IsRequired();
            builder.Property(mt => mt.LanguageId).IsRequired();
            builder.Property(mt => mt.Title).HasMaxLength(ModTitle.TitleLength).IsRequired();
            // Ignored Columns
            builder.Ignore(mt => mt.TitleLength);
            // Indexes
            builder.HasOne(mt => mt.Mod).WithMany(m => m.Titles);
            builder.HasOne(mt => mt.Language);

            base.Configure(builder);
        }
    }
}
