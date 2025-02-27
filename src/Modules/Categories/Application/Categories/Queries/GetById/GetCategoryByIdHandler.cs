using CustomCADs.Categories.Application.Common.Exceptions;
using CustomCADs.Categories.Domain.Categories.Reads;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.Categories.Application.Categories.Queries.GetById;

public sealed class GetCategoryByIdHandler(ICategoryReads reads, ICacheService cache)
    : IQueryHandler<GetCategoryByIdQuery, CategoryReadDto>
{
    public async Task<CategoryReadDto> Handle(GetCategoryByIdQuery req, CancellationToken ct)
    {
        Category? category = await cache.GetCategoryAsync(req.Id).ConfigureAwait(false);

        if (category is null)
        {
            category = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
                ?? throw CategoryNotFoundException.ById(req.Id);

            await cache.SetCategoryAsync(category.Id, category).ConfigureAwait(false);
        }

        return category.ToCategoryReadDto();
    }
}
