using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Tickets.Application.Models.Authorization;
using Tickets.Domain;
using File = System.IO.File;

namespace Tickets.Infrastructure.Persistance
{
    public class ApplicationDbContextData
    {
        public static async Task LoadDataAsync(
            ApplicationDbContext context,
            UserManager<ApplicationUser> usuarioManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory
        )
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole(Role.ADMIN));
                    await roleManager.CreateAsync(new IdentityRole(Role.USER));
                }

                if (!usuarioManager.Users.Any())
                {
                    var usuario = new ApplicationUser
                    {
                        Name = "Heri",
                        LastName = "Admin",
                        Email = "hbeltrang@gmail.com",
                        UserName = "hbeltrang",
                        Phone = "",
                        AvatarUrl = "",
                    };
                    await usuarioManager.CreateAsync(usuario, "Heri123$");
                    await usuarioManager.AddToRoleAsync(usuario, Role.ADMIN);

                    var usuarioAdmin = new ApplicationUser
                    {
                        Name = "User",
                        LastName = "Test",
                        Email = "usertest@gmail.com",
                        UserName = "usertest",
                        Phone = "",
                        AvatarUrl = "",
                    };
                    await usuarioManager.CreateAsync(usuarioAdmin, "User123$");
                    await usuarioManager.AddToRoleAsync(usuarioAdmin, Role.USER);

                }

                if (!context.Categories!.Any())
                {
                    var categoryData = File.ReadAllText("../Infrastructure/Data/category.json");
                    var categories = JsonConvert.DeserializeObject<List<Category>>(categoryData);
                    await context.Categories!.AddRangeAsync(categories!);
                    await context.SaveChangesAsync();
                }

                if (!context.Countries!.Any())
                {
                    var countryData = File.ReadAllText("../Infrastructure/Data/countries.json");
                    var countries = JsonConvert.DeserializeObject<List<Country>>(countryData);
                    await context.Countries!.AddRangeAsync(countries!);
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<ApplicationDbContextData>();
                logger.LogError(e.Message);
            }

        }

    }
}
