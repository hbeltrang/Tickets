using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tickets.Application.Contracts.Identity;
using Tickets.Application.Contracts.Infrastructure;
using Tickets.Application.Models.Email;
using Tickets.Application.Models.ImageManagement;
using Tickets.Application.Models.Payment;
using Tickets.Application.Models.Token;
using Tickets.Application.Persistence;
using Tickets.Infrastructure.MessageImplementation;
using Tickets.Infrastructure.Repositories;
using Tickets.Infrastructure.Services.Auth;

namespace Tickets.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
                                                                    IConfiguration configuration
        )
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAuthService, AuthService>();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));

            return services;
        }

    }
}
