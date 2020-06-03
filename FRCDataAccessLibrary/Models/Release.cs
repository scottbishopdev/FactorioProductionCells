using System;

namespace FRCDataAccessLibrary
{
    public class Release
    {
        public Guid Id { get; set; }
        public Guid ModId { get; set; }
        public Mod Mod { get; set; }

        public String Version { get; set; }
        public DateTime ReleasedAt { get; set; }
        public String DownloadUrl { get; set; }
        public String FileName { get; set; }
        public String Sha1 { get; set; }

        public DateTime AddDate { get; set; }
    }
}