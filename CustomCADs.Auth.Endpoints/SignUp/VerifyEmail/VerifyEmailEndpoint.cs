using CustomCADs.Auth.Application.Dtos;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Auth.Endpoints.SignUp.VerifyEmail;

using static ApiMessages;
using static AuthConstants;
using static StatusCodes;

public class VerifyEmailEndpoint(IUserService userService, ITokenService tokenService)
    : Endpoint<VerifyEmailRequest>
{
    public override void Configure()
    {
        Get("verifyEmail/{username}");
        Group<SignUpGroup>();
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

        UserId accountId = user.AccountId ?? throw UserAccountNotCreatedYetException.ByUsername(user.UserName ?? string.Empty);
        string role = await userService.GetRoleAsync(user).ConfigureAwait(false);

        AccessTokenDto jwt = tokenService.GenerateAccessToken(accountId, req.Username, role);
        SaveAccessToken(jwt.Value, jwt.EndDate);

        string rt = tokenService.GenerateRefreshToken();
        DateTime end = DateTime.UtcNow.AddDays(RtDurationInDays);
        SaveRefreshToken(rt, end);

        SaveRole(role, end);
        SaveUsername(req.Username, end);

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
