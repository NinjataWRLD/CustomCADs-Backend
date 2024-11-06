using CustomCADs.Account.Application.Users.Queries.GetAll;
using FastEndpoints;
using MediatR;

namespace CustomCADs.Account.Endpoints.Users.GetUsers;

public class GetUsersEndpoint(IMediator mediator) : Endpoint<GetUsersRequest, GetUsersResponse>
{
    public override void Configure()
    {
        Get("");
        Group<UsersGroup>();
    }

    public override async Task HandleAsync(GetUsersRequest req, CancellationToken ct)
    {
        GetAllUsersQuery query = new(
            Username: req.Name,
            Sorting: req.Sorting ?? "",
            Page: req.Page,
            Limit: req.Limit
        );
        GetAllUsersDto result = await mediator.Send(query, ct).ConfigureAwait(false);

        GetUsersResponse response = new(
            result.Count,
            result.Users.Select(u => new UserResponse(
                Role: u.Role,
                Username: u.Username,
                Email: u.Email,
                FirstName: u.FirstName,
                LastName: u.LastName
            )).ToArray()
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
