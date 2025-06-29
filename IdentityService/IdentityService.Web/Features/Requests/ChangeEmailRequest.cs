// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region 📥 ChangeEmailRequest – Change Email Input Model

/*
 * Represents the request body for initiating an email change process.
 * Used in the `/connect/change-email` endpoint.
 * A confirmation token will be sent to the new email before change is applied.
 */

namespace IdentityService.Web.Features.Requests;

/// <summary>
/// Incoming model for requesting an email change.
/// </summary>
internal record class ChangeEmailRequest
{
    /// <summary>
    /// The new email address the user wants to use.
    /// </summary>
    public string NewEmail { get; init; } = default!;
}

#endregion
