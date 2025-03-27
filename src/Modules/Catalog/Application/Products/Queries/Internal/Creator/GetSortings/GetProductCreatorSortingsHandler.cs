using CustomCADs.Catalog.Application.Products.Enums;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetSortings;

public class GetProductCreatorSortingsHandler
    : IQueryHandler<GetProductCreatorSortingsQuery, string[]>
{
    public Task<string[]> Handle(GetProductCreatorSortingsQuery req, CancellationToken ct)
        => Task.FromResult(
                Enum.GetNames<ProductCreatorSortingType>()
            );
}
