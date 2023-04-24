using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace IdentityProvider;

public static class Config
{

    public const string Admin = "admin";
    public const string Customer = "customer";


    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        
            new IdentityResource ("roles","your roles(s)",
                                  new []{"role"})
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
       new ApiResource("api1", "MyAPIResource",new [] {"role"})
       {
           Scopes = { "loza.fullaccess" }
       }
        };


    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
               new ApiScope(name: "loza", displayName: "Full Access loza api"),
               new ApiScope(name: "loza.read", displayName: "read loza data"),
               new ApiScope(name: "loza.write", displayName: "write loza data"),
               new ApiScope(name: "loza.delete", displayName: "delete loza data")
            };


    public static IEnumerable<Client> Clients =>
        new Client[]
            {
                new Client(){
                ClientName = "client",
                ClientId = "client",

                 AllowedGrantTypes = GrantTypes.ClientCredentials,

                    RedirectUris =
                    {
                         "https://localhost:6001/signin-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                       JwtClaimTypes.Role,
                        "roles",
                        "loza"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RequireConsent=true,


                },
                 new Client(){
                ClientName = "loza api",
                ClientId = "loza",

                 AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris =
                    {
                         "https://localhost:6001/signin-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                         IdentityServerConstants.StandardScopes.Email,
                        "roles",
                        "loza"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RequireConsent=true,


                }
            };
}