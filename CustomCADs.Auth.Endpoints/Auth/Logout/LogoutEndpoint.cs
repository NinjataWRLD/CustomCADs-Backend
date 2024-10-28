using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Shared.Presentation;
using FastEndpoints;

namespace CustomCADs.Auth.Endpoints.Auth.Logout;

public class LogoutEndpoint(IUserService service) : EndpointWithoutRequest<string>
{
    public override void Configure()
    {
        Post("Logout");
        Group<AuthGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await service.RevokeRefreshTokenAsync(User.GetId()).ConfigureAwait(false);
        DeleteCookies(["jwt", "rt", "username", "rt"]);
        DeleteCookies(["jwt", "rt", "username", "role"]);
    }

    private void DeleteCookies(params string[] cookies)
    {
        foreach (string cookie in cookies)
        {
            HttpContext.Response.Cookies.Delete(cookie);
        }
    }
}
