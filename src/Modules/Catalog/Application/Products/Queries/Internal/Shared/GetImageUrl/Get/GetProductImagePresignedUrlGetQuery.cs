namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Shared.GetImageUrl.Get;

public sealed record GetProductImagePresignedUrlGetQuery(
    ProductId Id
) : IQuery<GetProductImagePresignedUrlGetDto>;
