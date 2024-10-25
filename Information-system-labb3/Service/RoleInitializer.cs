using Microsoft.AspNetCore.Identity;

namespace Information_system_labb3.Service
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Admin", "Employee" };
            IdentityResult roleResult;

            // Ensure roles exist
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create admin user if it doesn't exist
            string adminEmail = "admin@example.com";
            string adminPassword = "Admin@123";

            if (userManager.Users.All(u => u.Email != adminEmail))
            {
                var adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    // Assign the Admin role to the user
                    await userManager.AddToRoleAsync(adminUser, "Admin");

                    // Add the Admin claim to the user
                    await userManager.AddClaimAsync(adminUser, new System.Security.Claims.Claim("Admin", "true"));
                }
            }
        }
    }
}
