using System;

namespace FRCDataAccessLibrary
{
    public class ModTitle
    {
        public Guid ModId { get; set; }
        public Mod Mod { get; set; }
        public Guid LanguageId { get; set; }
        public Language Language { get; set; }
        public String Title { get; set; }
        public DateTime AddDate { get; set; }
    }
}
