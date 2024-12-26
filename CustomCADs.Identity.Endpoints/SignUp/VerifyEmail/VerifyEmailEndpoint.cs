using CustomCADs.Identity.Domain;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Identity.Endpoints.SignUp.VerifyEmail;

using static AccountConstants;
using static ApiMessages;
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

        HttpContext.SaveAccessTokenCookie(jwt.Value, jwt.EndDate);
        HttpContext.SaveRefreshTokenCookie(rt, rtEnd);
        HttpContext.SaveRoleCookie(role, rtEnd);
        HttpContext.SaveUsernameCookie(req.Username, rtEnd);

        await SendOkAsync("Welcome!").ConfigureAwait(false);
    }
}
