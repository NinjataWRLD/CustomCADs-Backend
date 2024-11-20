namespace CustomCADs.Catalog.Application.Products.Queries.GetImageUrlPost;

public record GetProductImagePresignedUrlPostQuery(
    string ProductName,
    string ContentType,
    string FileName
) : IQuery<GetProductImagePresignedUrlPostDto>;
