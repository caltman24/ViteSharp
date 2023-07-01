using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViteSharp.Models;

namespace ViteSharp;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddViteApp(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<Dictionary<string, ViteAppConfig>>(config.GetRequiredSection("AspViteNet"));
        services.AddSingleton<IViteManifestParser, ViteManifestParser>();
        services.AddSingleton<IViteAppManager, ViteAppManager>();

        return services;
    }
}
