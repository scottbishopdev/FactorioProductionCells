using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FactorioProductionCells.Domain.Entities;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FactorioProductionCells.Infrastructure.Persistence.Configurations
{
    public class DependencyConfiguration : AuditableEntityConfiguration<Dependency>
    {
        public override void Configure(EntityTypeBuilder<Dependency> builder)
        {
            // Primary Key
            builder.HasKey(d => new {d.ReleaseId, d.DependentModId});
            // Columns
            builder.Property(d => d.ReleaseId).IsRequired();
            builder.Property(d => d.DependentModId).IsRequired();
            builder.Property(d => d.DependencyTypeId).HasConversion<int>();
            // TODO: I need to figure out if there's a way I can convert the ModName into a ModId for storage.
            builder.Property(d => d.DependentModName).HasMaxLength(Mod.NameLength).IsRequired();
            builder.Property(d => d.DependencyComparisonTypeId).HasConversion<int>();

            // Value Objects
            // TODO: So, fun fact, it's not actually possible to make the columns for the internal properties of a value object required using ef core!
            // This means that the database could store null values for these if something told it to, though that should be impossible via the service code.
            // See these issues for more information: https://github.com/dotnet/efcore/issues/12100, https://github.com/dotnet/efcore/issues/13947
            builder.OwnsOne(
                navigationExpression: d => d.DependentModVersion,
                buildAction: mv => {
                    mv.Property(mv => mv.Major);
                    mv.Property(mv => mv.Minor);
                    mv.Property(mv => mv.Patch);
                });
            
            // Indexes
            builder.HasOne(d => d.Release).WithMany(r => r.Dependencies);
            builder.HasOne(d => d.DependentMod).WithMany();

            base.Configure(builder);
        }
    }
}
