namespace dotnetHTTPLib;

using Microsoft.JavaScript.NodeApi;

[JSExport]
public static class TestHttps
{
    public static async Task<string> FetchAsync(string url)
    {
        using var client = new System.Net.Http.HttpClient();
        var result = await client.GetStringAsync(url);
        return $"OK: {result.Length} bytes";
    }
}
