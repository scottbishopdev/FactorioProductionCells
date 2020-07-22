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
            
            // Columns
            builder.Property(r => r.Id).HasDefaultValueSql("uuid_generate_v4()").ValueGeneratedOnAdd().IsRequired();
            builder.Property(r => r.ReleasedAt).IsRequired();
            builder.Property(r => r.Sha1).HasMaxLength(Release.Sha1Length).IsRequired();

            // Value Objects
            // TODO: So, fun fact: It's not (currently) possible to make the columns for the internal properties of a value object required using ef core.
            // This means that the database could store null values for these if something told it to, though that should be "impossible" via the service code
            // since we validate things to a gratuitous level. See these issues for more information: https://github.com/dotnet/efcore/issues/12100, https://github.com/dotnet/efcore/issues/13947
            builder.OwnsOne(
                navigationExpression: r => r.ModVersion,
                buildAction: mv => {
                    mv.Property(mv => mv.Major);
                    mv.Property(mv => mv.Minor);
                    mv.Property(mv => mv.Patch);
                });
            builder.OwnsOne(
                navigationExpression: r => r.FactorioVersion,
                buildAction: fv => {
                    fv.Property(fv => fv.Major);
                    fv.Property(fv => fv.Minor);
                });
            builder.OwnsOne(
                navigationExpression: r => r.ReleaseDownloadUrl,
                buildAction: du => {
                    du.Property(du => du.ModName).HasMaxLength(Mod.NameLength);
                    du.Property(du => du.ReleaseToken).HasMaxLength(ReleaseDownloadUrl.ReleaseTokenLength);
                });
            builder.OwnsOne(
                navigationExpression: r => r.ReleaseFileName,
                buildAction: fn => {
                    fn.Property(fn => fn.ModName).HasMaxLength(Mod.NameLength);
                    // VALUE OBJECT-CEPTION!
                    fn.OwnsOne(
                        navigationExpression: fn => fn.ModVersion,
                        buildAction: mv => {
                            mv.Property(mv => mv.Major);
                            mv.Property(mv => mv.Minor);
                            mv.Property(mv => mv.Patch);
                        });
                });

            // Indexes
            builder.HasOne(r => r.Mod).WithMany(m => m.Releases);

            base.Configure(builder);
        }
    }
}
