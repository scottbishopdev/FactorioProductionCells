using System;
using FactorioProductionCells.Application.Common.Interfaces.ModPortalService;

namespace FactorioProductionCells.Infrastructure.Services.ModPortalService.DTOs
{
    public class ReleaseDTO : IReleaseDTO
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
    }
}
