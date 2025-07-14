using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Shared.Info;

public class GetAccountInfoByUsernameHandler(IAccountReads reads)
	: IQueryHandler<GetAccountInfoByUsernameQuery, AccountInfo>
{
	public async Task<AccountInfo> Handle(GetAccountInfoByUsernameQuery req, CancellationToken ct = default)
	{
		Account account = await reads.SingleByUsernameAsync(req.Username, track: false, ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Account>.ByProp(nameof(req.Username), req.Username);

		return new(
			CreatedAt: account.CreatedAt,
			TrackViewedProducts: account.TrackViewedProducts,
			FirstName: account.FirstName,
			LastName: account.LastName
		);
	}
}
