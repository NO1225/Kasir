﻿using Kasir.Domain.Entities;
using Kasir.Domain.Enums;
using Kasir.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kasir.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>();

            foreach (var role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role.ToString());
                if (roleExists == false)
                {
                    await roleManager.CreateAsync(new IdentityRole() { Name = role.ToString() });
                }
            }

            // Ensure admin user exist
            var adminUser = await userManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                adminUser = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "admin@gmail.com"
                };
                await userManager.CreateAsync(adminUser, "admin123");
            }

            var userRoles = await userManager.GetRolesAsync(adminUser);
            if (userRoles.Contains(UserRole.Admin.ToString()) == false)
            {
                await userManager.AddToRoleAsync(adminUser, UserRole.Admin.ToString());
            }

            // Ensure developer user exist
            var developerUser = await userManager.FindByNameAsync("developer");
            if (developerUser == null)
            {
                developerUser = new ApplicationUser()
                {
                    UserName = "developer",
                    Email = "developer@gmail.com"
                };
                await userManager.CreateAsync(developerUser, "developer123");
            }

            userRoles = await userManager.GetRolesAsync(developerUser);
            if (userRoles.Contains(UserRole.Developer.ToString()) == false)
            {
                await userManager.AddToRoleAsync(developerUser, UserRole.Developer.ToString());
            }



            //var administratorRole = new IdentityRole("Administrator");

            //if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            //{
            //    await roleManager.CreateAsync(administratorRole);
            //}

            //var defaultUser = new ApplicationUser { UserName = "iayti", Email = "test@test.com" };

            //if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            //{
            //    await userManager.CreateAsync(defaultUser, "Matech_1850");
            //    await userManager.AddToRolesAsync(defaultUser, new[] { administratorRole.Name });
            //}
        }

        public static async Task SeedSampleCityDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Languages.Any())
            {
                context.Languages.AddRange(new Language
                {
                    Name = "en",
                    ImageName = "en.png"
                }, new Language
                {
                    Name = "ar",
                    ImageName = "ar.png"
                }, new Language
                {
                    Name = "sp",
                    ImageName = "sp.png"
                }, new Language
                {
                    Name = "fr",
                    ImageName = "fr.png"
                }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
