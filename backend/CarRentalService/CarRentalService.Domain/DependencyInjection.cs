using CarRentalService.Domain.Services;
using CarRentalService.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalService.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IVehicleDomainService, VehicleDomainService>();

            return services;
        }
    }
}
