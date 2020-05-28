using System;
using System.Collections.Generic;

namespace ModUpdateScheduler
{
    public class ModDTO
    {
        public String Name { get; set; }
        public String Title { get; set; }
        public ReleaseDTO Latest_Release { get; set; }

        public void PrintMod()
        {
            Console.WriteLine($"Mod.Name: {this.Name}");
            Console.WriteLine($"Mod.Title: {this.Title}");
            Latest_Release.PrintRelease();
        }
    }
}