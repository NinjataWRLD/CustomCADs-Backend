using CustomCADs.Account.Domain.Users;
using CustomCADs.Account.Domain.Users.Reads;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Account.Application.Users.Queries.GetAll;

public class GetAllUsersHandler(IUserReads reads)
    : IQueryHandler<GetAllUsersQuery, Result<GetAllUsersItem>>
{
    public async Task<Result<GetAllUsersItem>> Handle(GetAllUsersQuery req, CancellationToken ct)
    {
        UserQuery query = new(
            Role: req.Role,
            Username: req.Username,
            Email: req.Email,
            FirstName: req.FirstName,
            LastName: req.LastName,
            Sorting: req.Sorting,
            Page: req.Page,
            Limit: req.Limit
        );
        Result<User> result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        return new(
            result.Count,
            [.. result.Items.Select(u => u.ToGetAllUsersItem())]
        );
    }
}
