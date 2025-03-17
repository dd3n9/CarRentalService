using CarRentalService.Infrastructure.EF.Context;
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
            return services;
        }


        private static IServiceCollection AddMsSql(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<ReadDbContext>(ctx =>
            //{
            //    ctx.UseSqlServer(configuration.GetConnectionString("DbConnection"))
            //        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            //});

            services.AddDbContext<WriteDbContext>(ctx =>
            {
                ctx.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });

            return services;
        }
    }
}
