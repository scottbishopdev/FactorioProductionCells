using System;
using System.Collections.Generic;

namespace FactorioProductionCells.Infrastructure.Services.ModPortalService
{
    public class ReleaseInfoDTO
    {
        public String FactorioVersion { get; set; }
        public List<String> Dependencies { get; set; }
    }
}