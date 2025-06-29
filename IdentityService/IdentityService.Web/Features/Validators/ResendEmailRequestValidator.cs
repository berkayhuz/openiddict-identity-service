// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region ✅ ResendEmailRequestValidator – FluentValidation Rules

/*
 * Validates the ResendEmailRequest model used in `/connect/resend-confirmation`.
 * 
 * Ensures that:
 *   🔹 Email is provided
 *   🔹 Email has a valid format
 */

using FluentValidation;
using IdentityService.Web.Features.Requests;

namespace IdentityService.Web.Features.Validators;

internal class ResendEmailRequestValidator : AbstractValidator<ResendEmailRequest>
{
    public ResendEmailRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}

#endregion
