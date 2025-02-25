using CustomCADs.Catalog.Application.Common.Enums;

namespace CustomCADs.Catalog.Application.Products.Queries.Gallery.GetSortings;

public class GetProductGallerySortingsHandler
    : IQueryHandler<GetProductGallerySortingsQuery, string[]>
{
    public Task<string[]> Handle(GetProductGallerySortingsQuery req, CancellationToken ct)
        => Task.FromResult(
                Enum.GetNames<ProductGallerySortingType>()
            );
}
