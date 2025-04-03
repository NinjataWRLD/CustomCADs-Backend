namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;

public sealed record CreatorGetProductCadPresignedUrlPostQuery(
    string ProductName,
    string ContentType,
    string FileName
) : IQuery<CreatorGetProductCadPresignedUrlPostDto>;
