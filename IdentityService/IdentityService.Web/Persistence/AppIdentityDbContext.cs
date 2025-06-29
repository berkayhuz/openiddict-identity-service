// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region 🗃️ AppIdentityDbContext – Identity + OpenIddict DbContext

/*
 * Application DbContext integrating:
 *   🔹 ASP.NET Core Identity (via IdentityDbContext<AppUser>)
 *   🔹 OpenIddict (via builder.UseOpenIddict())
 * 
 * Registers all default Identity + OpenIddict schema and mappings.
 * Used for authentication, authorization, and token management.
 */

using IdentityService.Web.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Web.Persistence;

internal class AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
    : IdentityDbContext<AppUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Enables OpenIddict schema integration
        builder.UseOpenIddict();
    }
}

#endregion
