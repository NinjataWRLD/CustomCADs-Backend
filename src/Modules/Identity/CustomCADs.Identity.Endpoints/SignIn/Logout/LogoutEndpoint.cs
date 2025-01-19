namespace CustomCADs.Identity.Endpoints.SignIn.Logout;

public sealed class LogoutEndpoint(IUserService service)
    : EndpointWithoutRequest<string>
{
    public override void Configure()
    {
        Post("logout");
        Group<SignInGroup>();
        Description(d => d
            .WithName(SignInNames.Logout)
            .WithSummary("03. Log out")
            .WithDescription("Log out of your account")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await service.RevokeRefreshTokenAsync(User.GetName()).ConfigureAwait(false);
        HttpContext.DeleteAllCookies();
    }
}
