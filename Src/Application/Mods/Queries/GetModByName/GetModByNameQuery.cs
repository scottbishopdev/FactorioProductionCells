using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Application.Mods.Queries.GetModByName
{
    
    
    public class GetModByNameQuery : IRequest<Mod>
    {        
        public String Name { get; set; }
    }

    public class GetModByNameQueryHandler : IRequestHandler<GetModByNameQuery, Mod>
    {
        private readonly ILogger<GetModByNameQueryHandler> _logger;
        //private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IFactorioProductionCellsDbContext _dbContext;
        //private readonly FactorioProductionCellsDbContext _dbContext;

        // TODO: Probably gonna have to user an alternative method to get the dbContext here, as it probably won't be available via DI.
        public GetModByNameQueryHandler(
            ILogger<GetModByNameQueryHandler> logger,
            //IServiceScopeFactory serviceScopeFactory
            IFactorioProductionCellsDbContext dbContext)
            //FactorioProductionCellsDbContext dbContext)
        {
            _logger = logger;
            //_serviceScopeFactory = serviceScopeFactory;
            _dbContext = dbContext;
        }

        public async Task<Mod> Handle(GetModByNameQuery request, CancellationToken cancellationToken)
        {
            // TODO: can't DI in a db context because it hates us.
            
            _logger.LogInformation($"In GetModByNameQueryHandler. Trying to find mod with name \"{request.Name}\".");
            
            return await _dbContext.Mods
                .Where(m => m.Name == request.Name)
                //.Include(m => m.Releases)
                //.Include(m => m.Titles)
                .SingleOrDefaultAsync(cancellationToken);
            

            /*
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<IFactorioProductionCellsDbContext>();
                return await db.Mods
                    .Where(m => m.Name == request.Name)
                    //.Include(m => m.Releases)
                    //.Include(m => m.Titles)
                    .SingleOrDefaultAsync(cancellationToken);
            }
            */
        }
    }
}
