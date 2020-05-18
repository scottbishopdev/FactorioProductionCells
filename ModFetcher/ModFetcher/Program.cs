using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

namespace ModFetcher
{
    public class Program
    {
        private const String modPortalUrl = "https://mods.factorio.com/api/";

        public static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            var modService = new ModService(httpClient);
            
            List<String> modNameList = await modService.GetModNameList();

            Mod newMod = await modService.GetModByName("boblibrary");
            newMod.PrintMod();

            //CreateHostBuilder(args).Build().Run();
        }

        /*
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
                */
    }
}
