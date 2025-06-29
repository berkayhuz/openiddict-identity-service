// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region ✅ ChangeEmailRequestValidator – FluentValidation Rules

/*
 * Validates the ChangeEmailRequest input model.
 * Ensures that:
 *   🔹 NewEmail is not empty
 *   🔹 NewEmail is a valid email format
 * 
 * Used by FluentValidation pipeline during `/connect/change-email` requests.
 */

using FluentValidation;
using IdentityService.Web.Features.Requests;

namespace IdentityService.Web.Features.Validators;

internal class ChangeEmailRequestValidator : AbstractValidator<ChangeEmailRequest>
{
    public ChangeEmailRequestValidator()
    {
        RuleFor(x => x.NewEmail)
            .NotEmpty().WithMessage("New email must not be empty.")
            .EmailAddress().WithMessage("New email must be a valid email address.");
    }
}

#endregion
