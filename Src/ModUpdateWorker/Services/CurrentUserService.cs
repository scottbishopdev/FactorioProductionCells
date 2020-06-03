using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Infrastructure.Persistence;

namespace FactorioProductionCells.ModUpdateWorker.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private User _currentUser;

        public CurrentUserService(
            IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public User GetCurrentUser()
        {
            if (_currentUser == null)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<FactorioProductionCellsDbContext>();
                    var dbUser = db.Users
                        .SingleOrDefault(u => u.Username == "ModUpdateWorker");

                    this._currentUser = dbUser;
                }
            }
            
            return this._currentUser;
        }
    }
}
