using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;
using System.Security.Claims;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
    {
        new ApiResource("catalogAPI", "Catalog API")
        {
            Scopes = { "API" },
            UserClaims = { "role" }
        },
        new ApiResource("cartAPI", "Cart API")
        {
            Scopes = { "API" },
            UserClaims = { "role" }
        },
    };

    public static IEnumerable<ApiScope> ApiScopes =>
        [
            new ApiScope("API", "API")   
            {
                UserClaims = { "role" }
            }
        ];

    public static IEnumerable<Client> Clients
        => new List<Client>
        {
            new Client
            {
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "API" }
            },
            new Client
            {
                ClientId = "ro.client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = {"API"},
            },
        };

    public static List<TestUser> GetUsers()
    {
        return new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "manager",
                Password = "password",
                Claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Role, "Manager")
                },
            },
            new TestUser
            {
                SubjectId = "2",
                Username = "buyer",
                Password = "password",
                Claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Role, "Buyer")
                }
            }
        };
    }
}