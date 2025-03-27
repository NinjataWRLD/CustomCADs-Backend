namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Shared.GetImageUrl.Post;

public sealed record GetProductImagePresignedUrlPostQuery(
    string ProductName,
    string ContentType,
    string FileName
) : IQuery<GetProductImagePresignedUrlPostDto>;
