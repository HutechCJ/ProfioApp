using IdentityServer4.Models;

namespace IdentityServer;

public static class Config
{
  public static IEnumerable<IdentityResource> IdentityResources =>
    new IdentityResource[]
    {
      new IdentityResources.OpenId(),
      new IdentityResources.Profile(),
    };

  public static IEnumerable<ApiScope> ApiScopes =>
    new ApiScope[]
    {
      new("Read", "Read Access"),
      new("Write", "Write Access"),
    };

  public static IEnumerable<Client> Clients =>
    new Client[]
    {
      new()
      {
        ClientId = "m2m.client",
        ClientName = "Client Credentials Client",

        AllowedGrantTypes = GrantTypes.ClientCredentials,
        ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

        AllowedScopes = { "Read", "Write" }
      }
    };
}
