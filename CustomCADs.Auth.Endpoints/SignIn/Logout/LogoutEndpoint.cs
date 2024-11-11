using CustomCADs.Shared.Core;

namespace CustomCADs.Auth.Endpoints.SignIn.Logout;

using static StatusCodes;

public class LogoutEndpoint(IUserService service)
    : EndpointWithoutRequest<string>
{
    private const string NoLoginMessage = "In order to log out, you must be logged in";

    public override void Configure()
    {
        Post("logout");
        Group<SignInGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        if (!User.GetAuthentication())
        {
            ValidationFailures.Add(new("Account", NoLoginMessage));
            await SendErrorsAsync(Status401Unauthorized);
            return;
        }

        await service.RevokeRefreshTokenAsync(User.GetName()).ConfigureAwait(false);
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
