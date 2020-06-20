using System;
using System.Collections.Generic;

namespace FactorioProductionCells.Application.Common.Interfaces.ModPortalService
{
    public interface IModDTO
    {
        String Name { get; set; }
        String Title { get; set; }
        String Owner { get; set; }
        String Summary { get; set; }
        Int32 DownloadsCount { get; set; }
        String Thumbnail { get; set; }
        IList<IReleaseDTO> Releases { get; set; }
        IReleaseDTO LatestRelease { get; set; }

        void PrintMod();
    }
}
