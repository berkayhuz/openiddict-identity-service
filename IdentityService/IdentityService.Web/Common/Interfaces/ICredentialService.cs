// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region 🧠 Credential Service Interface (ICredentialService)

/*
 * Defines the contract for credential-related operations such as registration,
 * email confirmation, password reset, and authentication.
 */

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

internal interface ICredentialService<T>
{
    Task<bool> ValidateCredentials(T user, string password);
    Task<T?> FindByEmailAsync(string user);
    Task<T?> FindByIdAsync(string user);
    Task<T?> FindByUsernameAsync(string username);
    Task SignIn(T user);
    Task SignInAsync(T user, AuthenticationProperties properties, string authenticationMethod);
    Task<IdentityResult> RegisterAsync(T user, string password);
    Task<string> GenerateEmailConfirmationTokenAsync(T user);
    Task<IdentityResult> ConfirmEmailAsync(T user, string token);
    Task<string> GeneratePasswordResetTokenAsync(T user);
    Task<IdentityResult> ResetPasswordAsync(T user, string token, string newPassword);
    Task SignOutAsync();
    Task<string> GenerateChangeEmailTokenAsync(T user, string newEmail);
    Task<IdentityResult> ChangeEmailAsync(T user, string newEmail, string token);
}

#endregion