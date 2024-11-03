using CustomCADs.Account.Application.Users.Queries.GetByUsername;
using FastEndpoints;
using MediatR;

namespace CustomCADs.Account.Endpoints.Users.GetUser;

public class GetUserEndpoint(IMediator mediator) : Endpoint<GetUserRequest, UserResponse>
{
    public override void Configure()
    {
        Get("{username}");
        Group<UsersGroup>();
    }

    public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
    {
        GetUserByUsernameQuery query = new(req.Username);
        GetUserByUsernameDto dto = await mediator.Send(query, ct);

        UserResponse response = new(
            Role: dto.Role,
            Username: req.Username,
            Email: dto.Email,
            FirstName: dto.FirstName,
            LastName: dto.LastName
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
