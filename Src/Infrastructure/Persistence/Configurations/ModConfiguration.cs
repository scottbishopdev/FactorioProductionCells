using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Infrastructure.Services;

namespace FactorioProductionCells.Infrastructure.Persistence.Configurations
{
    public class ModConfiguration : AuditableEntityConfiguration<Mod>
    {
        private readonly IDefaultLanguageService _defaultLanguageService;
        
        public ModConfiguration(
            IDefaultLanguageService defaultLanguageService)
        {
            _defaultLanguageService = defaultLanguageService;
        }

        public override void Configure(EntityTypeBuilder<Mod> builder)
        {
            // Primary Key
            builder.HasKey(m => m.Id);
            // Columns
            builder.Property(m => m.Id).HasDefaultValueSql("uuid_generate_v4()").ValueGeneratedOnAdd().IsRequired();
            builder.Property(m => m.Name).HasMaxLength(Mod.NameLength).IsRequired();
            // Ignored properties
            // TODO: Determine if we need to ignore static properties. We can't reference them like this, but also, EF might decide it wants to store it.
            //builder.Ignore(m => m.NameLength);
            // Indexes
            builder.HasMany(m => m.Releases).WithOne(r => r.Mod);
            builder.HasMany(m => m.Titles).WithOne(mt => mt.Mod);

            builder.HasIndex(m => m.Name).IsUnique();


            /*
            Mod baseMod = new Mod(
                Name: Mod.BaseModName,
                Titles: new List<ModTitle>(),
                Releases: new List<Release>()
            );

            baseMod.TryAddTitle(
                languageId: _defaultLanguageService.GetDefaultLanguage().Id,
                title: "Factorio"
            );


            baseMod.TryAddRelease(
                releasedAt: DateTime.Parse("6/1/2020 3:24:52 AM"),
                sha1: "b53320182d7e53b81b1009d8353492614f9a5cac",
                downloadUrl: ReleaseDownloadUrl.For("/download/testMod/5a5f1ae6adcc441024d72e83"),
                releaseFileName: ReleaseFileName.For("testMod_0.1.9.zip"),
                version: ModVersion.For("0.14.0"),
                factorioVersion: FactorioVersion.For("0.17"),
                dependencies: new List<Dependency>()
            );
            */

            base.Configure(builder);
        }
    }
}
