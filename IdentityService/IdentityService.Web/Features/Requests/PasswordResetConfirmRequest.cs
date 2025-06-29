// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region 🔁 PasswordResetConfirmRequest – Password Reset Final Step Input

/*
 * Represents the request body for confirming a password reset using a valid token.
 * Used in the `/connect/password-reset-confirm` endpoint.
 * Requires:
 *   🔹 UserId – identifies the user
 *   🔹 Token – password reset token sent via email
 *   🔹 NewPassword – new password to be set
 */

namespace IdentityService.Web.Features.Requests;

/// <summary>
/// Incoming model for confirming a password reset.
/// </summary>
internal record class PasswordResetConfirmRequest
{
    /// <summary>
    /// The unique identifier of the user requesting the password reset.
    /// </summary>
    public string UserId { get; init; } = default!;

    /// <summary>
    /// The reset token that was sent to the user's email.
    /// </summary>
    public string Token { get; init; } = default!;

    /// <summary>
    /// The new password to set for the user.
    /// </summary>
    public string NewPassword { get; init; } = default!;
}

#endregion
