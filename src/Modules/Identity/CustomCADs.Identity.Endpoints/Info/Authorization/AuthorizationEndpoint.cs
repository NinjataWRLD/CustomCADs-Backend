using CustomCADs.Shared.Core;

namespace CustomCADs.Identity.Endpoints.Info.Authorization;

public sealed class AuthorizationEndpoint
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("authorization");
        Group<InfoGroup>();
        Description(d => d
            .WithName(InfoNames.Authorization)
            .WithSummary("02. AuthZ")
            .WithDescription("See what Role you're logged in with")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendOkAsync(User.GetAuthorization()).ConfigureAwait(false);
    }
}
