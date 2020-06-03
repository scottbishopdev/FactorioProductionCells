using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FactorioProductionCells.Application.Common.Extensions;
using FactorioProductionCells.Infrastructure.Persistence;

namespace FactorioProductionCells.ModUpdateScheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase<FactorioProductionCellsDbContext, Program>()
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
                    services.AddDbContext<FactorioProductionCellsDbContext>(options =>
                    {
                        options.UseNpgsql(
                            Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING"),
                            x => x.MigrationsAssembly("Infrastructure")
                        );
                    });
                    services.AddHostedService<Worker>();
                    
                });
    }
}
