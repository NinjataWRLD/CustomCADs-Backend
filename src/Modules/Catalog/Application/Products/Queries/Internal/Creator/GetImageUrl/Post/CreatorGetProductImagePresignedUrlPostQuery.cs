namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;

public sealed record CreatorGetProductImagePresignedUrlPostQuery(
    string ProductName,
    string ContentType,
    string FileName
) : IQuery<CreatorGetProductImagePresignedUrlPostDto>;
