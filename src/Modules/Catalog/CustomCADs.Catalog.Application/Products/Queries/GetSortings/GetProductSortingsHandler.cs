using CustomCADs.Accounts.Application.Accounts.Queries.GetSortings;
using CustomCADs.Catalog.Domain.Products.Enums;

namespace CustomCADs.Catalog.Application.Products.Queries.GetSortings;

public class GetProductSortingsHandler
    : IQueryHandler<GetProductSortingsQuery, string[]>
{
    public Task<string[]> Handle(GetProductSortingsQuery req, CancellationToken ct)
        => Task.FromResult(
            Enum.GetNames<ProductSortingType>()
        );
}
