using System;

namespace FRCDataAccessLibrary
{
    public class Release
    {
        public Guid Id { get; set; }
        public Guid ModId { get; set; }
        public Mod Mod { get; set; }
        public String Version { get; set; }
        public DateTime AddDate { get; set; }
    }
}