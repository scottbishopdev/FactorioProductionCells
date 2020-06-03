using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Application.Common.Interfaces;

namespace FactorioProductionCells.Infrastructure.Persistence
{
    public class FactorioProductionCellsDbContext : DbContext, IFactorioProductionCellsDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        
        public FactorioProductionCellsDbContext(DbContextOptions<FactorioProductionCellsDbContext> options) : base(options) {}

        public FactorioProductionCellsDbContext(
            DbContextOptions<FactorioProductionCellsDbContext> options, 
            ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Mod> Mods { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ModTitle> ModTitles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Dependency> Dependencies { get; set; }

        // By overriding SaveChangesAsync() in this manner, we don't have to worry about manually setting the Added/LastUpdated values on AuditableEntities.
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    // TODO: I'm not a fan of the fact that our EF code is responsible for setting these, rather than a default value or a database trigger.
                    case EntityState.Added:
                        entry.Entity.AddedBy = _currentUserService.GetCurrentUser().Id;
                        // TODO: Other implementations of Clean Architecture use an injected service to get the current time. Why? Should I be doing that??
                        entry.Entity.AddedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.GetCurrentUser().Id;
                        entry.Entity.LastModified = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FactorioProductionCellsDbContext).Assembly);
        }
    }
}
