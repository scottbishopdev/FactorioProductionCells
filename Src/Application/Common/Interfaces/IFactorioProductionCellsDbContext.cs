using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Application.Common.Interfaces
{
    public interface IFactorioProductionCellsDbContext
    {
        DbSet<Language> Languages { get; set; }
        DbSet<Mod> Mods { get; set; }
        DbSet<ModTitle> ModTitles { get; set; }
        DbSet<Release> Releases { get; set; }
        DbSet<Dependency> Dependencies { get; set; }
        DbSet<User> Users { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
