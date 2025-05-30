using CustomCADs.Customs.Domain.Customs;
using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Domain.Repositories.Reads;

public interface ICustomReads
{
	Task<Result<Custom>> AllAsync(CustomQuery query, bool track = true, CancellationToken ct = default);
	Task<Custom?> SingleByIdAsync(CustomId id, bool track = true, CancellationToken ct = default);
	Task<bool> ExistsByIdAsync(CustomId id, CancellationToken ct = default);
	Task<Dictionary<CustomStatus, int>> CountAsync(AccountId buyerId, CancellationToken ct = default);
}
