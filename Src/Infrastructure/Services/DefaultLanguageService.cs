using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Infrastructure.Persistence;

namespace FactorioProductionCells.Infrastructure.Services
{
    public class DefaultLanguageService : IDefaultLanguageService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private Language _defaultLanguage;

        public DefaultLanguageService(
            IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        
        public Language GetDefaultLanguage()
        {
            if (_defaultLanguage == null)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<FactorioProductionCellsDbContext>();
                    var dbLanguage = db.Languages
                        .SingleOrDefault(l => l.IsDefault);

                    this._defaultLanguage = dbLanguage;
                }
            }

            return this._defaultLanguage;
        }
    }
}
