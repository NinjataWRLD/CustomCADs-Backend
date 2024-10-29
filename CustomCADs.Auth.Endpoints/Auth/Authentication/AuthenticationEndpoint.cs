using CustomCADs.Shared.Presentation;
using FastEndpoints;

namespace CustomCADs.Auth.Endpoints.Auth.Authentication;
public class AuthenticationEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("Authentication");
        Group<AuthGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendOkAsync(User.GetAuthentication()).ConfigureAwait(false);
    }
}
