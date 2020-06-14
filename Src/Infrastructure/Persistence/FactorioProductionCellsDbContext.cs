using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FactorioProductionCells.Infrastructure.Persistence
{
    //public class FactorioProductionCellsDbContext : DbContext, IFactorioProductionCellsDbContext
    public class FactorioProductionCellsDbContext : IdentityUserContext<NetCoreUser, Guid>, IFactorioProductionCellsDbContext
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

        public DbSet<Mod> Mods { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ModTitle> ModTitles { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<Dependency> Dependencies { get; set; }
        public DbSet<DependencyType> DependencyTypes { get; set; }
        public DbSet<DependencyComparisonType> DependencyComparisonTypes { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    // TODO: The calls to GetCurrentUserId() are causing SERIOUS trouble here. How the fuck am I supposed to get an ID without being dependent on the dbContext?
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
            modelBuilder.HasPostgresExtension("uuid-ossp");
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FactorioProductionCellsDbContext).Assembly);

            /*
            // Create initial users.
            modelBuilder.Entity<NetCoreUser>().HasData(
                new NetCoreUser
                {
                    UserName = Environment.GetEnvironmentVariable("ADMINISTRATOR_USERNAME"),
                    //Password: Environment.GetEnvironmentVariable("ADMINISTRATOR_USERNAME"),
                    Email = Environment.GetEnvironmentVariable("ADMINISTRATOR_USERNAME")
                },
                new NetCoreUser
                {
                    UserName = Environment.GetEnvironmentVariable("MODUPDATESCHEDULER_USERNAME"),
                    //Password: Environment.GetEnvironmentVariable("MODUPDATESCHEDULER_EMAIL"),
                    Email = Environment.GetEnvironmentVariable("MODUPDATESCHEDULER_PASSWORD")
                },
                new NetCoreUser
                {
                    UserName = Environment.GetEnvironmentVariable("MODUPDATEWORKER_USERNAME"),
                    //Password = Environment.GetEnvironmentVariable("MODUPDATEWORKER_EMAIL"),
                    Email = Environment.GetEnvironmentVariable("MODUPDATEWORKER_PASSWORD")
                }
            );
            */

            base.OnModelCreating(modelBuilder);
        }
    }
}
