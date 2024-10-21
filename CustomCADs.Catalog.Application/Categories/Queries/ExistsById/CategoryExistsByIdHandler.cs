using CustomCADs.Catalog.Domain.Categories.Reads;

namespace CustomCADs.Catalog.Application.Categories.Queries.ExistsById;

public class CategoryExistsByIdHandler(ICategoryReads reads)
{
    public async Task<bool> Handle(CategoryExistsByIdQuery req, CancellationToken ct)
    {
        bool categoryExists = await reads.ExistsByIdAsync(req.Id, ct: ct).ConfigureAwait(false);

        return categoryExists;
    }
}
