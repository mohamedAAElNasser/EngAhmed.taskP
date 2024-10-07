using EngAhmed.TaskP.TaskIdentity.Extends;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace EngAhmed.TaskP.TaskIdentity.Seedings
{
    public class SeedAdminUser
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole { Id=Guid.NewGuid().ToString(),Name="Admin" });
            }

            var adminUser = await userManager.FindByEmailAsync("admin@admin.com");
            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@outlook.com",
                    EmailConfirmed = true,
                };

                var result = await userManager.CreateAsync(user, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
