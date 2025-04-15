using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Kratos.Api.Database;
using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Startup;

public static class DatabaseInitializer
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var database = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        await database.Database.EnsureCreatedAsync();
        await CreateDefaultRolesAsync(database, roleManager);
        await CreateDefaultUsersAsync(database, userManager);
    }

    public static async Task CreateDefaultRolesAsync(DatabaseContext database, RoleManager<Role> roleManager)
    {
        if (await database.Roles.AnyAsync())
        {
            return;
        }

        Role[] roles = [
            new() { Name = Common.Constants.Role.Admin.Name },
            new() { Name = Common.Constants.Role.User.Name },
        ];

        foreach (Role role in roles)
        {
            await roleManager.CreateAsync(role);
        }
    }

    public static async Task CreateDefaultUsersAsync(DatabaseContext database, UserManager<User> userManager)
    {
        if (await database.Users.AnyAsync())
        {
            return;
        }

        await CreateDefaultAdminUsers(database, userManager);
        await CreateDefaultNormalUsers(database, userManager);
    }

    private static async Task CreateDefaultAdminUsers(DatabaseContext database, UserManager<User> userManager)
    {
        // TODO: Make this data driven
        Dictionary<string, string> defaultAdmins = new()
        {
            ["admin@gmail.com"] = "Admin@1234",
        };

        foreach (KeyValuePair<string, string> defaultAdmin in defaultAdmins)
        {
            User user = new()
            {
                UserName = defaultAdmin.Key,
                Email = defaultAdmin.Key,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            string password = defaultAdmin.Value;
            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, Common.Constants.Role.Admin.Name);

            await database.UserClaims.AddAsync(new()
            {
                ClaimType = "Default",
                ClaimValue = Common.Constants.Permission.DefautlAdmin.Name,
                UserId = user.Id
            });
            await database.SaveChangesAsync();
        }
    }

    private static async Task CreateDefaultNormalUsers(DatabaseContext database, UserManager<User> userManager)
    {
        // TODO: Make this data driven
        Dictionary<string, string> defaultUsers = new()
        {
            ["user1@gmail.com"] = "User1@1234",
        };

        foreach (KeyValuePair<string, string> defaultUser in defaultUsers)
        {
            User user = new()
            {
                UserName = defaultUser.Key,
                Email = defaultUser.Key,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            string password = defaultUser.Value;
            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, Common.Constants.Role.User.Name);

            await database.UserClaims.AddAsync(new()
            {
                ClaimType = "Default",
                ClaimValue = Common.Constants.Permission.DefaultUser.Name,
                UserId = user.Id
            });
            await database.SaveChangesAsync();
        }
    }
}
