using CustomCADs.Identity.Application.Users.Commands.Internal.VerifyEmail;
using CustomCADs.Identity.Application.Users.Dtos;

namespace CustomCADs.Identity.Endpoints.Identity.Get.VerifyEmail;

public sealed class ConfirmEmailEndpoint(IRequestSender sender)
    : Endpoint<ConfirmEmailRequest>
{
    public override void Configure()
    {
        Get("email/confirm/{username}");
        Group<IdentityGroup>();
        Description(d => d
            .WithName(IdentityNames.ConfirmEmail)
            .WithSummary("Confirm Email")
            .WithDescription("Confirm the verification email")
        );
    }

    public override async Task HandleAsync(ConfirmEmailRequest req, CancellationToken ct)
    {
        TokensDto tokens = await sender.SendCommandAsync(command: new VerifyUserEmailCommand(
            Username: req.Username,
            Token: req.Token.Replace(' ', '+')
        ), ct).ConfigureAwait(false);

        HttpContext.SaveAccessTokenCookie(tokens.AccessToken.Value, tokens.AccessToken.ExpiresAt);
        HttpContext.SaveRefreshTokenCookie(tokens.RefreshToken.Value, tokens.RefreshToken.ExpiresAt);
        HttpContext.SaveRoleCookie(tokens.Role, tokens.RefreshToken.ExpiresAt);
        HttpContext.SaveUsernameCookie(req.Username, tokens.RefreshToken.ExpiresAt);

        await SendOkAsync("Welcome!").ConfigureAwait(false);
    }
}
