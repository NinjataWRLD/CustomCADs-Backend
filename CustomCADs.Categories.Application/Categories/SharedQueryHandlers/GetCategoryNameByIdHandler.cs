using CustomCADs.Categories.Domain.Categories;
using CustomCADs.Categories.Domain.Categories.Reads;
using CustomCADs.Categories.Domain.Common.Exceptions.Categories;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.Categories.Application.Categories.SharedQueryHandlers;

public class GetCategoryNameByIdHandler(ICategoryReads reads)
    : IQueryHandler<GetCategoryNameByIdQuery, string>
{
    public async Task<string> Handle(GetCategoryNameByIdQuery req, CancellationToken ct)
    {
        Category category = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CategoryNotFoundException.ById(req.Id);

        return category.Name;
    }
}
