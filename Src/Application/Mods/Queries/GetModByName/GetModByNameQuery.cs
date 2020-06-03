using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Application.Mods.Queries.GetModByName
{
    public class GetModByNameQuery : IRequest<Mod>
    {
        public String Name { get; set; }

        public class GetModViewModelQueryHandler : IRequestHandler<GetModByNameQuery, Mod>
        {
            private readonly IFactorioProductionCellsDbContext _dbContext;

            public GetModViewModelQueryHandler(IFactorioProductionCellsDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Mod> Handle(GetModByNameQuery request, CancellationToken cancellationToken)
            {
                return await _dbContext.Mods
                    .Include(m => m.Releases)
                    .Include(m => m.Titles)
                    .SingleOrDefaultAsync(m => m.Name == request.Name);
            }
        }
    }
}
