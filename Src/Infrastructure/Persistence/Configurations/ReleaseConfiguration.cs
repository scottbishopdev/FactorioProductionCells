using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.ValueObjects;

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
            builder.Property(r => r.Sha1).HasMaxLength(Release.Sha1Length).IsRequired();
            // Value Objects
            //builder.OwnsOne(r => r.Version);
            builder.OwnsOne(
                navigationExpression: r => r.Version,
                buildAction: mv => {
                    mv.Property(mv => mv.Major).IsRequired();
                    mv.Property(mv => mv.Minor).IsRequired();
                    mv.Property(mv => mv.Patch).IsRequired();
                }
            );
            //builder.OwnsOne(r => r.FactorioVersion);
            builder.OwnsOne(
                navigationExpression: r => r.FactorioVersion,
                buildAction: fv => {
                    fv.Property(fv => fv.Major).IsRequired();
                    fv.Property(fv => fv.Minor).IsRequired();
                }
            );
            //builder.OwnsOne(r => r.DownloadUrl);
            builder.OwnsOne(
                navigationExpression: r => r.DownloadUrl,
                buildAction: du => {
                    du.Property(du => du.ModName).HasMaxLength(Mod.NameLength).IsRequired();
                    du.Property(du => du.ReleaseToken).HasMaxLength(ReleaseDownloadUrl.ReleaseTokenLength).IsRequired();
                }
            );
            //builder.OwnsOne(r => r.ReleaseFileName);
            builder.OwnsOne(
                navigationExpression: r => r.ReleaseFileName,
                buildAction: fn => {
                    fn.Property(fn => fn.ModName).HasMaxLength(Mod.NameLength).IsRequired();
                    // VALUE OBJECT-CEPTION!
                    fn.OwnsOne(
                        navigationExpression: fn => fn.Version,
                        buildAction: mv => {
                            mv.Property(mv => mv.Major).IsRequired();
                            mv.Property(mv => mv.Minor).IsRequired();
                            mv.Property(mv => mv.Patch).IsRequired();
                        }
                    );
                }
            );

            // Ignored Columns
            // TODO: Determine if we need to ignore static properties. We can't reference them like this, but also, EF might decide it wants to store it.
            //builder.Ignore(r => r.Sha1Length);
            // Indexes
            builder.HasOne(r => r.Mod).WithMany(m => m.Releases);

            base.Configure(builder);
        }
    }
}
