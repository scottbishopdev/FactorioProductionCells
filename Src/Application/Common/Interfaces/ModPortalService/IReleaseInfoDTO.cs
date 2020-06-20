using System;
using System.Collections.Generic;

namespace FactorioProductionCells.Application.Common.Interfaces.ModPortalService
{
    public interface IReleaseInfoDTO
    {
        String FactorioVersion { get; set; }
        IList<String> Dependencies { get; set; }
        
        void PrintReleaseInfo();
    }
}
