// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region ✅ PasswordResetConfirmRequestValidator – FluentValidation Rules

/*
 * Validates the PasswordResetConfirmRequest model.
 * Used during password reset confirmation in `/connect/password-reset-confirm`.
 * 
 * Ensures that:
 *   🔹 UserId is provided
 *   🔹 Token is present (reset token from email)
 *   🔹 NewPassword meets strong password criteria
 */

using FluentValidation;
using IdentityService.Web.Features.Requests;

namespace IdentityService.Web.Features.Validators;

internal class PasswordResetConfirmRequestValidator : AbstractValidator<PasswordResetConfirmRequest>
{
    public PasswordResetConfirmRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID must not be empty.");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Reset token is required.");

        RuleFor(x => x.NewPassword)
            .ApplyStrongPasswordRules(); // Custom extension method for password complexity
    }
}

#endregion
