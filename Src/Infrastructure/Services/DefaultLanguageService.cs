using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Application.Common.Interfaces;

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
                    var db = scope.ServiceProvider.GetRequiredService<IFactorioProductionCellsDbContext>();
                    var dbLanguage = db.Languages
                        .SingleOrDefault(l => l.IsDefault);

                    if(dbLanguage == null)
                    {
                        throw new Exception("A default language wasn't found, and I can't be bothered to write a better Exception case for that right now.");
                    }

                    this._defaultLanguage = dbLanguage;
                }
            }

            return this._defaultLanguage;
        }
    }
}
