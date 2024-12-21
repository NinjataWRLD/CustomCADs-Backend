using CustomCADs.Shared.Core;

namespace CustomCADs.Identity.Endpoints.SignIn.Logout;

using static ApiMessages;
using static StatusCodes;

public sealed class LogoutEndpoint(IUserService service)
    : EndpointWithoutRequest<string>
{
    public override void Configure()
    {
        Post("logout");
        Group<SignInGroup>();
        Description(d => d
            .WithSummary("03. Log out")
            .WithDescription("Log out of your account")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        if (!User.GetAuthentication())
        {
            ValidationFailures.Add(new("Account", LoginBeforeLogout));
            await SendErrorsAsync(Status401Unauthorized);
            return;
        }

        await service.RevokeRefreshTokenAsync(User.GetName()).ConfigureAwait(false);
        HttpContext.DeleteAllCookies();
    }
}
