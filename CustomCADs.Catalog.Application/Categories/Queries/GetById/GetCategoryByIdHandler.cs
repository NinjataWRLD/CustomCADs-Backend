using CustomCADs.Catalog.Application.Categories.Common;
using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Catalog.Domain.Categories.Reads;
using Mapster;
using MediatR;

namespace CustomCADs.Catalog.Application.Categories.Queries.GetById;

public class GetCategoryByIdHandler(ICategoryReads reads) : IRequestHandler<GetCategoryByIdQuery, CategoryReadDto>
{
    public async Task<CategoryReadDto> Handle(GetCategoryByIdQuery req, CancellationToken ct)
    {
        Category? category = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw new CategoryNotFoundException(req.Id);

        var response = category.Adapt<CategoryReadDto>();
        return response;
    }
}
