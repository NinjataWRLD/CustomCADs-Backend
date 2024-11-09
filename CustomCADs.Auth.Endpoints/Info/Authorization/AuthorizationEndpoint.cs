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
        Guid userId = User.GetId();
        AppUser? user = await serivce.FindByIdAsync(userId).ConfigureAwait(false);
        if (user is null)
        {
            ValidationFailures.Add(new("Id", UserNotFound, userId));
            await SendErrorsAsync(Status401Unauthorized);
            return;
        }

        string role = await serivce.GetRoleAsync(user).ConfigureAwait(false);
        await SendOkAsync(role).ConfigureAwait(false);
    }
}
