using AgenticTokens;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
.AddUserSecrets<Program>()
.Build();

const string tenantId = "3f3d1cea-7a18-41af-872b-cfbbd5140984";
const string clientId = "f7c0e80b-b00e-48ac-8b57-363a2a9a7cda";
string clientSecret = config["AZURE_CLIENT_SECRET"] ?? throw new InvalidOperationException("Client secret not found in user secrets.");
const string agentIdentityId = "200fc45d-59f2-4260-9fd2-5413171ddcdb";
const string agentUserOid = "6037c803-c89a-4788-afae-8c9d24836a6e";
const string scope = "https://graph.microsoft.com/.default";

var tokenProvider = TokenProviderFactory.Create(tenantId, clientId, clientSecret, scope);
var token = await tokenProvider.GetTokenAsync(agentIdentityId, agentUserOid);
Console.WriteLine(token);