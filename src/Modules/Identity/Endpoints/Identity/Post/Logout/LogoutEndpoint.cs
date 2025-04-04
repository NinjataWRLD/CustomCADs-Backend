namespace CustomCADs.Identity.Endpoints.Identity.Post.Logout;

public sealed class LogoutEndpoint(IUserService service)
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
        await service.LogoutAsync(
            username: User.GetName()
        ).ConfigureAwait(false);

        HttpContext.DeleteAllCookies();
    }
}
