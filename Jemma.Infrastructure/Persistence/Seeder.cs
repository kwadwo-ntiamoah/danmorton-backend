using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Jemma.Infrastructure.Persistence
{
    // public class Seeder
    // {
    //     public static class IdentitySeeder
    //     {
    //         public static async Task CreateDefaultUsers(UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager, AppDbContext context, IConfiguration config)
    //         {

    //             //define roles
    //             var roles = new[] { "Administrator", "Staff" };

    //             // Get the list of the roles from the enum
    //             //Role[] roles = (Role[])Enum.GetValues(typeof(Role));
    //             foreach (var role in roles)
    //             {
    //                 // Create an identity role object out of the enum value
    //                 var identityRole = new IdentityRole
    //                 {
    //                     Name = role
    //                 };

    //                 // Create the role if it doesn't already exist
    //                 if (!await _roleManager.RoleExistsAsync(roleName: identityRole.Name))
    //                 {
    //                     var result = await _roleManager.CreateAsync(identityRole);

    //                     if (!result.Succeeded)
    //                         throw new Exception(string.Join(".", result.Errors.Select(x => x.Description)));
    //                 }
    //             }

    //             //Admin user
    //             var hasher = new PasswordHasher<User>();

    //             User adminUser = User.Create(
    //                 email: "admin@admin.com",
    //                 otherNames: "Kwadwo",
    //                 lastName: "Thompson"
    //             );

    //             // Add the admin user to the database if he doesn't exist
    //             if (await _userManager.FindByEmailAsync(adminUser.Email!) == null)
    //             {
    //                 var result = await _userManager.CreateAsync(adminUser, "Http404#k");

    //                 if (!result.Succeeded) // Return 500 if it fails
    //                     throw new Exception(string.Join(".", result.Errors.Select(x => x.Description)));

    //                 // Assign All Roles to Admin User
    //                 result = await _userManager.AddToRolesAsync(adminUser, roles);

    //                 if (!result.Succeeded)
    //                     throw new Exception(string.Join(".", result.Errors.Select(x => x.Description)));

    //             }

    //         }
    //     }
    // }
}