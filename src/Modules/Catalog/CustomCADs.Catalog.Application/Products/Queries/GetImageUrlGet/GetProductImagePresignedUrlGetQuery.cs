namespace CustomCADs.Catalog.Application.Products.Queries.GetImageUrlGet;

public sealed record GetProductImagePresignedUrlGetQuery(
    ProductId Id
) : IQuery<GetProductImagePresignedUrlGetDto>;
