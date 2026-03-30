using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.AgentIdentities;

namespace AgenticTokens;

internal class TokenProvider : ITokenProvider
{
    private readonly IAuthorizationHeaderProvider _authorizationHeaderProvider;
    private readonly string _scope;

    public TokenProvider(IAuthorizationHeaderProvider authorizationHeaderProvider, string scope)
    {
        _authorizationHeaderProvider = authorizationHeaderProvider;
        _scope = scope;
    }

    public async Task<string> GetTokenAsync(
        string agenticAppId,
        string agenticUserId,
        CancellationToken cancellationToken = default)
    {
        var options = new DownstreamApiOptions();
        options.WithAgentUserIdentity(agenticAppId, Guid.Parse(agenticUserId));

        string token = await _authorizationHeaderProvider
            .CreateAuthorizationHeaderAsync([_scope], options, null, cancellationToken);

        return token;
    }
}
