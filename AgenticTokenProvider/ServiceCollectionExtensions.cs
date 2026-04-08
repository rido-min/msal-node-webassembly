using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.TokenCacheProviders.InMemory;

namespace AgenticTokens;

/// <summary>
/// DI registration helpers for the agentic token provider.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the agentic token provider and related services.
    /// </summary>
    public static IServiceCollection AddAgenticTokenProvider(
        this IServiceCollection services,
        string scope,
        string tenantId,
        string clientId,
        string clientSecret,
        string instance = "https://login.microsoftonline.com/")
    {
        services.AddLogging(logging =>
        {
            logging.SetMinimumLevel(LogLevel.Warning);
            logging.AddFilter("Microsoft.Identity.Web", LogLevel.Error);
        });
        services.AddHttpClient();
        services.AddTokenAcquisition(true);
        services.AddAgentIdentities();
        services.AddInMemoryTokenCaches();

        services.Configure<MicrosoftIdentityOptions>(ops =>
        {
            ops.Instance = instance;
            ops.TenantId = tenantId;
            ops.ClientId = clientId;
        });

        services.Configure<MicrosoftIdentityApplicationOptions>(ops =>
        {
            ops.Instance = instance;
            ops.TenantId = tenantId;
            ops.ClientId = clientId;
            ops.ClientCredentials =
            [
                new CredentialDescription()
                {
                    SourceType = CredentialSource.ClientSecret,
                    ClientSecret = clientSecret
                }
            ];
        });

        services.AddScoped<ITokenProvider>(sp =>
        {
            var authHeaderProvider = sp.GetRequiredService<IAuthorizationHeaderProvider>();
            return new TokenProvider(authHeaderProvider, scope);
        });

        return services;
    }
}
