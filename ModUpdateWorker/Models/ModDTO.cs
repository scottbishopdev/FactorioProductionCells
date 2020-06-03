using System;
using System.Collections.Generic;
using FRCDataAccessLibrary;

namespace ModUpdateWorker
{
    public class ModDTO
    {
        public String Name { get; set; }
        public String Title { get; set; }
        public String Owner { get; set; }
        public String Summary { get; set; }
        public Int32 DownloadsCount { get; set; }
        public List<ReleaseDTO> Releases { get; set; }
        public String Thumbnail { get; set; }

        public void PrintMod()
        {
            Console.WriteLine($"Mod.Name: {this.Name}");
            Console.WriteLine($"Mod.Title: {this.Title}");
            Console.WriteLine($"Mod.Owner: {this.Owner}");
            Console.WriteLine($"Mod.Summary: {this.Summary}");
            Console.WriteLine($"Mod.Downloads_Count: {this.DownloadsCount}");
            Console.WriteLine($"Mod.Thumbnail: {this.Thumbnail}");

            this.Releases.ForEach(n => n.PrintRelease());
        }

        public Mod ToDbMod()
        {
            var dbMod = new Mod();
            dbMod.Name = this.Name;
            // TODO: Need to determine how to get language information before we can set the title property.
            //dbMod.Titles.Add(new ModTitle({Title = this.Title, LanguageId = ?????}));

            return dbMod;
        }
    }
}