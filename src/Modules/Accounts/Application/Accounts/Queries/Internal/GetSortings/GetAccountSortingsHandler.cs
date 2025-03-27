using CustomCADs.Accounts.Domain.Accounts.Enums;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetSortings;

public class GetAccountSortingsHandler
    : IQueryHandler<GetAccountSortingsQuery, string[]>
{
    public Task<string[]> Handle(GetAccountSortingsQuery req, CancellationToken ct)
        => Task.FromResult(
            Enum.GetNames<AccountSortingType>()
        );
}
