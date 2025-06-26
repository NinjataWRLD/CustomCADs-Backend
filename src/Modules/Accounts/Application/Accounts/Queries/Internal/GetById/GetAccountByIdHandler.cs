using CustomCADs.Accounts.Domain.Repositories.Reads;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetById;

public sealed class GetAccountByIdHandler(IAccountReads reads)
	: IQueryHandler<GetAccountByIdQuery, GetAccountByIdDto>
{
	public async Task<GetAccountByIdDto> Handle(GetAccountByIdQuery req, CancellationToken ct)
	{
		Account account = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Account>.ById(req.Id);

		return account.ToGetByUsernameDto();
	}
}
