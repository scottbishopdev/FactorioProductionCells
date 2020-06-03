using System;
using System.Collections.Generic;

namespace FRCDataAccessLibrary
{
    public class Language
    {
        public Guid Id { get; set; }
        public String EnglishName { get; set; }
        public String LanguageCode { get; set; }
        public DateTime AddDate { get; set; }
        public List<ModTitle> ModTitles { get; private set; } = new List<ModTitle>();
    }
}
