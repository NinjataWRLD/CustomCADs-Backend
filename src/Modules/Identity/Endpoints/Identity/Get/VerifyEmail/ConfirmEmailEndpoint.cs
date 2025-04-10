using CustomCADs.Identity.Application.Users.Commands.Internal.VerifyEmail;
using CustomCADs.Identity.Application.Users.Dtos;
using Microsoft.Extensions.Options;

namespace CustomCADs.Identity.Endpoints.Identity.Get.VerifyEmail;

public sealed class ConfirmEmailEndpoint(IRequestSender sender, IOptions<CookieSettings> settings)
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
        TokensDto tokens = await sender.SendCommandAsync(
            new VerifyUserEmailCommand(
                Username: req.Username,
                Token: req.Token.Replace(' ', '+')
            ),
            ct
        ).ConfigureAwait(false);

        HttpContext.SaveAllCookies(
            domain: settings.Value.Domain,
            username: req.Username,
            tokens: tokens
        );
        await SendOkAsync("Welcome!").ConfigureAwait(false);
    }
}
