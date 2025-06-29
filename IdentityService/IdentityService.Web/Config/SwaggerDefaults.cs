// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region 📑 Swagger Defaults Configuration

/*
 * Provides default OpenIddict-compliant OAuth scopes used in Swagger UI.
 * These scopes are required for proper authorization and identity claims mapping
 * when integrating with OpenIddict-secured endpoints.
 */

namespace IdentityService.Web.Config;

internal static class SwaggerDefaults
{
    public static readonly string[] OAuthScopes = [
        "api",             // Custom API scope
        "openid",          // Required for OpenID Connect authentication
        "email",           // Access to the user's email address
        "profile",         // Access to basic profile information (name, etc.)
        "offline_access"   // Enables refresh token support
    ];
}

#endregion
