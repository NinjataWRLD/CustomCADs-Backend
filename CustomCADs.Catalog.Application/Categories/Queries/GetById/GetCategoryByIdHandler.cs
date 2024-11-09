using CustomCADs.Catalog.Domain.Categories.Reads;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Catalog.Application.Categories.Queries.GetById;

public class GetCategoryByIdHandler(ICategoryReads reads, ICacheService cache)
    : IQueryHandler<GetCategoryByIdQuery, CategoryReadDto>
{
    public async Task<CategoryReadDto> Handle(GetCategoryByIdQuery req, CancellationToken ct)
    {
        Category category =
            await cache.GetAsync<Category>($"categories/{req.Id}").ConfigureAwait(false)
            ?? await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw new CategoryNotFoundException(req.Id);

        CategoryReadDto response = new(category.Id, category.Name);
        return response;
    }
}
