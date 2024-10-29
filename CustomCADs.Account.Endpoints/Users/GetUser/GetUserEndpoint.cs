using CustomCADs.Account.Application.Users.Queries.GetByUsername;
using FastEndpoints;
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

        UserResponseDto response = new()
        {
            Role = dto.Role,
            Username = req.Username,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
        };
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
