using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Shared.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace CustomCADs.Auth.Endpoints.Auth.Logout;

using static StatusCodes;

public class LogoutEndpoint(IUserService service) : EndpointWithoutRequest<string>
{
    private const string NoLoginMessage = "In order to log out, you must be logged in";

    public override void Configure()
    {
        Post("logout");
        Group<AuthGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Guid id = User.GetId();
        if (id != Guid.Empty)
        {
            ValidationFailures.Add(new("Id", NoLoginMessage));
            await SendErrorsAsync(Status401Unauthorized);
            return;
        }

        await service.RevokeRefreshTokenAsync(id).ConfigureAwait(false);
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
