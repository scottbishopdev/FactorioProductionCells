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
        public IList<Release> Releases { get; private set; } = new List<Release>();
        public IList<ModTitle> Titles { get; private set; } = new List<ModTitle>();
    }
}
