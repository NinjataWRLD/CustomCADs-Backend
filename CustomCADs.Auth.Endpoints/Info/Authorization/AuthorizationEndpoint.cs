using CustomCADs.Shared.Core;

namespace CustomCADs.Auth.Endpoints.Info.Authorization;

public class AuthorizationEndpoint
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("authorization");
        Group<InfoGroup>();
        Description(d => d.WithSummary("2. What Role do I have?"));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendOkAsync(User.GetAuthorization()).ConfigureAwait(false);
    }
}
