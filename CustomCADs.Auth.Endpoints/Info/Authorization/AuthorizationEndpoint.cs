using CustomCADs.Shared.Core;

namespace CustomCADs.Auth.Endpoints.Info.Authorization;

using static ApiMessages;
using static StatusCodes;

public class AuthorizationEndpoint(IUserService serivce)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("authorization");
        Group<InfoGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        string username = User.GetName();
        AppUser? user = await serivce.FindByNameAsync(username).ConfigureAwait(false);
        if (user is null)
        {
            ValidationFailures.Add(new("Id", UserNotFound, username));
            await SendErrorsAsync(Status401Unauthorized);
            return;
        }

        string role = await serivce.GetRoleAsync(user).ConfigureAwait(false);
        await SendOkAsync(role).ConfigureAwait(false);
    }
}
