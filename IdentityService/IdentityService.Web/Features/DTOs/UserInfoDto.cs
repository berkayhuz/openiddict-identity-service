// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region 📤 UserInfoDto – User Profile Response DTO

/*
 * Represents a simplified snapshot of user information for profile retrieval APIs.
 * Used in authenticated responses such as `/connect/user-info`.
 */

namespace IdentityService.Web.Features.DTOs;

/// <summary>
/// Lightweight DTO for returning basic user information.
/// </summary>
internal record UserInfoDto(
    string FirstName,
    string LastName,
    string Email,
    string UserName
);

#endregion
