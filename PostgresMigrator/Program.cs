using System;
using System.Linq;

using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;

using Microsoft.Extensions.DependencyInjection;

namespace PostgresMigrator
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServices();

            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING"))
                    // What is this line actually doing? We're pulling in migrations that aren't of this type, so what gives?
                    .ScanIn(typeof(Migrations.CreateInitialSchema).Assembly).For.Migrations()
                    )
                // TODO: Figure out wtf this line does.
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // TODO: Figure out wtf this line does.
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}