using System;
using Maria_Sons.Constants;
using Maria_Sons.Models;
using Microsoft.AspNetCore.Identity;


namespace Maria_Sons.Data
{
	public static class DbSeeder
	{

        public static async Task SeedRolesAndUserAsync(IServiceProvider service)
        {
            //Seed Roles
            var userManager = service.GetService<UserManager<IdentityUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.CEO.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.RECEPTIONIST.ToString()));

            // creating admin

            var reception = new IdentityUser
            {
                UserName = "reception@gmail.com",
                Email = "repeption@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userInDb = await userManager.FindByEmailAsync(reception.Email);
            if (userInDb == null)
            {
                await userManager.CreateAsync(reception, "Recep@123");
                await userManager.AddToRoleAsync(reception, Roles.RECEPTIONIST.ToString());
            }

        }

        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            //Seed Roles
            var userManager = service.GetService<UserManager<IdentityUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.CEO.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.RECEPTIONIST.ToString()));

            // creating admin

            var ceo = new IdentityUser
            {
                UserName = "ceo@gmail.com",
                Email = "ceo@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userInDb = await userManager.FindByEmailAsync(ceo.Email);
            if (userInDb == null)
            {
                await userManager.CreateAsync(ceo, "Admin@123");
                await userManager.AddToRoleAsync(ceo, Roles.CEO.ToString());
            }

        }




    }

}

