using System;
using System.Collections.Generic;

namespace ModUpdateWorker
{
    public class ReleaseInfoDTO
    {
        public String FactorioVersion { get; set; }
        
        public List<String> Dependencies { get; set; }
    }
}