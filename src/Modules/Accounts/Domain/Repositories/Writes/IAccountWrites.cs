using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Accounts.Domain.Repositories.Writes;

public interface IAccountWrites
{
	Task<Account> AddAsync(Account entity, CancellationToken ct = default);
	Task ViewProductAsync(AccountId id, ProductId productId, CancellationToken ct = default);
	void Remove(Account entity);
}
