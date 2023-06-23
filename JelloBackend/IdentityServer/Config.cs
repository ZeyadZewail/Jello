using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new("user", "userAPI")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new()
            {
                ClientId = "client",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.Code,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "user" }
            }, 
            // Swagger client
            new()
            {
                ClientId = "api_swagger",
                ClientName = "Swagger UI for Sample API",
                ClientSecrets = { new Secret("secret".Sha256()) }, // change me!

                AllowedGrantTypes = GrantTypes.Code,
                
                RedirectUris = { "https://localhost:7174/swagger/oauth2-redirect.html" },
                AllowedCorsOrigins = { "https://localhost:7174" },
                AllowedScopes = new List<string>
                {
                    "user"
                }
            }
        };
}