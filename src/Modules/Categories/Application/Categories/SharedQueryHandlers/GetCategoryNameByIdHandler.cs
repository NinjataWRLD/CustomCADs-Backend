using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.Categories.Application.Categories.SharedQueryHandlers;

public sealed class GetCategoryNameByIdHandler(ICategoryReads reads)
    : IQueryHandler<GetCategoryNameByIdQuery, string>
{
    public async Task<string> Handle(GetCategoryNameByIdQuery req, CancellationToken ct)
    {
        Category category = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Category>.ById(req.Id);

        return category.Name;
    }
}
