using System;
using System.Collections.Generic;

namespace ModUpdateWorker
{
    public class ReleaseDTO
    {
        public String Download_URL { get; set; }
        
        public String File_Name { get; set; }

        public DateTime Released_At { get; set; }

        public ReleaseInfoDTO Info_Json { get; set; }

        public String Version { get; set; }

        public String Sha1 { get; set; }

        public void PrintRelease()
        {
            Console.WriteLine($"Mod.Releases.Version: {this.Version}");
            Console.WriteLine($"    Mod.Releases.Download_URL: {this.Download_URL}");
            Console.WriteLine($"    Mod.Releases.File_Name: {this.File_Name}");
            Console.WriteLine($"    Mod.Releases.Released_At: {this.Released_At}");
            Console.WriteLine($"    Mod.Releases.Sha1: {this.Sha1}");
            Console.WriteLine($"    Mod.Releases.Info_Json.Factorio_Version: {this.Info_Json.Factorio_Version}");
            Console.WriteLine($"    Mod.Releases.Info_Json.Dependencies:");

            foreach(String d in this.Info_Json.Dependencies)
            {
                Console.WriteLine($"        {d}");
            }
        }
    }
}