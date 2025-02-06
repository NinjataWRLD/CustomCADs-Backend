using CustomCADs.Catalog.Application.Common;

namespace CustomCADs.Catalog.Application.Products.Queries.GetSortings;

public class GetProductSortingsHandler
    : IQueryHandler<GetProductSortingsQuery, string[]>
{
    public Task<string[]> Handle(GetProductSortingsQuery req, CancellationToken ct)
        => Task.FromResult(
            Enum.GetNames<ProductSortingType>()
        );
}
