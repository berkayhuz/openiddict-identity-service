// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region 🔐 AuthController – Identity & Account Management Endpoints

/*
 * Handles all authentication, registration, password reset,
 * email confirmation, and user info update endpoints.
 * Integrates with ASP.NET Identity and OpenIddict token-based flows.
 */

using AutoMapper;
using FluentValidation;
using IdentityService.Web.Entities;
using IdentityService.Web.Features.DTOs;
using IdentityService.Web.Features.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IdentityService.Web.Controllers;

[ApiController]
[Route("connect")]
internal class AuthController(
    ICredentialService<AppUser> _credentialService,
    UserManager<AppUser> _userManager,
    IMapper _mapper,
    ILogger<AuthController> _logger) : ControllerBase
{
    private static readonly Action<ILogger, string, Exception?> _logConfirmationLink =
        LoggerMessage.Define<string>(LogLevel.Information, new EventId(1, nameof(Register)),
            "Email confirmation URL generated: {Url}");

    #region ✍️ Register Endpoint

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var validationProblem = await ValidateRequestAsync(request).ConfigureAwait(false);
        if (validationProblem != null)
            return validationProblem;

        var user = _mapper.Map<AppUser>(request);

        var result = await _credentialService.RegisterAsync(user, request.Password).ConfigureAwait(false);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        var token = await _credentialService.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
        var confirmationUrl = Url.Action(nameof(ConfirmEmail), "Auth", new { userId = user.Id, token }, Request.Scheme);

        _logConfirmationLink(_logger, confirmationUrl!, null);

        return Ok("Registration successful. Please check your email to confirm your account.");
    }

    #endregion

    #region ✅ Confirm Email Endpoint

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
    {
        var user = await _credentialService.FindByIdAsync(userId).ConfigureAwait(false);
        if (user == null)
            return NotFound("User not found");

        var result = await _credentialService.ConfirmEmailAsync(user, token).ConfigureAwait(false);
        return result.Succeeded ? Ok("Email confirmed!") : BadRequest("Invalid token");
    }

    #endregion

    #region 🔓 Logout Endpoint

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _credentialService.SignOutAsync().ConfigureAwait(false);
        return Ok("Logged out.");
    }

    #endregion

    #region 🔑 Password Reset Endpoints

    [HttpPost("password-reset-request")]
    public async Task<IActionResult> PasswordResetRequest([FromBody] PasswordResetRequest model)
    {
        var user = await _credentialService.FindByEmailAsync(model.Email).ConfigureAwait(false);
        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user).ConfigureAwait(false)))
            return BadRequest("Invalid user");

        var token = await _credentialService.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
        var resetUrl = Url.Action(nameof(PasswordResetConfirm), "Auth", new { userId = user.Id, token }, Request.Scheme);

        Console.WriteLine($"Password reset link: {resetUrl}");
        return Ok("Password reset email sent.");
    }

    [HttpPost("password-reset-confirm")]
    public async Task<IActionResult> PasswordResetConfirm([FromBody] PasswordResetConfirmRequest model)
    {
        var validationProblem = await ValidateRequestAsync(model).ConfigureAwait(false);
        if (validationProblem != null)
            return validationProblem;

        var user = await _credentialService.FindByIdAsync(model.UserId).ConfigureAwait(false);
        if (user == null)
            return NotFound("User not found");

        var result = await _credentialService.ResetPasswordAsync(user, model.Token, model.NewPassword).ConfigureAwait(false);
        return result.Succeeded ? Ok("Password reset successful.") : BadRequest(result.Errors);
    }

    #endregion

    #region 📝 Update & Retrieve User Info

    [Authorize]
    [HttpPost("update-user-info")]
    public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserInfoRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(userId)) return Unauthorized();

        var user = await _credentialService.FindByIdAsync(userId).ConfigureAwait(false);
        if (user == null) return NotFound();

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        var result = await _userManager.UpdateAsync(user).ConfigureAwait(false);
        return result.Succeeded ? Ok("User info updated.") : BadRequest(result.Errors);
    }

    [Authorize]
    [HttpGet("user-info")]
    public async Task<IActionResult> GetUserInfo()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(userId)) return Unauthorized();

        var user = await _credentialService.FindByIdAsync(userId).ConfigureAwait(false);
        if (user == null) return NotFound();

        var dto = _mapper.Map<UserInfoDto>(user);
        return Ok(dto);
    }

    #endregion

    #region 🔐 Change Password

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(userId)) return Unauthorized();

        var user = await _credentialService.FindByIdAsync(userId).ConfigureAwait(false);
        if (user == null) return NotFound();

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword).ConfigureAwait(false);
        return result.Succeeded ? Ok("Password changed successfully.") : BadRequest(result.Errors);
    }

    #endregion

    #region ✉️ Resend Confirmation Email

    [HttpPost("resend-confirmation")]
    public async Task<IActionResult> ResendEmailConfirmation([FromBody] ResendEmailRequest request)
    {
        var validationProblem = await ValidateRequestAsync(request).ConfigureAwait(false);
        if (validationProblem != null)
            return validationProblem;

        var user = await _credentialService.FindByEmailAsync(request.Email).ConfigureAwait(false);
        if (user == null)
            return NotFound("User not found");

        if (await _userManager.IsEmailConfirmedAsync(user).ConfigureAwait(false))
            return BadRequest("Email already confirmed");

        var token = await _credentialService.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
        var confirmationUrl = Url.Action(nameof(ConfirmEmail), "Auth", new { userId = user.Id, token }, Request.Scheme);

        _logConfirmationLink(_logger, confirmationUrl!, null);

        return Ok("Confirmation email sent.");
    }

    #endregion

    #region 📧 Change Email

    [Authorize]
    [HttpPost("change-email")]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequest request)
    {
        var validationProblem = await ValidateRequestAsync(request).ConfigureAwait(false);
        if (validationProblem != null)
            return validationProblem;

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(userId)) return Unauthorized();

        var user = await _credentialService.FindByIdAsync(userId).ConfigureAwait(false);
        if (user == null) return NotFound();

        if (await _userManager.IsEmailConfirmedAsync(user).ConfigureAwait(false) == false)
            return BadRequest("Your email must be confirmed before changing it.");

        if (string.Equals(user.Email, request.NewEmail, StringComparison.OrdinalIgnoreCase))
            return BadRequest("New email address must be different from the current one.");

        var token = await _credentialService.GenerateChangeEmailTokenAsync(user, request.NewEmail).ConfigureAwait(false);
        var confirmUrl = Url.Action(nameof(ConfirmEmailChange), "Auth", new { userId = user.Id, newEmail = request.NewEmail, token }, Request.Scheme);

        _logConfirmationLink(_logger, confirmUrl!, null);

        return Ok("Confirmation email sent to your new address. Please verify to complete the change.");
    }

    [HttpGet("confirm-email-change")]
    public async Task<IActionResult> ConfirmEmailChange([FromQuery] string userId, [FromQuery] string newEmail, [FromQuery] string token)
    {
        var user = await _credentialService.FindByIdAsync(userId).ConfigureAwait(false);
        if (user == null)
            return NotFound("User not found");

        var result = await _credentialService.ChangeEmailAsync(user, newEmail, token).ConfigureAwait(false);
        return result.Succeeded ? Ok("Email successfully changed.") : BadRequest(result.Errors);
    }

    #endregion

    #region ✅ Internal Validator Resolver

    /*
     * Dynamically resolves FluentValidation validators from the DI container
     * and executes validation logic. Returns 400 BadRequest if validation fails.
     */
    private async Task<IActionResult?> ValidateRequestAsync<T>(T model)
    {
        var validator = HttpContext.RequestServices.GetService<IValidator<T>>();
        if (validator is null) return null;

        var validationResult = await validator.ValidateAsync(model).ConfigureAwait(false);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        return null;
    }

    #endregion
}

#endregion
