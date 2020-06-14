/*
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Application.Users.Queries.GetUserByUserName
{
    public class GetUserByUserNameQuery : IRequest<User>
    {        
        public String UserName { get; set; }
    }

    public class GetUserByUserNameQueryHandler : IRequestHandler<GetUserByUserNameQuery, User>
    {
        private readonly IFactorioProductionCellsDbContext _dbContext;

        public GetUserByUserNameQueryHandler(
            IFactorioProductionCellsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .Where(u => u.UserName == request.UserName)
                .SingleOrDefaultAsync();
        }
    }
}
*/