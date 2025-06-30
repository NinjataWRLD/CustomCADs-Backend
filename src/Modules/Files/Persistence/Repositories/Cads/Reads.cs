using CustomCADs.Files.Domain.Cads;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Files.Persistence.Repositories.Cads;

public sealed class Reads(FilesContext context) : ICadReads
{
	public async Task<Result<Cad>> AllAsync(CadQuery query, bool track = true, CancellationToken ct = default)
	{
		IQueryable<Cad> queryable = context.Cads
			.WithFilter(query.Ids);

		int count = await queryable.CountAsync(ct).ConfigureAwait(false);
		Cad[] cads = await queryable
			.WithPagination(query.Pagination.Page, query.Pagination.Limit)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

		return new(count, cads);
	}

	public async Task<Cad?> SingleByIdAsync(CadId id, bool track = true, CancellationToken ct = default)
		=> await context.Cads
			.WithTracking(track)
			.FirstOrDefaultAsync(c => c.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<bool> ExistsByIdAsync(CadId id, CancellationToken ct = default)
		=> await context.Cads
			.WithTracking(false)
			.AnyAsync(c => c.Id == id, ct)
			.ConfigureAwait(false);
}
