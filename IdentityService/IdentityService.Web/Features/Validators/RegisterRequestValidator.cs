// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region ✅ RegisterRequestValidator – FluentValidation Rules

/*
 * Validates the RegisterRequest model used in `/connect/register`.
 * 
 * Ensures that:
 *   🔹 Email is present and properly formatted
 *   🔹 Password meets strong password criteria
 *   🔹 FirstName and LastName are not empty
 */

using FluentValidation;
using IdentityService.Web.Features.Requests;

namespace IdentityService.Web.Features.Validators;

internal class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .ApplyStrongPasswordRules();

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.");
    }
}

#endregion
