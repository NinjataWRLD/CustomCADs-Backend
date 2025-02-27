namespace CustomCADs.Catalog.Application.Products.Queries.Shared.GetImageUrl.Get;

public sealed record GetProductImagePresignedUrlGetQuery(
    ProductId Id
) : IQuery<GetProductImagePresignedUrlGetDto>;
