namespace CustomCADs.Catalog.Application.Products.Queries.Shared.GetImageUrl.Post;

public sealed record GetProductImagePresignedUrlPostQuery(
    string ProductName,
    string ContentType,
    string FileName
) : IQuery<GetProductImagePresignedUrlPostDto>;
