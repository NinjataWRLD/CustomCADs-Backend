using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.Categories.Application.Categories.SharedQueryHandlers;

public class GetCategoryExistsByIdHandler(ICategoryReads reads)
    : IQueryHandler<GetCategoryExistsByIdQuery, bool>
{
    public async Task<bool> Handle(GetCategoryExistsByIdQuery req, CancellationToken ct)
    {
        return await reads.ExistsByIdAsync(req.Id, ct).ConfigureAwait(false);
    }
}
