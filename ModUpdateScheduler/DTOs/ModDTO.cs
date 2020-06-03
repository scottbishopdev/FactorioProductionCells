using System;

namespace ModUpdateScheduler
{
    public class ModDTO
    {
        public String Name { get; set; }
        public String Title { get; set; }
        public ReleaseDTO LatestRelease { get; set; }
    }
}