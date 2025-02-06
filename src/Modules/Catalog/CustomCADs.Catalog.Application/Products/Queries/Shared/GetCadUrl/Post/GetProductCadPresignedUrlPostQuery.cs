namespace CustomCADs.Catalog.Application.Products.Queries.Shared.GetCadUrl.Post;

public sealed record GetProductCadPresignedUrlPostQuery(
    string ProductName,
    string ContentType,
    string FileName
) : IQuery<GetProductCadPresignedUrlPostDto>;
