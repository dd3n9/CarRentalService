using Asp.Versioning;

namespace CarRentalService.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddVersionApiSetup();

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
    }
}
