using Microsoft.EntityFrameworkCore;

namespace FRCDataAccessLibrary
{
    public class ModContext : DbContext
    {
        public DbSet<Mod> Mods { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ModTitle> ModTitles { get; set; }

        public ModContext(DbContextOptions<ModContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("public");
            builder.HasPostgresExtension("uuid-ossp");

            // Language table
            builder.Entity<Language>().HasKey(l => l.Id);
            // Columns
            builder.Entity<Language>().Property(l => l.Id).HasDefaultValueSql("uuid_generate_v4()").ValueGeneratedOnAdd().IsRequired();
            builder.Entity<Language>().Property(l => l.EnglishName).HasMaxLength(50).IsRequired();
            builder.Entity<Language>().Property(l => l.LanguageCode).HasMaxLength(20).IsRequired();
            builder.Entity<Language>().Property(l => l.AddDate).HasDefaultValueSql("now()").ValueGeneratedOnAdd().IsRequired();
            // Indexes
            builder.Entity<Language>().HasMany(l => l.ModTitles).WithOne(mt => mt.Language);

            // Mod table
            builder.Entity<Mod>().HasKey(m => m.Id);
            // Columns
            builder.Entity<Mod>().Property(m => m.Id).HasDefaultValueSql("uuid_generate_v4()").ValueGeneratedOnAdd().IsRequired();
            builder.Entity<Mod>().Property(m => m.Name).HasMaxLength(200).IsRequired();
            builder.Entity<Mod>().Property(m => m.AddDate).HasDefaultValueSql("now()").ValueGeneratedOnAdd().IsRequired();
            builder.Entity<Mod>().Property(m => m.UpdateDate).HasDefaultValueSql("now()").ValueGeneratedOnAddOrUpdate().IsRequired();
            // Indexes
            builder.Entity<Mod>().HasMany(m => m.Releases).WithOne(r => r.Mod);
            builder.Entity<Mod>().HasMany(m => m.Titles).WithOne(mt => mt.Mod);

            // ModTitle table
            builder.Entity<ModTitle>().HasKey(mt => new {mt.ModId, mt.LanguageId});
            // Columns
            builder.Entity<ModTitle>().Property(mt => mt.ModId).IsRequired();
            builder.Entity<ModTitle>().Property(mt => mt.LanguageId).IsRequired();
            builder.Entity<ModTitle>().Property(mt => mt.Title).HasMaxLength(200).IsRequired();
            builder.Entity<ModTitle>().Property(mt => mt.AddDate).HasDefaultValueSql("now()").ValueGeneratedOnAdd().IsRequired();
            // Indexes
            builder.Entity<ModTitle>().HasOne(mt => mt.Mod).WithMany(m => m.Titles);
            builder.Entity<ModTitle>().HasOne(mt => mt.Language);

            // Release table
            builder.Entity<Release>().HasKey(r => r.Id);
            // Columns
            builder.Entity<Release>().Property(r => r.Id).HasDefaultValueSql("uuid_generate_v4()").ValueGeneratedOnAdd().IsRequired();
            builder.Entity<Release>().Property(r => r.Version).HasMaxLength(50).IsRequired();
            builder.Entity<Release>().Property(r => r.AddDate).HasDefaultValueSql("now()").ValueGeneratedOnAdd().IsRequired();
            builder.Entity<Release>().Property(r => r.ReleasedAt).IsRequired();
            builder.Entity<Release>().Property(r => r.DownloadUrl).HasMaxLength(200).IsRequired();
            builder.Entity<Release>().Property(r => r.FileName).HasMaxLength(200).IsRequired();
            builder.Entity<Release>().Property(r => r.Sha1).HasMaxLength(50).IsRequired();
            // Indexes
            builder.Entity<Release>().HasOne(r => r.Mod).WithMany(m => m.Releases);

            base.OnModelCreating(builder);
        }
    }
}
