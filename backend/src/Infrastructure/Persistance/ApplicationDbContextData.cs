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
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory
        )
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole(Role.APIADMIN));
                    await roleManager.CreateAsync(new IdentityRole(Role.ADMIN));
                    await roleManager.CreateAsync(new IdentityRole(Role.USER));
                    await roleManager.CreateAsync(new IdentityRole(Role.PROMOTER));
                }

                if (!userManager.Users.Any())
                {
                    var userAdmin = new ApplicationUser
                    {
                        Name = "Heri",
                        LastName = "Admin",
                        Email = "hbeltrang@gmail.com",
                        UserName = "hbeltrang",
                        Phone = "",
                        AvatarUrl = "",
                    };
                    await userManager.CreateAsync(userAdmin, "Heri123$");
                    await userManager.AddToRoleAsync(userAdmin, Role.ADMIN);

                    var userAdminApi = new ApplicationUser
                    {
                        Name = "Admin",
                        LastName = "Api",
                        Email = "adminapi@gmail.com",
                        UserName = "adminapi",
                        Phone = "",
                        AvatarUrl = "",
                    };
                    await userManager.CreateAsync(userAdminApi, "AdminApi123$");
                    await userManager.AddToRoleAsync(userAdminApi, Role.APIADMIN);


                    var user = new ApplicationUser
                    {
                        Name = "User",
                        LastName = "Test",
                        Email = "usertest@gmail.com",
                        UserName = "usertest",
                        Phone = "",
                        AvatarUrl = "",
                    };
                    await userManager.CreateAsync(user, "User123$");
                    await userManager.AddToRoleAsync(user, Role.USER);

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
                    var countryData = File.ReadAllText("../Infrastructure/Data/country.json");
                    var countries = JsonConvert.DeserializeObject<List<Country>>(countryData);
                    await context.Countries!.AddRangeAsync(countries!);
                    await context.SaveChangesAsync();
                }

                if (!context.States!.Any())
                {
                    var stateData = File.ReadAllText("../Infrastructure/Data/state.json");
                    var states = JsonConvert.DeserializeObject<List<State>>(stateData);
                    await context.States!.AddRangeAsync(states!);
                    await context.SaveChangesAsync();
                }

                if (!context.Cities!.Any())
                {
                    var cityData = File.ReadAllText("../Infrastructure/Data/city.json");
                    var cities = JsonConvert.DeserializeObject<List<City>>(cityData);
                    await context.Cities!.AddRangeAsync(cities!);
                    await context.SaveChangesAsync();
                }

                if (!context.Taxes!.Any())
                {
                    var taxData = File.ReadAllText("../Infrastructure/Data/tax.json");
                    var taxes = JsonConvert.DeserializeObject<List<Tax>>(taxData);
                    await context.Taxes!.AddRangeAsync(taxes!);
                    await context.SaveChangesAsync();
                }

                if (!context.Terms!.Any())
                {
                    var taxData = File.ReadAllText("../Infrastructure/Data/term.json");
                    var taxes = JsonConvert.DeserializeObject<List<Tax>>(taxData);
                    await context.Taxes!.AddRangeAsync(taxes!);
                    await context.SaveChangesAsync();
                }

                if (!context.Privacys!.Any())
                {
                    var taxData = File.ReadAllText("../Infrastructure/Data/privacypolicy.json");
                    var taxes = JsonConvert.DeserializeObject<List<Tax>>(taxData);
                    await context.Taxes!.AddRangeAsync(taxes!);
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
