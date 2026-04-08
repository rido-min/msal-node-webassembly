namespace AgenticTokens;

using Microsoft.JavaScript.NodeApi;

/// <summary>
/// Provides HTTP/HTTPS connectivity testing from within node-api-dotnet.
/// </summary>
[JSExport]
public static class TestHttps
{
    /// <summary>
    /// Fetches the content at the specified URL and returns a summary.
    /// </summary>
    public static async Task<string> FetchAsync(string url)
    {
        using var client = new System.Net.Http.HttpClient();
        var result = await client.GetStringAsync(url);
        return $"OK: {result.Length} bytes";
    }
}
