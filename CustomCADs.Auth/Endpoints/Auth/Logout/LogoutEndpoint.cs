using CustomCADs.Auth.Business.Contracts;
using CustomCADs.Shared.Presentation;
using FastEndpoints;

namespace CustomCADs.Auth.Endpoints.Auth.Logout;

public class LogoutEndpoint(IUserManager manager) : EndpointWithoutRequest<string>
{
    public override void Configure()
    {
        Post("Logout");
        Group<AuthGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await manager.RevokeRefreshTokenAsync(User.GetId()).ConfigureAwait(false);
        DeleteCookies(["jwt", "rt", "username", "rt"]);
    }

    private void DeleteCookies(params string[] cookies)
    {
        foreach (string cookie in cookies)
        {
            HttpContext.Response.Cookies.Delete(cookie);
        }
    }
}
