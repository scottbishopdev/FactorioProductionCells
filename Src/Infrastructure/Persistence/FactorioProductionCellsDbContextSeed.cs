using System;
using System.Linq;
using System.Threading.Tasks;
using FactorioProductionCells.Infrastructure.Identity;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Infrastructure.Persistence
{
    public static class FactorioProductionCellsDbContextSeed
    {   
        // Note: The application as a whole depends on this seed data. Also, Most things are supposed to interact with the database via the Application
        // layer, right? Finally, the only reason that the Infrastructure layer (specifically, the dbContext) needs to be dependent on the the CurrentUserService
        // is so it can populate the audit fields when data is saved, and the only thing mandating *that* dependency is the fact that our data seeding is done down
        // in the Infrastructure layer. Could we eliminate the whole DbContext -> CurrentUserService -> UserManager -> DbContext circular dependency issue by moving
        // both the data seeding and population of audit fields up to the Application layer?
        public static async Task<User> SeedAdministratorUser(IIdentityService identityService, Language preferredLanguage)
        {
            return await CreateUserIfNotExists(
                identityService: identityService,
                Id: new Guid(Environment.GetEnvironmentVariable("ADMINISTRATOR_ID")),
                UserName: Environment.GetEnvironmentVariable("ADMINISTRATOR_USERNAME"),
                Email: Environment.GetEnvironmentVariable("ADMINISTRATOR_EMAIL"),
                Password: Environment.GetEnvironmentVariable("ADMINISTRATOR_PASSWORD"),
                PreferredLanguage: preferredLanguage);
        }
        
        public static async Task<User> SeedModUpdateSchedulerUser(IIdentityService identityService, Language preferredLanguage)
        {
            String modUpdateSchedulerId = Environment.GetEnvironmentVariable("MODUPDATESCHEDULER_ID");
            Console.WriteLine($"ModUpdateScheduler Id will be: {modUpdateSchedulerId}");
            
            return await CreateUserIfNotExists(
                identityService: identityService,
                Id: new Guid(Environment.GetEnvironmentVariable("MODUPDATESCHEDULER_ID")),
                UserName: Environment.GetEnvironmentVariable("MODUPDATESCHEDULER_USERNAME"),
                Email: Environment.GetEnvironmentVariable("MODUPDATESCHEDULER_EMAIL"),
                Password: Environment.GetEnvironmentVariable("MODUPDATESCHEDULER_PASSWORD"),
                PreferredLanguage: preferredLanguage);
        }

        public static async Task<User> SeedModUpdateWorkerUser(IIdentityService identityService, Language preferredLanguage)
        {
            return await CreateUserIfNotExists(
                identityService: identityService,
                Id: new Guid(Environment.GetEnvironmentVariable("MODUPDATEWORKER_ID")),
                UserName: Environment.GetEnvironmentVariable("MODUPDATEWORKER_USERNAME"),
                Email: Environment.GetEnvironmentVariable("MODUPDATEWORKER_EMAIL"),
                Password: Environment.GetEnvironmentVariable("MODUPDATEWORKER_PASSWORD"),
                PreferredLanguage: preferredLanguage);
        }

        private static async Task<User> CreateUserIfNotExists(IIdentityService identityService, Guid Id, String UserName, String Email, String Password, Language PreferredLanguage)
        {
            var result = await identityService.GetUserFromUserNameAsync(UserName);

            if (result == null)
            {
                var createResult = await identityService.CreateUserAsync(Id, UserName, Email, Password, PreferredLanguage);

                if (createResult.Result.Succeeded)
                {
                    // TODO: This cast feels a little dirty, is currently necessary since our User entity is defined in the Infrastructure layer and not the Domain layer.
                    return (User)createResult.User;
                }
                else
                {
                    // If we somehow got here, we don't have any user data to return, so I think an exception is warranted (especially considering that the
                    // user in question is a requirement for the system to operate).
                    throw new InvalidOperationException($"The following errors occurred while attempting to create the user {UserName}:\n {String.Join("\n", createResult.Result.Errors)}");
                }
            }
            else
            {
                return (User)result;
            }
        }

        public static async Task<Language> SeedDefaultLanguageAsync(IFactorioProductionCellsDbContext dbContext)
        {
            if (!dbContext.Languages.Any())
            {
                var defaultLanguage = new Language("English", "en-us", true);

                dbContext.Languages.Add(defaultLanguage);

                await dbContext.SaveChangesAsync();

                return defaultLanguage;
            }

            return (Language)null;
        }
    }
}
