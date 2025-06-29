// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region 🔐 PasswordRulesExtensions – Strong Password RuleSet

/*
 * Provides a reusable FluentValidation rule set for enforcing strong password requirements.
 * 
 * Rules enforced:
 *   🔹 Not empty
 *   🔹 Minimum 8 characters
 *   🔹 At least one uppercase letter
 *   🔹 At least one lowercase letter
 *   🔹 At least one digit
 *   🔹 At least one special character
 * 
 * Usage:
 *   RuleFor(x => x.Password).ApplyStrongPasswordRules();
 */

using FluentValidation;

namespace IdentityService.Web.Features.Validators;

internal static class PasswordRulesExtensions
{
    public static IRuleBuilderOptions<T, string> ApplyStrongPasswordRules<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .MinimumLength(8)
            .Matches("[A-Z]").WithMessage("At least one uppercase letter required.")
            .Matches("[a-z]").WithMessage("At least one lowercase letter required.")
            .Matches("[0-9]").WithMessage("At least one digit required.")
            .Matches("[^a-zA-Z0-9]").WithMessage("At least one special character required.");
    }
}

#endregion
