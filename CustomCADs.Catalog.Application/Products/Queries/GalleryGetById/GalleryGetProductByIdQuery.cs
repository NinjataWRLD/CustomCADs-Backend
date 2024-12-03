namespace CustomCADs.Catalog.Application.Products.Queries.GalleryGetById;

public sealed record GalleryGetProductByIdQuery(
    ProductId Id
) : IQuery<GalleryGetProductByIdDto>;
