using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Accounts.Application.Accounts.Queries.GetAll;

public class GetAllAccountsHandler(IAccountReads reads)
    : IQueryHandler<GetAllAccountsQuery, Result<GetAllAccountsItem>>
{
    public async Task<Result<GetAllAccountsItem>> Handle(GetAllAccountsQuery req, CancellationToken ct)
    {
        AccountQuery query = new(
            Role: req.Role,
            Username: req.Username,
            Email: req.Email,
            FirstName: req.FirstName,
            LastName: req.LastName,
            Sorting: req.Sorting,
            Page: req.Page,
            Limit: req.Limit
        );
        Result<Account> result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        return new(
            Count: result.Count,
            Items: [.. result.Items.Select(a => a.ToGetAllAccountsItem())]
        );
    }
}
