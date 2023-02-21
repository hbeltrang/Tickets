using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Tickets.Api.Middlewares;
using Tickets.Application;
using Tickets.Application.Contracts.Infrastructure;
using Tickets.Application.Features.Countries.Queries.GetCountryList;
using Tickets.Domain;
using Tickets.Infrastructure;
using Tickets.Infrastructure.ImageCloudinary;
using Tickets.Infrastructure.Persistance;
using WatchDog;
using static Org.BouncyCastle.Math.EC.ECCurve;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Infrastructure - InfrastructureServiceRegistration, servicios a agregar
builder.Services.AddInfrastructureServices(builder.Configuration);

//Application - ApplicationServiceRegistration, servicios a agregar
builder.Services.AddApplicationServices(builder.Configuration);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
    )
);

builder.Services.AddMediatR(typeof(GetCountryListQueryHandler).GetTypeInfo().Assembly);

builder.Services.AddScoped<IManageImageService, ManageImageService>();


//builder.Services.AddControllers();

//todos los controladores tengan seguridad/autenticacion
builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
}).AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//admin de usuarios
IdentityBuilder identityBuilder = builder.Services.AddIdentityCore<ApplicationUser>();
identityBuilder = new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services);

identityBuilder.AddRoles<IdentityRole>().AddDefaultTokenProviders();
identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>>();

identityBuilder.AddEntityFrameworkStores<ApplicationDbContext>();
identityBuilder.AddSignInManager<SignInManager<ApplicationUser>>();

builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

//Tokens
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(builder.Configuration["CorsSettings:CorsPolicy"]!, builder => builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//middleware
app.UseMiddleware<ExceptionMiddleware>();

//authentication
app.UseAuthentication();

app.UseAuthorization();

//cors
app.UseCors(builder.Configuration["CorsSettings:CorsPolicy"]!);

app.MapControllers();

//watchdog
app.UseWatchDogExceptionLogger();
app.UseWatchDog(configuration =>
{
    configuration.WatchPageUsername = builder.Configuration["WatchDogSettings:UserAdmin"];
    configuration.WatchPagePassword = builder.Configuration["WatchDogSettings:PwdAdmin"];
}
);

//carga informacion inicial
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = service.GetRequiredService<ApplicationDbContext>();
        var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
        await context.Database.MigrateAsync();
        await ApplicationDbContextData.LoadDataAsync(context, userManager, roleManager, loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Error in the migration");
    }
}


app.Run();
