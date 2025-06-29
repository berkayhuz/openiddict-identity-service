// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region 🔐 TokenController – Token Exchange Endpoints

/*
 * Handles OpenIddict token endpoint (`/connect/token`) requests.
 * Supports:
 *   🔹 Password Grant Type
 *   🔹 Refresh Token Grant Type
 * 
 * Produces OpenID Connect-compliant JWTs (access, ID, refresh tokens)
 * using validated Identity users and their claims.
 */

using IdentityService.Web.Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace IdentityService.Web.Controllers;

[ApiController]
[Route("connect")]
internal class TokenController(
    ICredentialService<AppUser> _credentialService) : ControllerBase
{
    #region 🔑 /connect/token – Exchange Endpoint

    /*
     * Handles token issuance for supported OpenIddict grant types:
     * - password
     * - refresh_token
     */
    [HttpPost("token"), Produces("application/json")]
    public async Task<IActionResult> Exchange()
    {
        var request = HttpContext.GetOpenIddictServerRequest();
        if (request is null)
            return BadRequest("Invalid token request.");

        #region 🧾 Password Grant Flow

        if (request.IsPasswordGrantType())
        {
            var email = request.Username ?? string.Empty;
            var password = request.Password ?? string.Empty;

            var user = await _credentialService.FindByEmailAsync(email).ConfigureAwait(false);
            if (user is null || !await _credentialService.ValidateCredentials(user, password).ConfigureAwait(false))
                return Forbid();

            if (!user.EmailConfirmed)
                return Forbid();

            var identity = new ClaimsIdentity(
                TokenValidationParameters.DefaultAuthenticationType,
                Claims.Name,
                Claims.Role
            );

            identity.AddClaim(Claims.Subject, user.Id ?? string.Empty);
            identity.AddClaim(Claims.Name, user.UserName ?? string.Empty);

            var principal = new ClaimsPrincipal(identity);
            principal.SetScopes(new[] { Scopes.OpenId, Scopes.Email, Scopes.Profile });

            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        #endregion

        #region 🔄 Refresh Token Grant Flow

        else if (request.IsRefreshTokenGrantType())
        {
            var result = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme).ConfigureAwait(false);
            var userId = result.Principal?.GetClaim(Claims.Subject);

            if (string.IsNullOrWhiteSpace(userId))
                return Forbid();

            var user = await _credentialService.FindByIdAsync(userId).ConfigureAwait(false);
            if (user is null)
                return Forbid();

            var identity = new ClaimsIdentity(
                TokenValidationParameters.DefaultAuthenticationType,
                Claims.Name,
                Claims.Role
            );

            identity.AddClaim(Claims.Subject, user.Id ?? string.Empty);
            identity.AddClaim(Claims.Name, user.UserName ?? string.Empty);

            var principal = new ClaimsPrincipal(identity);
            principal.SetScopes(result.Principal!.GetScopes());

            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        #endregion

        return BadRequest("Unsupported grant type.");
    }

    #endregion
}

#endregion
