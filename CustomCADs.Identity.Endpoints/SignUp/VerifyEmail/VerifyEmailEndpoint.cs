using CustomCADs.Identity.Application.Common.Dtos;
using CustomCADs.Identity.Domain;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Identity.Endpoints.SignUp.VerifyEmail;

using static ApiMessages;
using static AuthConstants;
using static StatusCodes;

public sealed class VerifyEmailEndpoint(IUserService userService, ITokenService tokenService)
    : Endpoint<VerifyEmailRequest>
{
    public override void Configure()
    {
        Get("verifyEmail/{username}");
        Group<SignUpGroup>();
        Description(d => d
            .WithSummary("03. Verify Email")
            .WithDescription("Verify your Email by providing the token received in the email")
        );
    }

    public override async Task HandleAsync(VerifyEmailRequest req, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(req.Token))
        {
            ValidationFailures.Add(new("EmailToken", IsRequired, req.Token)
            {
                FormattedMessagePlaceholderValues = new() { ["0"] = "Email Token" },
            });
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        AppUser? user = await userService.FindByNameAsync(req.Username).ConfigureAwait(false);
        if (user is null)
        {
            ValidationFailures.Add(new("Username", UserNotFound, req.Username));
            await SendErrorsAsync(Status404NotFound).ConfigureAwait(false);
            return;
        }

        if (user.EmailConfirmed)
        {
            ValidationFailures.Add(new("Email", EmailAlreadyVerified));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        string decodedEct = req.Token.Replace(' ', '+');
        IdentityResult result = await userService.ConfirmEmailAsync(user, decodedEct).ConfigureAwait(false);
        if (!result.Succeeded)
        {
            ValidationFailures.Add(new("EmailToken", InvalidEmailToken, decodedEct));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        string role = await userService.GetRoleAsync(user).ConfigureAwait(false);
        AccessTokenDto jwt = tokenService.GenerateAccessToken(user.AccountId, req.Username, role);
        string rt = tokenService.GenerateRefreshToken();

        DateTime rtEnd = DateTime.UtcNow.AddDays(RtDurationInDays);
        await userService.UpdateRefreshTokenAsync(user.Id, rt, rtEnd).ConfigureAwait(false);

        SaveAccessToken(jwt.Value, jwt.EndDate);
        SaveRefreshToken(rt, rtEnd);
        SaveRole(role, rtEnd);
        SaveUsername(req.Username, rtEnd);

        await SendOkAsync("Welcome!").ConfigureAwait(false);
    }

    private void SaveAccessToken(string jwt, DateTime end)
    {
        HttpContext.Response.Cookies.Append("jwt", jwt, new() { HttpOnly = true, Secure = true, Expires = end });
    }

    private void SaveRefreshToken(string rt, DateTime end)
    {
        HttpContext.Response.Cookies.Append("rt", rt, new() { HttpOnly = true, Secure = true, Expires = end });
    }

    private void SaveRole(string role, DateTime end)
    {
        HttpContext.Response.Cookies.Append("role", role, new() { Expires = end });
    }

    private void SaveUsername(string username, DateTime end)
    {
        HttpContext.Response.Cookies.Append("username", username, new() { Expires = end });
    }
}
