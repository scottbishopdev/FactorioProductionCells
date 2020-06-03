using System;

namespace ModUpdateWorker
{
    public class ReleaseDTO
    {
        public String DownloadURL { get; set; }
        
        public String FileName { get; set; }

        public DateTime ReleasedAt { get; set; }

        public ReleaseInfoDTO InfoJson { get; set; }

        public String Version { get; set; }

        public String Sha1 { get; set; }

        public void PrintRelease()
        {
            Console.WriteLine($"Mod.Releases.Version: {this.Version}");
            Console.WriteLine($"    Mod.Releases.Download_URL: {this.DownloadURL}");
            Console.WriteLine($"    Mod.Releases.File_Name: {this.FileName}");
            Console.WriteLine($"    Mod.Releases.Released_At: {this.ReleasedAt}");
            Console.WriteLine($"    Mod.Releases.Sha1: {this.Sha1}");
            Console.WriteLine($"    Mod.Releases.Info_Json.Factorio_Version: {this.InfoJson.FactorioVersion}");
            Console.WriteLine($"    Mod.Releases.Info_Json.Dependencies:");

            foreach(String d in this.InfoJson.Dependencies)
            {
                Console.WriteLine($"        {d}");
            }
        }
    }
}