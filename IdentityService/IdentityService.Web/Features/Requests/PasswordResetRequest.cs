// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region ✉️ PasswordResetRequest – Reset Token Request Input

/*
 * Represents the initial password reset request payload.
 * Used in the `/connect/password-reset-request` endpoint.
 * Triggers generation of a reset token and email delivery.
 */

namespace IdentityService.Web.Features.Requests;

/// <summary>
/// Incoming model for requesting a password reset link.
/// </summary>
internal record PasswordResetRequest(string Email);

#endregion
