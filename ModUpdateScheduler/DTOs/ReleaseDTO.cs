using System;
using System.Collections.Generic;

namespace ModUpdateScheduler
{
    public class ReleaseDTO
    {
        public String Download_URL { get; set; }
        public String File_Name { get; set; }
        public DateTime Released_At { get; set; }
        public String Version { get; set; }
        public String Sha1 { get; set; }

        public void PrintRelease()
        {
            Console.WriteLine($"    Release.Version: {this.Version}");
            Console.WriteLine($"    Release.Released_At: {this.Released_At}");
            Console.WriteLine($"    Release.Download_URL: {this.Download_URL}");
            Console.WriteLine($"    Release.File_Name: {this.File_Name}");
            Console.WriteLine($"    Release.Sha1: {this.Sha1}");
        }
        
    }
}
