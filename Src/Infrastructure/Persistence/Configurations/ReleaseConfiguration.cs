using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Infrastructure.Persistence.Configurations
{
    public class ReleaseConfiguration : AuditableEntityConfiguration<Release>
    {
        public override void Configure(EntityTypeBuilder<Release> builder)
        {
            // Primary Key
            builder.HasKey(r => r.Id);
            // Columns
            builder.Property(r => r.Id).HasDefaultValueSql("uuid_generate_v4()").ValueGeneratedOnAdd().IsRequired();
            builder.Property(r => r.ReleasedAt).IsRequired();
            builder.Property(r => r.DownloadUrl).HasMaxLength(Release.DownloadUrlLength).IsRequired();
            builder.Property(r => r.FileName).HasMaxLength(Release.FileNameLength).IsRequired();
            builder.Property(r => r.Sha1).HasMaxLength(Release.Sha1Length).IsRequired();
            // Value Objects
            builder.OwnsOne(r => r.Version);
            builder.OwnsOne(r => r.FactorioVersion);
            // Ignored Columns
            builder.Ignore(r => r.DownloadUrlLength);
            builder.Ignore(r => r.FileNameLength);
            builder.Ignore(r => r.Sha1Length);
            // Indexes
            builder.HasOne(r => r.Mod).WithMany(m => m.Releases);

            base.Configure(builder);
        }
    }
}
