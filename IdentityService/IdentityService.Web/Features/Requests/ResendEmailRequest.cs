// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region 🔁 ResendEmailRequest – Email Confirmation Resend Input

/*
 * Represents the request to resend the email confirmation link.
 * Used in the `/connect/resend-confirmation` endpoint.
 * 
 * Typically used when:
 *   🔹 User didn't receive the confirmation email
 *   🔹 Token expired or was lost
 */

namespace IdentityService.Web.Features.Requests;

/// <summary>
/// Incoming model for requesting a new confirmation email.
/// </summary>
internal record ResendEmailRequest(string Email);

#endregion
