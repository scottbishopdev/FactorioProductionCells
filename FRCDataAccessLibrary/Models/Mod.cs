using System;
using System.Collections.Generic;

namespace FRCDataAccessLibrary
{
    public class Mod
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public List<Release> Releases { get; set; } = new List<Release>();
        public List<ModTranslation> Titles { get; set; } = new List<ModTranslation>();
    }
}