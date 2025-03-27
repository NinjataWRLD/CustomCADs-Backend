namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Shared.GetCadUrl.Post;

public sealed record GetProductCadPresignedUrlPostQuery(
    string ProductName,
    string ContentType,
    string FileName
) : IQuery<GetProductCadPresignedUrlPostDto>;
