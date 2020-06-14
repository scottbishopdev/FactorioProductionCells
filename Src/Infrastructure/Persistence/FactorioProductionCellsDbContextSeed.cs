using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using FactorioProductionCells.Infrastructure.Identity;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Infrastructure.Persistence
{
    public static class FactorioProductionCellsDbContextSeed
    {   
        public static async Task<NetCoreUser> SeedAdministratorUser(UserManager<NetCoreUser> userManager)
        {
            return await CreateUserIfNotExists(
                userManager: userManager,
                Id: new Guid(Environment.GetEnvironmentVariable("ADMINISTRATOR_ID")),
                UserName: Environment.GetEnvironmentVariable("ADMINISTRATOR_USERNAME"),
                Email: Environment.GetEnvironmentVariable("ADMINISTRATOR_EMAIL"),
                Password: Environment.GetEnvironmentVariable("ADMINISTRATOR_PASSWORD"));
        }
        
        public static async Task<NetCoreUser> SeedModUpdateSchedulerUser(UserManager<NetCoreUser> userManager)
        {
            return await CreateUserIfNotExists(
                userManager: userManager,
                Id: new Guid(Environment.GetEnvironmentVariable("MODUPDATESCHEDULER_ID")),
                UserName: Environment.GetEnvironmentVariable("MODUPDATESCHEDULER_USERNAME"),
                Email: Environment.GetEnvironmentVariable("MODUPDATESCHEDULER_EMAIL"),
                Password: Environment.GetEnvironmentVariable("MODUPDATESCHEDULER_PASSWORD"));
        }

        public static async Task<NetCoreUser> SeedModUpdateWorkerUser(UserManager<NetCoreUser> userManager)
        {
            return await CreateUserIfNotExists(
                userManager: userManager,
                Id: new Guid(Environment.GetEnvironmentVariable("MODUPDATEWORKER_ID")),
                UserName: Environment.GetEnvironmentVariable("MODUPDATEWORKER_USERNAME"),
                Email: Environment.GetEnvironmentVariable("MODUPDATEWORKER_EMAIL"),
                Password: Environment.GetEnvironmentVariable("MODUPDATEWORKER_PASSWORD"));
        }

        /*
        public static async Task SeedDefaultUsersAsync(UserManager<NetCoreUser> userManager)
        {
            await CreateUserIfNotExists(
                userManager: userManager,
                UserName: Environment.GetEnvironmentVariable("ADMINISTRATOR_USERNAME"),
                Email: Environment.GetEnvironmentVariable("ADMINISTRATOR_EMAIL"),
                Password: Environment.GetEnvironmentVariable("ADMINISTRATOR_PASSWORD"));

            await CreateUserIfNotExists(
                userManager: userManager,
                UserName: Environment.GetEnvironmentVariable("MODUPDATESCHEDULER_USERNAME"),
                Email: Environment.GetEnvironmentVariable("MODUPDATESCHEDULER_EMAIL"),
                Password: Environment.GetEnvironmentVariable("MODUPDATESCHEDULER_PASSWORD"));

            await CreateUserIfNotExists(
                userManager: userManager,
                UserName: Environment.GetEnvironmentVariable("MODUPDATEWORKER_USERNAME"),
                Email: Environment.GetEnvironmentVariable("MODUPDATEWORKER_EMAIL"),
                Password: Environment.GetEnvironmentVariable("MODUPDATEWORKER_PASSWORD"));
        }
        */

        private static async Task<NetCoreUser> CreateUserIfNotExists(UserManager<NetCoreUser> userManager, Guid Id, String UserName, String Email, String Password)
        {
            var user = await userManager.FindByNameAsync(UserName);
            
            if (user == null)
            {
                var newUser = new NetCoreUser
                {
                    Id = Id,
                    UserName = UserName,
                    Email = Email
                };

                var result = await userManager.CreateAsync(newUser, Password);

                if (result.Succeeded)
                {
                    // TODO: I really don't like how I'm doing this here. It feels like there should be some kind of CreateAsync method that returns the actual
                    // data for the user what was created (ala normal entity framework), but I can only find stuff that returns an IdentityResult.
                    return await userManager.FindByNameAsync(UserName);
                }
                else
                {
                    // TODO: If we somehow got here, we don't have any user data to return, so I think an exception is warranted (especially considering that the
                    // user in question is a requirement for the system to operate).
                    throw new InvalidOperationException($"The following errors occurred while attempting to create the user {UserName}:\n {String.Join("\n", result.Errors)}");
                }
            }
            else
            {
                return user;
            }
        }

        public static async Task SeedDefaultLanguageAsync(IFactorioProductionCellsDbContext dbContext)
        {
            if (!dbContext.Languages.Any())
            {
                dbContext.Languages.Add(new Language("English", "en-us", true));

                await dbContext.SaveChangesAsync();
            }
        }



        /*
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.TodoLists.Any())
            {
                context.TodoLists.Add(new TodoList
                {
                    Title = "Shopping",
                    Items =
                    {
                        new TodoItem { Title = "Apples", Done = true },
                        new TodoItem { Title = "Milk", Done = true },
                        new TodoItem { Title = "Bread", Done = true },
                        new TodoItem { Title = "Toilet paper" },
                        new TodoItem { Title = "Pasta" },
                        new TodoItem { Title = "Tissues" },
                        new TodoItem { Title = "Tuna" },
                        new TodoItem { Title = "Water" }
                    }
                });

                await context.SaveChangesAsync();
            }
        }
        */
    }
}
