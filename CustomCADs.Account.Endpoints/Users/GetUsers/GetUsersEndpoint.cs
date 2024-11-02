using CustomCADs.Account.Application.Users.Queries.GetAll;
using FastEndpoints;
using Wolverine;

namespace CustomCADs.Account.Endpoints.Users.GetUsers;

public class GetUsersEndpoint(IMessageBus bus) : Endpoint<GetUsersRequest, (int Count, UserResponse[] Users)>
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
        var result = await bus.InvokeAsync<GetAllUsersDto>(query, ct).ConfigureAwait(false);

        (int Count, UserResponse[] Users) response = 
        (
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
