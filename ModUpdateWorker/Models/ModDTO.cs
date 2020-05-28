using System;
using System.Collections.Generic;

namespace ModUpdateWorker
{
    public class ModDTO
    {
        public String Name { get; set; }
        public String Title { get; set; }
        public String Owner { get; set; }
        public String Summary { get; set; }
        public Int32 Downloads_Count { get; set; }
        public List<ReleaseDTO> Releases { get; set; }
        public String Thumbnail { get; set; }

        public void PrintMod()
        {
            Console.WriteLine($"Mod.Name: {this.Name}");
            Console.WriteLine($"Mod.Title: {this.Title}");
            Console.WriteLine($"Mod.Owner: {this.Owner}");
            Console.WriteLine($"Mod.Summary: {this.Summary}");
            Console.WriteLine($"Mod.Downloads_Count: {this.Downloads_Count}");
            Console.WriteLine($"Mod.Thumbnail: {this.Thumbnail}");

            this.Releases.ForEach(n => n.PrintRelease());
        }
    }
}