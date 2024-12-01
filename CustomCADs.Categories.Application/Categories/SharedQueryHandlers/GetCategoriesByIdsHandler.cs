using CustomCADs.Categories.Domain.Categories;
using CustomCADs.Categories.Domain.Categories.Reads;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.Categories.Application.Categories.SharedQueryHandlers;

public class GetCategoriesByIdsHandler(ICategoryReads reads)
    : IQueryHandler<GetCategoriesByIdsQuery, IEnumerable<(CategoryId Id, string Name)>>
{
    public async Task<IEnumerable<(CategoryId Id, string Name)>> Handle(GetCategoriesByIdsQuery req, CancellationToken ct)
    {
        IEnumerable<Category> categories = await reads.AllAsync(track: false, ct: ct);

        return [.. categories
            .Where(c => req.Ids.Contains(c.Id))
            .Select(c => (c.Id, c.Name))
        ];
    }
}
