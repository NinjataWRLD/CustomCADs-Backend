namespace CustomCADs.Inventory.Application.Products.Queries.GalleryGetById;

public sealed record GalleryGetProductByIdQuery(
    ProductId Id
) : IQuery<GalleryGetProductByIdDto>;
