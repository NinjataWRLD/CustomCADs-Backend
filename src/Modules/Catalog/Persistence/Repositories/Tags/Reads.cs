using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.Shared.Persistence.Extensions;

namespace CustomCADs.Catalog.Persistence.Repositories.Tags;

public sealed class Reads(CatalogContext context) : ITagReads
{
	public async Task<Tag[]> AllAsync(bool track = true, CancellationToken ct = default)
		=> await context.Tags
			.WithTracking(track)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

	public async Task<Tag?> SingleByIdAsync(TagId id, bool track = true, CancellationToken ct = default)
		=> await context.Tags
			.WithTracking(track)
			.FirstOrDefaultAsync(p => p.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<bool> ExistsByIdAsync(TagId id, CancellationToken ct = default)
		=> await context.Tags
			.WithTracking(false)
			.AnyAsync(p => p.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<Dictionary<TagId, int>> CountByProductAsync(CancellationToken ct = default)
		=> await context.ProductTags
			.AsNoTracking()
			.GroupBy(x => x.TagId)
			.ToDictionaryAsync(x => x.Key, x => x.Count(), ct)
			.ConfigureAwait(false);
}
