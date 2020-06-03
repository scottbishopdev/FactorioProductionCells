using System;

namespace ModUpdateScheduler
{
    public class ReleaseDTO
    {
        public String DownloadUrl { get; set; }
        public String FileName { get; set; }
        public DateTime ReleasedAt { get; set; }
        public String Version { get; set; }
        public String Sha1 { get; set; }

        public void PrintRelease()
        {
            Console.WriteLine($"    Release.Version: {this.Version}");
            Console.WriteLine($"    Release.Released_At: {this.ReleasedAt}");
            Console.WriteLine($"    Release.Download_URL: {this.DownloadUrl}");
            Console.WriteLine($"    Release.File_Name: {this.FileName}");
            Console.WriteLine($"    Release.Sha1: {this.Sha1}");
        }
        
    }
}
