using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FactorioProductionCells.Application.Common.Extensions;
using FactorioProductionCells.Infrastructure.Persistence;

namespace FactorioProductionCells.ModUpdateWorker
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
                .ConfigureServices((hostContext, services) =>
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
                
        // TODO: Due to some smart shenanigans going on in the Application layer, this needs to include a few services. Clean Architecture provides an extension method for this. Should I?
        /*
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        */

            
    }
}
