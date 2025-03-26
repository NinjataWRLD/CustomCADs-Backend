using CustomCADs.Catalog.Application.Products.Enums;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetSortings;

public class GetProductGallerySortingsHandler
    : IQueryHandler<GetProductGallerySortingsQuery, string[]>
{
    public Task<string[]> Handle(GetProductGallerySortingsQuery req, CancellationToken ct)
        => Task.FromResult(
                Enum.GetNames<ProductGallerySortingType>()
            );
}
