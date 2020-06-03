using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FRCDataAccessLibrary;

namespace ModUpdateScheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase<ModContext>()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                    logging.AddDebug();
                    logging.AddConsole();
                })
                .ConfigureServices(services =>
                {
                    services.AddDbContext<ModContext>(options =>
                    {
                        options.UseNpgsql(
                            Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING"),
                            x => x.MigrationsAssembly("FRCDataAccessLibrary")
                        );
                    });
                    services.AddHostedService<Worker>();
                    //services.
                });
    }
}
