using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FactorioProductionCells.Infrastructure.Persistence
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        //private const string ConnectionStringName = "NorthwindDatabase";
        //private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public TContext CreateDbContext(string[] args)
        {
            /*
            //TODO: Leaving this here in case we decide that we want to load connection strings and such via appsettings.json.
            var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}WebUI", Path.DirectorySeparatorChar);
            return Create(basePath, Environment.GetEnvironmentVariable(AspNetCoreEnvironment));
            */

            // TODO: "dotnet ef migrations add" is remarkably stupid, and believes that we need a connection string in order to generate a migration. Since this is a class library, we
            // won't be able to read our connection string from the environment at generation time, so it throws a fit here. Fortunately, this factory should only ever be used by
            // "ef migrations", so we can just trick it into thinking we have one (it won't accept null or empty string). There's probably a better way to do this.
            return Create(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING") ?? "X");
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        private TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"The provided connection string is null or empty.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return CreateNewInstance(optionsBuilder.Options);
        }
    }
}