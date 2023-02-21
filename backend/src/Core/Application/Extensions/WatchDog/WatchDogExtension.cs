using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDog;
using WatchDog.src.Enums;

namespace Tickets.Application.Extensions.WatchDog
{
    public static class WatchDogExtension
    {
        public static IServiceCollection AddWatchDog(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWatchDogServices(options =>
            {
                var watchDogAutoClear = configuration.GetValue<string>("WatchDogAutoClear");
                
                options.SetExternalDbConnString = configuration.GetConnectionString("DefaultConnection");
                options.DbDriverOption = WatchDogDbDriverEnum.MSSQL;
                options.IsAutoClear = true;

                switch (watchDogAutoClear)
                {
                    case "1":
                        {
                            options.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Daily;
                            break;
                        }
                    case "2":
                        {
                            options.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Weekly;
                            break;
                        }
                    case "3":
                        {
                            options.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Monthly;
                            break;
                        }
                    default:
                        {
                            options.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Weekly;
                            break;
                        }
                }

            });

            return services;
        }
    }
}
