using Microsoft.JavaScript.NodeApi;

namespace AgenticTokens;

/// <summary>
/// Provides agentic identity tokens.
/// </summary>
[JSExport]
public interface ITokenProvider
{
    /// <summary>
    /// Gets a token for the specified agentic app and user.
    /// </summary>
    /// <param name="agenticAppId">The agentic application ID.</param>
    /// <param name="agenticUserId">The agentic user ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The authorization header token string.</returns>
    Task<string> GetTokenAsync(
        string agenticAppId,
        string agenticUserId,
        CancellationToken cancellationToken = default);
}
