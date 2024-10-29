using CustomCADs.Account.Application.Users.Queries.GetAll;
using CustomCADs.Account.Domain.Users.Reads;
using FastEndpoints;
using Wolverine;

namespace CustomCADs.Account.Endpoints.Users.GetUsers;

public class GetUsersEndpoint(IMessageBus bus) : Endpoint<GetUsersRequest, GetUsersResponse>
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
        var result = await bus.InvokeAsync<UserResult>(query, ct).ConfigureAwait(false);

        GetUsersResponse response = new()
        {
            Count = result.Count,
            Users = result.Users.Select(u => new UserResponseDto(
                Role: u.RoleName,
                Username: u.Username,
                Email: u.Email,
                FirstName: u.NameInfo.FirstName,
                LastName: u.NameInfo.LastName
            )).ToArray(),
        };
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
