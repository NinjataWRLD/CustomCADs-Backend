using CustomCADs.Identity.Application.Users.Commands.Internal.Logout;

namespace CustomCADs.Identity.Endpoints.Identity.Post.Logout;

public sealed class LogoutEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<string>
{
    public override void Configure()
    {
        Post("logout");
        Group<IdentityGroup>();
        Description(d => d
            .WithName(IdentityNames.Logout)
            .WithSummary("Log out")
            .WithDescription("Log out of your account")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await sender.SendCommandAsync(
            new LogoutUserCommand(
                Username: User.GetName()
            ),
            ct
        ).ConfigureAwait(false);

        HttpContext.DeleteAllCookies();
    }
}
