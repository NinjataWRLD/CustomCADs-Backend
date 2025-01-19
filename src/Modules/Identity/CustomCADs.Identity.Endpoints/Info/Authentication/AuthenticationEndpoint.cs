namespace CustomCADs.Identity.Endpoints.Info.Authentication;

public sealed class AuthenticationEndpoint
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("authentication");
        Group<InfoGroup>();
        Description(d => d
            .WithName(InfoNames.Authentication)
            .WithSummary("01. AuthN")
            .WithDescription("See if you're logged in")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendOkAsync(User.GetAuthentication()).ConfigureAwait(false);
    }
}
