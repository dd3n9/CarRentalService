using Asp.Versioning;
using CarRentalService.Api.Infrastructure;
using Mapster;
using MapsterMapper;
using System.Reflection;

namespace CarRentalService.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddVersionApiSetup();
            services.AddMappings();

            return services;
        }

        private static void AddVersionApiSetup(this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ReportApiVersions = true;
                o.ApiVersionReader = ApiVersionReader.Combine(
                        new QueryStringApiVersionReader("api-version"),
                        new HeaderApiVersionReader("X-Version"),
                        new MediaTypeApiVersionReader("ver")
                    );
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VW";
                options.SubstituteApiVersionInUrl = true;
            });
        }
        private static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            return services;
        }

    }
}
