// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region 🔐 ChangePasswordRequest – Password Update Input Model

/*
 * Represents the request body for changing the currently authenticated user's password.
 * Used in the `/connect/change-password` endpoint.
 */

namespace IdentityService.Web.Features.Requests;

/// <summary>
/// Incoming model for requesting a password change.
/// </summary>
internal record class ChangePasswordRequest
{
    /// <summary>
    /// The user's current password for verification.
    /// </summary>
    public string CurrentPassword { get; init; } = default!;

    /// <summary>
    /// The new password the user wants to set.
    /// </summary>
    public string NewPassword { get; init; } = default!;
}

#endregion
