using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FactorioProductionCells.Infrastructure.Persistence
{
    public class FactorioProductionCellsDbContext : IdentityUserContext<User, Guid>, IFactorioProductionCellsDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;
        
        public FactorioProductionCellsDbContext(DbContextOptions<FactorioProductionCellsDbContext> options) : base(options) {}

        public FactorioProductionCellsDbContext(
            DbContextOptions<FactorioProductionCellsDbContext> options, 
            ICurrentUserService currentUserService,
            IDateTimeService dateTimeService) : base(options)
        {
            _currentUserService = currentUserService;
            _dateTimeService = dateTimeService;
        }

        // TODO: Some of these (e.g. dependencies) probably don't need to be queried directly and can be solely accessed through other entities. I'm concerned
        // that the migration system might not puck up on schema changes if those DbSets are removed, though.
        public DbSet<Mod> Mods { get; set; }
        //public DbSet<Release> Releases { get; set; }
        public DbSet<Language> Languages { get; set; }
        //public DbSet<ModTitle> ModTitles { get; set; }
        //public DbSet<Dependency> Dependencies { get; set; }
        //public DbSet<DependencyType> DependencyTypes { get; set; }
        //public DbSet<DependencyComparisonType> DependencyComparisonTypes { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                // TODO: I'm not a fan of the fact that our EF code is responsible for setting these, rather than a default value or a database trigger.
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.AddedBy = _currentUserService.GetCurrentUserId();
                        entry.Entity.AddedDate = _dateTimeService.GetCurrentTime();
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.GetCurrentUserId();
                        entry.Entity.LastModified = _dateTimeService.GetCurrentTime();
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FactorioProductionCellsDbContext).Assembly);
        }
    }
}
