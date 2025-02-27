using CustomCADs.Catalog.Application.Common.Enums;

namespace CustomCADs.Catalog.Application.Products.Queries.Creator.GetSortings;

public class GetProductCreatorSortingsHandler
    : IQueryHandler<GetProductCreatorSortingsQuery, string[]>
{
    public Task<string[]> Handle(GetProductCreatorSortingsQuery req, CancellationToken ct)
        => Task.FromResult(
                Enum.GetNames<ProductCreatorSortingType>()
            );
}
