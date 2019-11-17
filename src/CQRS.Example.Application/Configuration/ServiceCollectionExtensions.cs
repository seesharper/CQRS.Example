using CQRS.Example.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Example.Application.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationConfiguration = configuration.Get<ApplicationConfiguration>(c => c.BindNonPublicProperties = true);
            services.AddSingleton<ApplicationConfiguration>(applicationConfiguration);
            return services;
        }
    }
}