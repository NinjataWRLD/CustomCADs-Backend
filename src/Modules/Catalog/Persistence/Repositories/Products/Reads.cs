using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Persistence;

namespace CustomCADs.Catalog.Persistence.Repositories.Products;

public sealed class Reads(CatalogContext context) : IProductReads
{
	public async Task<Result<Product>> AllAsync(ProductQuery query, bool track = true, CancellationToken ct = default)
	{
		ProductId[]? ids = await context.ProductTags
			.GetProductIdsByTagIdsOrDefaultAsync(query.TagIds, ct)
			.ConfigureAwait(false)
			?? query.Ids;

		IQueryable<Product> queryable = context.Products
				.WithTracking(track)
				.WithFilter(ids, query.CreatorId, query.DesignerId, query.CategoryId, query.Status)
				.WithSearch(query.Name)
				.WithSorting(query.Sorting ?? new());

		int count = await queryable.CountAsync(ct).ConfigureAwait(false);
		Product[] products = await queryable
				.WithPagination(query.Pagination.Page, query.Pagination.Limit)
				.ToArrayAsync(ct)
				.ConfigureAwait(false);

		return new(count, products);
	}

	public async Task<ProductId[]> AllAsync(DateTimeOffset? before, DateTimeOffset? after, CancellationToken ct = default)
		=> await context.Products
			.WithTracking(false)
			.WithFilter(before: before, after: after)
			.Select(x => x.Id)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

	public async Task<Product?> SingleByIdAsync(ProductId id, bool track = true, CancellationToken ct = default)
		=> await context.Products
			.WithTracking(track)
			.FirstOrDefaultAsync(p => p.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<string[]> TagsByIdAsync(ProductId id, CancellationToken ct = default)
		=> await context.ProductTags
			.Where(p => p.ProductId == id)
			.Select(p => p.Tag.Name)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

	public async Task<Dictionary<ProductId, string[]>> TagsByIdsAsync(ProductId[] ids, CancellationToken ct = default)
		=> await context.ProductTags
			.GroupBy(x => x.ProductId)
			.Select(x => new { Id = x.Key, Tags = x.Select(x => x.Tag.Name).ToArray() })
			.ToDictionaryAsync(x => x.Id, x => x.Tags, ct)
			.ConfigureAwait(false);

	public async Task<bool> ExistsByIdAsync(ProductId id, CancellationToken ct = default)
		=> await context.Products
			.WithTracking(false)
			.AnyAsync(p => p.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<Dictionary<ProductStatus, int>> CountByStatusAsync(AccountId creatorId, CancellationToken ct = default)
		=> await context.Products
			.WithTracking(false)
			.Where(p => p.CreatorId == creatorId)
			.GroupBy(p => p.Status)
			.ToDictionaryAsync(x => x.Key, x => x.Count(), ct)
			.ConfigureAwait(false);
}
