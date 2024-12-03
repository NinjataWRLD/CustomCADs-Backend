using CustomCADs.Shared.Core;

namespace CustomCADs.Auth.Endpoints.Info.Authorization;

public sealed class AuthorizationEndpoint
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("authorization");
        Group<InfoGroup>();
        Description(d => d
            .WithSummary("02. AuthZ")
            .WithDescription("What Role do you have?")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendOkAsync(User.GetAuthorization()).ConfigureAwait(false);
    }
}
