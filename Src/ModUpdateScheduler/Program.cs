using System;
using System.Linq;
using System.Threading;
using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Application.Common.Interfaces.ModPortalService;
// TODO: Since this project is akin to the Presentation layer in Clean Architecture, is it actually OK to reference these namespaces? On one hand, this is the only
// place in this project where Infrastructure is referenced, and it's only used to wire up the interfaces and their comcrete implementations that are defined in the 
// Infrastructure and ModUpdateScheduler projects. We still remove direct dependencies for *most* of the code in the project. On the other hand, isn't there still
// technically a direct dependency on infrastructure here??
using FactorioProductionCells.Infrastructure.Identity;
using FactorioProductionCells.Infrastructure.Messaging;
using FactorioProductionCells.Infrastructure.Services;
using FactorioProductionCells.Infrastructure.Services.ModPortalService;
using FactorioProductionCells.Infrastructure.Persistence;
using FactorioProductionCells.ModUpdateScheduler.Services;

namespace FactorioProductionCells.ModUpdateScheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // TODO: Maybe we should put some kind of check in here that just makes it wait until Postgres and RabbitMQ are alive. Alternatively, we might be able to mitigate
            // this wait by implementing a retry solution (e.g. Polly). Alternatively, we could have the process kill itsels if they're not up and rely on some kind of Docker
            // or k8s retry policy.
            Console.WriteLine("Waiting for 45 seconds to ensure Postgres and RabbitMQ have started up...");
            Thread.Sleep(45*1000);

            Console.WriteLine("Creating the host...");
            var host = CreateHostBuilder(args).Build();

            Console.WriteLine("Attempting to migrate the database...");
            TryMigrateDatabase(host);

            Console.WriteLine("Attempting to create seed data for the database...");
            TrySeedDatabase(host);

            host.Run();
        }

        // TODO: See if there's some reasonable way to create this as an extension that can be shared between the services. It *shouldn't* live in the Application layer, though.
        private static void TryMigrateDatabase(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                
                try
                {
                    // TODO: This cast is dubious at best. There's probably a better way to handle this for migrations.
                    logger.LogInformation("Attempting to get an instance of FactorioProductionCellsDbContext to check for migration state.");
                    var db = (FactorioProductionCellsDbContext)scope.ServiceProvider.GetRequiredService<IFactorioProductionCellsDbContext>();

                    var lastAppliedMigration = db.Database.GetAppliedMigrations().LastOrDefault();
                    var lastDefinedMigration = db.Database.GetMigrations().LastOrDefault();

                    logger.LogInformation($"Last applied migration id: {lastAppliedMigration}. Last defined migration id: {lastDefinedMigration}");
                    if(lastAppliedMigration == lastDefinedMigration)
                    {
                        logger.LogInformation("Database is up to date.");
                    }
                    else
                    {
                        logger.LogInformation($"Applying outstanding migrations up to id: {lastDefinedMigration}");
                        db.Database.Migrate();
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred while performing migration of the database context {typeof(FactorioProductionCellsDbContext).FullName}.");
                    throw ex;
                }
            }
        }

        private static async void TrySeedDatabase(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                
                try
                {
                    logger.LogInformation($"Attempting to seed the database context {typeof(FactorioProductionCellsDbContext).FullName} with default language data...");
                    var dbContext = scope.ServiceProvider.GetRequiredService<IFactorioProductionCellsDbContext>();
                    var defaultLanguage = await FactorioProductionCellsDbContextSeed.SeedDefaultLanguageAsync(dbContext);

                    logger.LogInformation($"Attempting to seed the database context {typeof(FactorioProductionCellsDbContext).FullName} with default user data...");
                    var identityService = scope.ServiceProvider.GetRequiredService<IIdentityService>();
                    var modUpdateSchedulerUser = await FactorioProductionCellsDbContextSeed.SeedModUpdateSchedulerUser(identityService, defaultLanguage);
                    logger.LogInformation($"Successfully created the user {modUpdateSchedulerUser.UserName} with Id {modUpdateSchedulerUser.Id} and email \"{modUpdateSchedulerUser.Email}\".");

                    logger.LogInformation($"Data seed of database context {typeof(FactorioProductionCellsDbContext).FullName} complete.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred while seeding the database context {typeof(FactorioProductionCellsDbContext).FullName} with data.");
                    throw ex;
                }
            }
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
                    // TODO: Not a fan of this. It feels like we're shoehorning in the ability to get a concrete FactorioProductionCellsDbContext just because the UserManager
                    // that the IdentityService uses wants one. I don't think I really have much control over what the UserManager is demanding since ef core is kinda doing
                    // that automagically. Could I override the UserManager with my own class that inherits from it and expects the interface instead?
                    services.AddDbContext<FactorioProductionCellsDbContext>(options =>
                    {
                        options.UseNpgsql(
                            Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING"),
                            x => x.MigrationsAssembly("FactorioProductionCells.Infrastructure")
                        );
                    });
                    services.AddScoped<IFactorioProductionCellsDbContext>(sp => sp.GetRequiredService<FactorioProductionCellsDbContext>());

                    services.AddIdentityCore<User>().AddEntityFrameworkStores<FactorioProductionCellsDbContext>();

                    Console.WriteLine("Attempting to add RabbitMqAdapter as singleton...");
                    services.AddSingleton<IDateTimeService, DateTimeService>();

                    Console.WriteLine("Attempting to add RabbitMqAdapter as singleton...");
                    services.AddSingleton<IMessageQueue, RabbitMqAdapter>();
                    
                    Console.WriteLine("Attempting to add IdentityService as singleton...");
                    services.AddSingleton<IIdentityService, IdentityService>();

                    Console.WriteLine("Attempting to add CurrentUserService as singleton...");
                    services.AddSingleton<ICurrentUserService, CurrentUserService>();

                    Console.WriteLine("Attempting to add ModPortalService as singleton...");
                    services.AddSingleton<IModPortalService, ModPortalService>();

                    Console.WriteLine("Attempting to add MediatR...");
                    // TODO: It feels kinda dirty to get the assembly from some arbitrary class within it. Can we explicitly request the "Application" assembly somehow?S
                    services.AddMediatR(typeof(IFactorioProductionCellsDbContext).GetTypeInfo().Assembly);

                    Console.WriteLine("Attempting to add the Worker as a hosted service...");
                    services.AddHostedService<Worker>();
                });
    }
}
