using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ModUpdateScheduler
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                
                try
                {
                    var db = services.GetRequiredService<T>();

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
                    
                    logger.LogError(ex, $"An error occurred while performing migration of the database context {typeof(T).FullName}.");
                }
            }
            
            return host;
        }
    }
}
