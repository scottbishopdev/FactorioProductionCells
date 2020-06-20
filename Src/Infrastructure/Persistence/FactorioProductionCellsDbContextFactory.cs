using Microsoft.EntityFrameworkCore;

namespace FactorioProductionCells.Infrastructure.Persistence
{
    public class FactorioProductionCellsDbContextFactory : DesignTimeDbContextFactoryBase<FactorioProductionCellsDbContext>
    {
        protected override FactorioProductionCellsDbContext CreateNewInstance(DbContextOptions<FactorioProductionCellsDbContext> options)
        {
            return new FactorioProductionCellsDbContext(options);
        }
    }
}
