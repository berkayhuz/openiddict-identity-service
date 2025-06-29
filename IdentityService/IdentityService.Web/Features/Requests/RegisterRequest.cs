// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region 📝 RegisterRequest – User Registration Input Model

/*
 * Represents the incoming data required to register a new user.
 * Used in the `/connect/register` endpoint.
 * 
 * Fields:
 *   🔹 FirstName – user's first name
 *   🔹 LastName – user's last name
 *   🔹 Email – used as both email and username
 *   🔹 Password – user-defined secure password
 */

namespace IdentityService.Web.Features.Requests;

/// <summary>
/// Incoming model for user registration.
/// </summary>
internal record class RegisterRequest
{
    /// <summary>
    /// The user's first name.
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// The user's last name.
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    /// The user's email address (used as username).
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// The user's desired password.
    /// </summary>
    public required string Password { get; init; }
}

#endregion
