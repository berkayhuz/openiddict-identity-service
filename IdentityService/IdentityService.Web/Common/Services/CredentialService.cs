#region 🔐 Credential Service Implementation

/*
 * Implements the ICredentialService using ASP.NET Core Identity's UserManager and SignInManager.
 * Used to manage authentication, user search, registration, token generation, and validation.
 */

using IdentityService.Web.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Web.Common.Services;
internal class CredentialService(
    UserManager<AppUser> _userManager,
    SignInManager<AppUser> _signInManager
) : ICredentialService<AppUser>
{
    public async Task<AppUser?> FindByEmailAsync(string user)
        => await _userManager.FindByEmailAsync(user).ConfigureAwait(false);

    public async Task<AppUser?> FindByIdAsync(string user)
        => await _userManager.FindByIdAsync(user).ConfigureAwait(false);

    public async Task<AppUser?> FindByUsernameAsync(string username)
        => await _userManager.FindByNameAsync(username).ConfigureAwait(false);

    public async Task<bool> ValidateCredentials(AppUser user, string password)
        => await _userManager.CheckPasswordAsync(user, password).ConfigureAwait(false);

    public Task SignIn(AppUser user)
        => _signInManager.SignInAsync(user, isPersistent: true);

    public Task SignInAsync(AppUser user, AuthenticationProperties properties, string? authenticationMethod = null)
        => _signInManager.SignInAsync(user, properties, authenticationMethod);

    public async Task<IdentityResult> RegisterAsync(AppUser user, string password)
        => await _userManager.CreateAsync(user, password).ConfigureAwait(false);

    public async Task<string> GenerateEmailConfirmationTokenAsync(AppUser user)
        => await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);

    public async Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token)
        => await _userManager.ConfirmEmailAsync(user, token).ConfigureAwait(false);

    public async Task<string> GeneratePasswordResetTokenAsync(AppUser user)
        => await _userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);

    public async Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword)
        => await _userManager.ResetPasswordAsync(user, token, newPassword).ConfigureAwait(false);

    public async Task SignOutAsync()
        => await _signInManager.SignOutAsync().ConfigureAwait(false);

    public async Task<string> GenerateChangeEmailTokenAsync(AppUser user, string newEmail)
        => await _userManager.GenerateChangeEmailTokenAsync(user, newEmail).ConfigureAwait(false);

    public async Task<IdentityResult> ChangeEmailAsync(AppUser user, string newEmail, string token)
        => await _userManager.ChangeEmailAsync(user, newEmail, token).ConfigureAwait(false);
}

#endregion