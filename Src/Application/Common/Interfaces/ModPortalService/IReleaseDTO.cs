using System;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Application.Common.Interfaces.ModPortalService
{
    public interface IReleaseDTO
    {
        String DownloadURL { get; set; }
        String FileName { get; set; }
        DateTime ReleasedAt { get; set; }
        IReleaseInfoDTO InfoJson { get; set; }
        String Version { get; set; }
        String Sha1 { get; set; }
        
        void PrintRelease();

        //Release ToDbRelease();
    }
}
