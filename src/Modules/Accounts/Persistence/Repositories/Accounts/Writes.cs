using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Accounts.Domain.Repositories.Writes;
using CustomCADs.Accounts.Persistence.ShadowEntities;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Accounts.Persistence.Repositories.Accounts;

public class Writes(AccountsContext context) : IAccountWrites
{
	public async Task<Account> AddAsync(Account entity, CancellationToken ct = default)
		=> (await context.Accounts.AddAsync(entity, ct).ConfigureAwait(false)).Entity;

	public async Task ViewProductAsync(AccountId id, ProductId productId, CancellationToken ct = default)
		=> await context.ViewedProducts.AddAsync(
				entity: ViewedProduct.Create(id, productId),
				ct
			).ConfigureAwait(false);

	public void Remove(Account entity)
		=> context.Accounts.Remove(entity);
}
