using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.JavaScript.NodeApi;

namespace AgenticTokens;

/// <summary>
/// Factory to create an <see cref="ITokenProvider"/> from Node.js.
/// </summary>
[JSExport]
public static class TokenProviderFactory
{
    /// <summary>
    /// Creates an <see cref="ITokenProvider"/> using the specified Azure AD configuration values and scope.
    /// </summary>
    /// <param name="tenantId">Azure AD tenant ID.</param>
    /// <param name="clientId">Azure AD client (application) ID for the agent blueprint.</param>
    /// <param name="clientSecret">Azure AD client secret.</param>
    /// <param name="scope">Target API scope (e.g. "api://target-app/.default").</param>
    /// <param name="instance">Azure AD instance URL. Defaults to "https://login.microsoftonline.com/".</param>
    /// <returns>An <see cref="ITokenProvider"/> instance.</returns>
    public static ITokenProvider Create(
        string tenantId,
        string clientId,
        string clientSecret,
        string scope,
        string? instance = null)
    {
        instance ??= "https://login.microsoftonline.com/";
        var builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
        {
            ContentRootPath = Path.GetDirectoryName(typeof(TokenProviderFactory).Assembly.Location)
                ?? Directory.GetCurrentDirectory(),
            Args = [],
        });

        builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["AzureAd:Instance"] = instance,
            ["AzureAd:TenantId"] = tenantId,
            ["AzureAd:ClientId"] = clientId,
            ["AzureAd:ClientSecret"] = clientSecret,
        });

        builder.Services.AddLogging();
        builder.Services.AddAgenticTokenProvider(builder.Configuration, scope);

        var host = builder.Build();
        var scope1 = host.Services.CreateScope();
        return scope1.ServiceProvider.GetRequiredService<ITokenProvider>();
    }

    /// <summary>
    /// Test HTTP connectivity from within node-api-dotnet.
    /// </summary>
    public static async Task<string> TestHttpAsync()
    {
        using var client = new System.Net.Http.HttpClient();
        var result = await client.GetStringAsync("https://httpbin.org/get");
        return $"OK: {result.Length} bytes";
    }
}
