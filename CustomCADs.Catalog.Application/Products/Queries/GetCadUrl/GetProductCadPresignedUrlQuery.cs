namespace CustomCADs.Catalog.Application.Products.Queries.GetCadUrl;

public record GetProductCadPresignedUrlQuery(
    string ProductName,
    string ContentType,
    string FileName
) : IQuery<GetProductCadPresignedUrlDto>;
