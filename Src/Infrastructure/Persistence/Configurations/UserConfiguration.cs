using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FactorioProductionCells.Infrastructure.Identity;
namespace FactorioProductionCells.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary Key
            builder.HasKey(au => au.Id);

            // Columns
            builder.Property(u => u.PreferredLanguageId).IsRequired();

            // Indexes
            builder.HasOne(mt => mt.PreferredLanguage).WithMany();
        }
    }
}
