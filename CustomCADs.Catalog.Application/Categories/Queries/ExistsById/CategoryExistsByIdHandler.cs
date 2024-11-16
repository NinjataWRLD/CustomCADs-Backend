using CustomCADs.Catalog.Domain.Categories.Reads;

namespace CustomCADs.Catalog.Application.Categories.Queries.ExistsById;

public class CategoryExistsByIdHandler(ICategoryReads reads)
    : IQueryHandler<CategoryExistsByIdQuery, bool>
{
    public async Task<bool> Handle(CategoryExistsByIdQuery req, CancellationToken ct)
    {
        return await reads.ExistsByIdAsync(req.Id, ct: ct).ConfigureAwait(false);
    }
}
