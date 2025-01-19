using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Accounts.Domain.Accounts.Reads;

public interface IAccountReads
{
    Task<Result<Account>> AllAsync(AccountQuery query, bool track = true, CancellationToken ct = default);
    Task<Account?> SingleByIdAsync(AccountId id, bool track = true, CancellationToken ct = default);
    Task<Account?> SingleByUsernameAsync(string username, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(AccountId id, CancellationToken ct = default);
    Task<bool> ExistsByUsernameAsync(string username, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
}
