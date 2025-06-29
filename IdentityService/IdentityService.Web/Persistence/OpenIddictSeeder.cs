// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region 🌱 OpenIddictSeeder – Default Client Registration

/*
 * Seeds initial OpenIddict clients into the database.
 * Ensures that the application has a default trusted client for token requests.
 * 
 * Registers:
 *   🔹 Client ID: authservice-client
 *   🔹 Grant Types: password, refresh_token
 *   🔹 Scopes: openid, profile, email, offline_access, api
 * 
 * Should be called on application startup if clients are not already present.
 */

using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace IdentityService.Web.Persistence;

internal static class OpenIddictSeeder
{
    internal static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var manager = serviceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        if (await manager.FindByClientIdAsync("authservice-client").ConfigureAwait(false) is null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "authservice-client",
                ClientSecret = "C78A11D0-4D8A-4B61-BB20-F6D937C6B7F4",
                DisplayName = "AuthService Client",

                Permissions =
                {
                    // Grant types and endpoints
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.Password,
                    Permissions.GrantTypes.RefreshToken,
                    Permissions.ResponseTypes.Token,

                    // Scopes
                    Permissions.Prefixes.Scope + Scopes.OpenId,
                    Permissions.Prefixes.Scope + Scopes.Profile,
                    Permissions.Prefixes.Scope + Scopes.Email,
                    Permissions.Prefixes.Scope + Scopes.OfflineAccess,
                    Permissions.Prefixes.Scope + "api"
                }
            }).ConfigureAwait(false);
        }
    }
}

#endregion
