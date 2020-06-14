using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Application.Common.Interfaces
{
    public interface IFactorioProductionCellsDbContext
    {
        DbSet<Mod> Mods { get; set; }
        DbSet<Release> Releases { get; set; }
        DbSet<Language> Languages { get; set; }
        DbSet<ModTitle> ModTitles { get; set; }
        //DbSet<User> Users { get; set; }
        DbSet<Dependency> Dependencies { get; set; }
        DbSet<DependencyType> DependencyTypes { get; set; }
        DbSet<DependencyComparisonType> DependencyComparisonTypes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
