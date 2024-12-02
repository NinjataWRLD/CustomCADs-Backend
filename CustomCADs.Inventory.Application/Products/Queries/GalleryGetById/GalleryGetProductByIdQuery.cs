namespace CustomCADs.Inventory.Application.Products.Queries.GalleryGetById;

public record GalleryGetProductByIdQuery(ProductId Id) : IQuery<GalleryGetProductByIdDto>;
