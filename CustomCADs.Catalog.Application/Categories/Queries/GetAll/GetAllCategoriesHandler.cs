using CustomCADs.Catalog.Application.Common.Contracts;
using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Catalog.Domain.Categories.Reads;

namespace CustomCADs.Catalog.Application.Categories.Queries.GetAll;

public class GetAllCategoriesHandler(ICategoryReads reads)
    : IQueryHandler<GetAllCategoriesQuery, IEnumerable<CategoryReadDto>>
{
    public async Task<IEnumerable<CategoryReadDto>> Handle(GetAllCategoriesQuery req, CancellationToken ct)
    {
        IEnumerable<Category> categories = await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false);

        var response = categories.Select(c => new CategoryReadDto(c.Id, c.Name));
        return response;
    }
}
