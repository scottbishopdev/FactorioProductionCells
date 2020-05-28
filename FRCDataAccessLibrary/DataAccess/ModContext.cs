using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FRCDataAccessLibrary
{
    public class ModContext : DbContext
    {
        public DbSet<Mod> Mods { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ModTranslation> ModTranslations { get; set; }

        public ModContext(DbContextOptions<ModContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("public");
            builder.HasPostgresExtension("uuid-ossp");

            // Language table
            builder.Entity<Language>()
                .HasKey(l => l.Id);
            builder.Entity<Language>()
                .Property(l => l.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Entity<Language>()
                .Property(l => l.EnglishName)
                .HasMaxLength(50)
                .IsRequired();
            builder.Entity<Language>()
                .Property(l => l.LanguageCode)
                .HasMaxLength(20)
                .IsRequired();
            builder.Entity<Language>()
                .Property(l => l.AddDate)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd()
                .IsRequired();

            // Mod table
            builder.Entity<Mod>()
                .HasKey(m => m.Id);
            builder.Entity<Mod>()
                .Property(m => m.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Entity<Mod>()
                .Property(m => m.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder.Entity<Mod>()
                .Property(m => m.AddDate)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Entity<Mod>()
                .Property(m => m.UpdateDate)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();
            // Relationships
            builder.Entity<Mod>()
                .HasMany(m => m.Releases)
                .WithOne(r => r.Mod);
            builder.Entity<Mod>()
                .HasMany(m => m.Titles)
                .WithOne(mt => mt.Mod);

            // ModTranslation table
            builder.Entity<ModTranslation>()
                .HasKey(mt => new {mt.ModId, mt.LanguageId});
            builder.Entity<ModTranslation>()
                .Property(mt => mt.ModId)
                .IsRequired();
            builder.Entity<ModTranslation>()
                .Property(mt => mt.LanguageId)
                .IsRequired();
            builder.Entity<ModTranslation>()
                .Property(mt => mt.Title)
                .HasMaxLength(200)
                .IsRequired();
            builder.Entity<ModTranslation>()
                .Property(mt => mt.AddDate)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd()
                .IsRequired();
            // Relationships
            builder.Entity<ModTranslation>()
                .HasOne(mt => mt.Mod)
                .WithMany(m => m.Titles);
            builder.Entity<ModTranslation>()
                .HasOne(mt => mt.Language);

            // Release table
            builder.Entity<Release>()
                .HasKey(r => r.Id);
            builder.Entity<Release>()
                .Property(m => m.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Entity<Release>()
                .Property(m => m.Version)
                .HasMaxLength(50)
                .IsRequired();
            builder.Entity<Release>()
                .Property(m => m.AddDate)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd()
                .IsRequired();
            // Relationships
            builder.Entity<Release>()
                .HasOne(r => r.Mod)
                .WithMany(m => m.Releases);

            base.OnModelCreating(builder);
        }
    }
}
