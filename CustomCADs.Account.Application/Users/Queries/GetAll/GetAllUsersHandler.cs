using CustomCADs.Account.Domain.Users.Reads;

namespace CustomCADs.Account.Application.Users.Queries.GetAll;

public class GetAllUsersHandler(IUserReads reads)
    : IQueryHandler<GetAllUsersQuery, GetAllUsersDto>
{
    public async Task<GetAllUsersDto> Handle(GetAllUsersQuery req, CancellationToken ct)
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
        UserResult result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        GetAllUsersDto response = new(
            result.Count,
            result.Users.Select(u => u.ToGetAllUsersItem()).ToArray()
        );
        return response;
    }
}
