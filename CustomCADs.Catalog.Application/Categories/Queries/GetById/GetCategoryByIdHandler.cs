using CustomCADs.Catalog.Application.Common.Contracts;
using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Catalog.Domain.Categories.Reads;

namespace CustomCADs.Catalog.Application.Categories.Queries.GetById;

public class GetCategoryByIdHandler(ICategoryReads reads)
    : IQueryHandler<GetCategoryByIdQuery, CategoryReadDto>
{
    public async Task<CategoryReadDto> Handle(GetCategoryByIdQuery req, CancellationToken ct)
    {
        Category? category = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw new CategoryNotFoundException(req.Id);

        CategoryReadDto response = new(category.Id, category.Name);
        return response;
    }
}
