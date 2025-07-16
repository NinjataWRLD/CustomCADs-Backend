using CustomCADs.Customs.Domain.Customs;
using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Customs.Persistence.Repositories.Customs;

public sealed class Reads(CustomsContext context) : ICustomReads
{
	public async Task<Result<Custom>> AllAsync(CustomQuery query, bool track = true, CancellationToken ct = default)
	{
		IQueryable<Custom> queryable = context.Customs
			.WithTracking(track)
			.WithFilter(query.ForDelivery, query.CustomStatus, query.BuyerId, query.DesignerId)
			.WithSearch(query.Name);

		int count = await queryable.CountAsync(ct).ConfigureAwait(false);
		Custom[] customs = await queryable
			.WithSorting(query.Sorting ?? new())
			.WithPagination(query.Pagination.Page, query.Pagination.Limit)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

		return new(count, customs);
	}

	public async Task<Custom?> SingleByIdAsync(CustomId id, bool track = true, CancellationToken ct = default)
		=> await context.Customs
			.WithTracking(track)
			.FirstOrDefaultAsync(o => o.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<bool> ExistsByIdAsync(CustomId id, CancellationToken ct = default)
		=> await context.Customs
			.WithTracking(false)
			.AnyAsync(o => o.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<Dictionary<CustomStatus, int>> CountAsync(AccountId buyerId, CancellationToken ct = default)
		=> await context.Customs
			.WithTracking(false)
			.Where(o => o.BuyerId == buyerId)
			.GroupBy(o => o.CustomStatus)
			.ToDictionaryAsync(x => x.Key, x => x.Count(), ct)
			.ConfigureAwait(false);
}
