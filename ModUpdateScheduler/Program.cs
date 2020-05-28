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
            
            /*
            IHost host = CreateHostBuilder(args).Build();

            if(MigrateDatabase<ModContext>(host))
            {
                host.Run();
            }
            else
            {
                return;
            }
            */

            /*
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                // TODO: Implement check for current database version, targeted database version, and logging to indicate whether or not we're attempting a migration.
                try
                {
                    var db = services.GetRequiredService<ModContext>();
                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, $"An error occurred while performing migration of the database context {typeof(ModContext).FullName}. ");
                    return;
                }
            }

            host.Run();
            */
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
                });


        /*
        public Boolean MigrateDatabase<T>(IHost host) where T : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                // TODO: Implement check for current database version, targeted database version, and logging to indicate whether or not we're attempting a migration.
                try
                {
                    var db = services.GetRequiredService<T>();
                    db.Database.Migrate();
                    return true;
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, $"An error occurred while performing migration of the database context {typeof(T).FullName}. ");
                    return false;
                }
            }

            //return webHost;
        }
        */

        /*
        public static Boolean MigrateDatabase<T>(this IWebHost webHost) where T : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                // TODO: Implement check for current database version, targeted database version, and logging to indicate whether or not we're attempting a migration.
                try
                {
                    var db = services.GetRequiredService<T>();
                    db.Database.Migrate();
                    return true;
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, $"An error occurred while performing migration of the database context {typeof(T).FullName}. ");
                    return false;
                }
            }

            //return webHost;
        }
        */

        /*
        public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetRequiredService<T>();
                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }
            return host;
        }
        */





        /*
        public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                // TODO: Implement check for current database version, targeted database version, and logging to indicate whether or not we're attempting a migration.
                try
                {
                    var db = services.GetRequiredService<T>();
                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, $"An error occurred while performing migration of the database context {typeof(T).FullName}. ");
                }
            }

            return host;
        }
        */
    }
}
