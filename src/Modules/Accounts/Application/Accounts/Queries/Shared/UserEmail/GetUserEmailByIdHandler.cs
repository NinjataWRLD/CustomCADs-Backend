using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Shared.UserEmail;

public sealed class GetUserEmailByIdHandler(IAccountReads reads)
	: IQueryHandler<GetUserEmailByIdQuery, string>
{
	public async Task<string> Handle(GetUserEmailByIdQuery req, CancellationToken ct)
	{
		Account account = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Account>.ById(req.Id);

		return account.Email;
	}
}
