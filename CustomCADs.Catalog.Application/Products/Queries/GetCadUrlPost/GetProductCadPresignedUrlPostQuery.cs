namespace CustomCADs.Catalog.Application.Products.Queries.GetCadUrlPost;

public sealed record GetProductCadPresignedUrlPostQuery(
    string ProductName,
    string ContentType,
    string FileName
) : IQuery<GetProductCadPresignedUrlPostDto>;
