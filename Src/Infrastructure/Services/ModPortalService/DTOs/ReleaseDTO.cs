using System;
using System.Collections.Generic;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.ValueObjects;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Application.Common.Interfaces.ModPortalService;

namespace FactorioProductionCells.Infrastructure.Services.ModPortalService.DTOs
{
    public class ReleaseDTO /*: IMapFrom<Release>*/ : IReleaseDTO
    {
        public String DownloadURL { get; set; }
        public String FileName { get; set; }
        public DateTime ReleasedAt { get; set; }
        public IReleaseInfoDTO InfoJson { get; set; }
        public String Version { get; set; }
        public String Sha1 { get; set; }

        public void PrintRelease()
        {
            Console.WriteLine($"Mod.Releases.Version: {this.Version}");
            Console.WriteLine($"    Mod.Releases.Download_URL: {this.DownloadURL}");
            Console.WriteLine($"    Mod.Releases.File_Name: {this.FileName}");
            Console.WriteLine($"    Mod.Releases.Released_At: {this.ReleasedAt}");
            Console.WriteLine($"    Mod.Releases.Sha1: {this.Sha1}");
            InfoJson.PrintReleaseInfo();
        }

        /*
        public Release ToDbRelease(Guid ModId)
        {
            var dbDependencies = new List<Dependency>();
            foreach (var dependencyString in this.InfoJson.Dependencies)
            {
                dbDependencies.Add(Dependency.For(dependencyString));
            }
            
            return new Release(
                ModId: ModId, // TODO: wat? How do I get this?
                ReleasedAt: this.ReleasedAt,
                Sha1: this.Sha1,
                DownloadUrl: ReleaseDownloadUrl.For(this.DownloadURL),
                ReleaseFileName: ReleaseFileName.For(this.FileName),
                Version: ModVersion.For(this.Version),
                FactorioVersion: FactorioVersion.For(InfoJson.FactorioVersion),
                Dependencies: dbDependencies
            );
        }
        */
    }
}
