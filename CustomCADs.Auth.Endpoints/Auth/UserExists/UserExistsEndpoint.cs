using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Infrastructure.Entities;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace CustomCADs.Auth.Endpoints.Auth.UserExists;
public class UserExistsEndpoint(IUserService service) : Endpoint<UserExistsRequest>
{
    public override void Configure()
    {
        Get("UserExists/{username}");
        Group<AuthGroup>();
    }

    public override async Task HandleAsync(UserExistsRequest req, CancellationToken ct)
    {
        AppUser? user = await service.FindByNameAsync(req.Username).ConfigureAwait(false);
        bool response = user != null;

        await SendOkAsync(response).ConfigureAwait(false);
    }
}
