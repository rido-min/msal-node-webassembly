using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.TokenCacheProviders.InMemory;

namespace AgenticTokens;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAgenticTokenProvider(
        this IServiceCollection services,
        IConfiguration configuration,
        string scope,
        string configSectionName = "AzureAd")
    {
        services.AddHttpClient();
        services.AddTokenAcquisition();
        services.AddInMemoryTokenCaches();

        services.Configure<MicrosoftIdentityOptions>(configuration.GetSection(configSectionName));

        services.AddScoped<ITokenProvider>(sp =>
        {
            var authHeaderProvider = sp.GetRequiredService<Microsoft.Identity.Abstractions.IAuthorizationHeaderProvider>();
            return new TokenProvider(authHeaderProvider, scope);
        });

        return services;
    }
}
