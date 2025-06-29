// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region 📝 UpdateUserInfoRequest – Profile Update Input Model

/*
 * Represents the request body for updating a user's personal information.
 * Used in the `/connect/update-user-info` endpoint.
 * Requires authorization.
 */

namespace IdentityService.Web.Features.Requests;

/// <summary>
/// Incoming model for updating the authenticated user's profile info.
/// </summary>
internal record class UpdateUserInfoRequest
{
    /// <summary>
    /// New first name to be updated.
    /// </summary>
    public string FirstName { get; init; } = default!;

    /// <summary>
    /// New last name to be updated.
    /// </summary>
    public string LastName { get; init; } = default!;
}

#endregion
