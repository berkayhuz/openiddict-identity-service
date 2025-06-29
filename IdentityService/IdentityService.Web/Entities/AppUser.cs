// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region 🧾 AppUser Entity – Identity User Model

/*
 * Represents the application-specific user entity extending ASP.NET IdentityUser.
 * Stores user profile data and metadata for identity-related operations.
 * 
 * Notes:
 * - Used by UserManager<AppUser> for identity operations
 * - Integrated with OpenIddict for claim generation and authentication
 */

using Microsoft.AspNetCore.Identity;

namespace IdentityService.Web.Entities;

internal class AppUser : IdentityUser
{
    /// <summary>
    /// User's first name – required during registration.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// User's last name – required during registration.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// The UTC datetime when the user account was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

#endregion
