using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
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
        private readonly IFactorioProductionCellsDbContext _dbContext;

        public GetModByNameQueryHandler(
            ILogger<GetModByNameQueryHandler> logger,
            IFactorioProductionCellsDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Mod> Handle(GetModByNameQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"In GetModByNameQueryHandler. Trying to find mod with name \"{request.Name}\".");
            
            return await _dbContext.Mods
                .Where(m => m.Name == request.Name)
                .Include(m => m.Releases)
                .Include(m => m.Titles)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
