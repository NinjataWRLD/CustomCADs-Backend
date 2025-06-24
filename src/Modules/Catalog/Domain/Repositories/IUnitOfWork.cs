namespace CustomCADs.Catalog.Domain.Repositories;

public interface IUnitOfWork
{
	Task SaveChangesAsync(CancellationToken ct = default);
	Task ClearProductTagsAsync(ProductId[] ids, string tag, CancellationToken ct = default);
	Task AddProductPurchasesAsync(ProductId[] ids, int count = 1, CancellationToken ct = default);
}
