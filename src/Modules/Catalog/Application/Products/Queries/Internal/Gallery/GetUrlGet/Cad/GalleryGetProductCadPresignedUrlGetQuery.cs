namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetUrlGet.Cad;

public sealed record GalleryGetProductCadPresignedUrlGetQuery(
    ProductId Id
) : IQuery<GalleryGetProductCadPresignedUrlGetDto>;
