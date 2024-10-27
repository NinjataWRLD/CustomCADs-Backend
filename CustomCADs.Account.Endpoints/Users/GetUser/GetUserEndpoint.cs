using CustomCADs.Account.Application.Users.Queries.GetByUsername;
using FastEndpoints;
using Mapster;
using Wolverine;

namespace CustomCADs.Account.Endpoints.Users.GetUser;

public class GetUserEndpoint(IMessageBus bus) : Endpoint<GetUserRequest, UserResponseDto>
{
    public override void Configure()
    {
        Get("{username}");
        Group<UsersGroup>();
    }

    public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
    {
        GetUserByUsernameQuery query = new(req.Username);
        var dto = await bus.InvokeAsync<GetUserByUsernameDto>(query, ct);

        UserResponseDto response = dto.Adapt<UserResponseDto>();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
