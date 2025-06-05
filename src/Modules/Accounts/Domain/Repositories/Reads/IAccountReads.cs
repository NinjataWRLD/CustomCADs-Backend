using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Accounts.Domain.Repositories.Reads;

public interface IAccountReads
{
	Task<Result<Account>> AllAsync(AccountQuery query, bool track = true, CancellationToken ct = default);
	Task<Account?> SingleByIdAsync(AccountId id, bool track = true, CancellationToken ct = default);
	Task<Account?> SingleByUsernameAsync(string username, bool track = true, CancellationToken ct = default);
	Task<bool> ExistsByIdAsync(AccountId id, CancellationToken ct = default);
	Task<bool> ExistsByUsernameAsync(string username, CancellationToken ct = default);
	Task<ProductId[]> ViewedProductsByIdAsync(AccountId id, CancellationToken ct = default);
	Task<ProductId[]> ViewedProductsByUsernameAsync(string username, CancellationToken ct = default);
	Task<int> CountAsync(CancellationToken ct = default);
}
