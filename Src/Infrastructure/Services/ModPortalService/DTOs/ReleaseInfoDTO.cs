using System;
using System.Collections.Generic;
using FactorioProductionCells.Application.Common.Interfaces.ModPortalService;

namespace FactorioProductionCells.Infrastructure.Services.ModPortalService.DTOs
{
    public class ReleaseInfoDTO : IReleaseInfoDTO
    {
        public String FactorioVersion { get; set; }
        public IList<String> Dependencies { get; set; } = new List<String>();

        public void PrintReleaseInfo()
        {
            Console.WriteLine($"    Mod.Releases.InfoJson.Factorio_Version: {this.FactorioVersion}");
            Console.WriteLine($"    Mod.Releases.InfoJson.Dependencies:");
            foreach(String d in this.Dependencies)
            {
                Console.WriteLine($"        {d}");
            }
        }
    }
}
