namespace CustomCADs.Catalog.Application.Products.Queries.GetImageUrl;

public record GetProductImagePresignedUrlQuery(
    string ProductName,
    string ContentType,
    string FileName
) : IQuery<GetProductImagePresignedUrlDto>;
