/*
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Application.Mods.Commands
{
    public partial class CreateModCommand : IRequest<Guid>
    {
        public String Name { get; set; }
        public IList<ModTitle> ModTitles { get; private set; } = new List<ModTitle>();
        public IList<Release> Releases { get; private set; } = new List<Release>();
    }

    public class CreateModCommandHandler : IRequestHandler<CreateModCommand, Guid>
    {
        private readonly IFactorioProductionCellsDbContext _dbContext;
        private readonly IDefaultLanguageService _defaultLanguageService;

        public CreateModCommandHandler(
            IFactorioProductionCellsDbContext dbContext,
            IDefaultLanguageService defaultLanguageService)
        {
            _dbContext = dbContext;
            _defaultLanguageService = defaultLanguageService;
        }

        public async Task<Guid> Handle(CreateModCommand request, CancellationToken cancellationToken)
        {
            var newMod = new Mod
            {
                Name = request.Name
            };

            newMod.Titles.Add(new ModTitle
            {
                Title = request.Title,
                LanguageId = _defaultLanguageService.GetDefaultLanguage().Id
            });
            


            _dbContext.Mods.Add(newMod);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return newMod.Id;
        }
    }
}
*/