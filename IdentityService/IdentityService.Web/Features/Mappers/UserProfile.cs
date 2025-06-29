// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region 🔁 UserProfile – AutoMapper Configurations

/*
 * Defines object-to-object mapping rules for identity-related DTOs and entities.
 * Includes:
 *   🔹 RegisterRequest → AppUser (for registration)
 *   🔹 AppUser → UserInfoDto (for profile responses)
 * 
 * Used via dependency-injected IMapper during request/response transformations.
 */

using AutoMapper;
using IdentityService.Web.Entities;
using IdentityService.Web.Features.DTOs;
using IdentityService.Web.Features.Requests;

namespace IdentityService.Web.Features.Mappers;

internal class UserProfile : Profile
{
    public UserProfile()
    {
        // Register → AppUser mapping
        CreateMap<RegisterRequest, AppUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(_ => false));

        // AppUser → UserInfoDto mapping
        CreateMap<AppUser, UserInfoDto>();
    }
}

#endregion
