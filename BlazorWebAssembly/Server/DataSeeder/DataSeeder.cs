using BlazorWebAssembly.Server.Models;

using Microsoft.AspNetCore.Identity;

namespace BlazorWebAssembly.Server.DataSeeder
{
    public class DataSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeData()
        {
            // Create role
            string roleName = "Demo";
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // Create user
            ApplicationUser user = new ApplicationUser
            {
                UserName = "user1@gmail.com",
                Email = "user1@gmail.com",
                EmailConfirmed = true
            };
            string password = "Password!123";
            var userResult = await _userManager.CreateAsync(user, password);

            // Assign role to user
            if (userResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
        }
    }
}
