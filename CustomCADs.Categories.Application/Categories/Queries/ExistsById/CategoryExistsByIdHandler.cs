using CustomCADs.Categories.Domain.Categories.Reads;

namespace CustomCADs.Categories.Application.Categories.Queries.ExistsById;

public class CategoryExistsByIdHandler(ICategoryReads reads)
    : IQueryHandler<CategoryExistsByIdQuery, bool>
{
    public async Task<bool> Handle(CategoryExistsByIdQuery req, CancellationToken ct)
    {
        return await reads.ExistsByIdAsync(req.Id, ct: ct).ConfigureAwait(false);
    }
}
