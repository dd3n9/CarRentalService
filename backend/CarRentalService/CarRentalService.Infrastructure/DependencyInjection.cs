using CarRentalService.Application.Behaviors;
using CarRentalService.Application.Common.Interfaces.ReadServices;
using CarRentalService.Application.Common.Interfaces.Services;
using CarRentalService.Contracts.Configurations;
using CarRentalService.Domain.Repositories;
using CarRentalService.Domain.UserAggregate;
using CarRentalService.Domain.UserAggregate.ValueObjects;
using CarRentalService.Infrastructure.EF.Context;
using CarRentalService.Infrastructure.EF.Repositories;
using CarRentalService.Infrastructure.EF.Services;
using CarRentalService.Infrastructure.Services.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CarRentalService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole<UserId>>()
                .AddEntityFrameworkStores<WriteDbContext>()
                .AddDefaultTokenProviders();

            services.AddMsSql(configuration)
                .AddAuth(configuration);

            //MediatR
            services.AddMediatR(config =>
                config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            //DI Vehicle
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleReadService, VehicleReadService>();

            //DI User
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserReadService, UserReadService>();

            //DI Rental
            services.AddScoped<IRentalPointRepository, RentalPointRepository>();

            //Configuration
            services.Configure<CookiesConfig>(configuration.GetSection(CookiesConfig.SectionName));
            

            return services;
        }


        private static IServiceCollection AddMsSql(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ReadDbContext>(ctx =>
            {
                ctx.UseSqlServer(configuration.GetConnectionString("DbConnection"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddDbContext<WriteDbContext>(ctx =>
            {
                ctx.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });

            return services;
        }

        private static IServiceCollection AddAuth(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSettings = new JwtConfig();
            configuration.Bind(JwtConfig.SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserRoleService, UserRoleService>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret))
                });

            return services;
        }
    }
}
