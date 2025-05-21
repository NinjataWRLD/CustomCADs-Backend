using CustomCADs.Identity.Application.Users.Commands.Internal.Delete;
using CustomCADs.Identity.Application.Users.Dtos;
using Microsoft.Extensions.Options;

namespace CustomCADs.Identity.Endpoints.Identity.Delete;

public sealed class DeleteAccountEndpoint(IRequestSender sender, IOptions<CookieSettings> settings)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("");
        Group<IdentityGroup>();
        AllowAnonymous();
        Description(d => d
            .WithName(IdentityNames.DeleteAccount)
            .WithSummary("Delete")
            .WithDescription("Delete your account")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await sender.SendCommandAsync(
            new DeleteAccountCommand(Username: User.GetName()),
            ct
        ).ConfigureAwait(false);

        HttpContext.DeleteAllCookies(settings.Value.Domain);
    }
}
