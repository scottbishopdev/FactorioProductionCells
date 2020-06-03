/*
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FactorioProductionCells.Application.Common.Interfaces;

namespace FactorioProductionCells.Application.Mods.Queries.GetModViewModel
{
    public class GetModViewModelQuery : IRequest<ModViewModel>
    {
        public String modName;

        public class GetModViewModelQueryHandler : IRequestHandler<GetModViewModelQuery, ModViewModel>
        {
            private readonly IFactorioProductionCellsDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetModViewModelQueryHandler(
                IFactorioProductionCellsDbContext dbContext,
                IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task<ModViewModel> Handle(GetModViewModelQuery request, CancellationToken cancellationToken)
            {
                var viewModel = new ModViewModel();

                viewModel.Mod = await _dbContext.Mods
                    .ProjectTo<ModDto>(_mapper.ConfigurationProvider)
                    .Include(mod => mod.Releases)
                    .SingleOrDefault(m => m.Name == request.modName);

                return viewModel;
            }
        }
    }
}
*/