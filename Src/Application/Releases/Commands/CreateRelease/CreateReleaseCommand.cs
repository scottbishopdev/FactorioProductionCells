/*
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.ValueObjects;

namespace FactorioProductionCells.Application.Releases.Commands
{
    public partial class CreateReleaseCommand : IRequest<Guid>
    {
        public Guid ModId { get; set; }
        public String Version { get; set; }
        public DateTime ReleasedAt { get; set; }
        public String DownloadUrl { get; set; }
        public String FileName { get; set; }
        public String Sha1 { get; set; }
        public String FactorioVersion { get; set; }
        
        // Are we allowed to have Lists here? I think so?
        public IList<Dependency> Dependencies { get; private set; } = new List<Dependency>();
    }

    public class CreateReleaseCommandHandler : IRequestHandler<CreateReleaseCommand, Guid>
    {
        private readonly IFactorioProductionCellsDbContext _dbContext;
        //private readonly IDefaultLanguageService _defaultLanguageService;

        public CreateReleaseCommandHandler(
            IFactorioProductionCellsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateReleaseCommand request, CancellationToken cancellationToken)
        {
            var newRelease = new Release
            {
                ModId = request.ModId,
                
                        Name = request.Version,

                ReleasedAt = request.ReleasedAt,
                DownloadUrl = request.DownloadUrl,
                FileName = request.FileName,
                Sha1 = request.Sha1,
                FactorioVersion = (FactorioVersion)request.FactorioVersion
            };
            foreach (Dependency d in request.Dependencies)
            {
                newRelease.Dependencies.Add(d);
            }

            _dbContext.Releases.Add(newRelease);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return newRelease.Id;
        }
    }
}
*/