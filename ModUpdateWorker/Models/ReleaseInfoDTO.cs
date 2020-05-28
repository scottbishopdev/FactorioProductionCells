using System;
using System.Collections.Generic;

namespace ModUpdateWorker
{
    public class ReleaseInfoDTO
    {
        public String Factorio_Version { get; set; }
        
        public List<String> Dependencies { get; set; }
    }
}