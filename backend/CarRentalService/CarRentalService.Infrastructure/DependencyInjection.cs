using CarRentalService.Application.Common.Interfaces.ReadServices;
using CarRentalService.Domain.Repositories;
using CarRentalService.Infrastructure.EF.Context;
using CarRentalService.Infrastructure.EF.Repositories;
using CarRentalService.Infrastructure.EF.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMsSql(configuration);

            //DI Vehicle
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleReadService, VehicleReadService>();

            //DI User
            services.AddScoped<IUserRepository, UserRepository>();

            //DI Rental
            services.AddScoped<IRentalPointRepository, RentalPointRepository>();

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
    }
}
