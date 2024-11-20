namespace CustomCADs.Catalog.Application.Products.Queries.GetCadUrlPost;

public record GetProductCadPresignedUrlPostQuery(
    string ProductName,
    string ContentType,
    string FileName
) : IQuery<GetProductCadPresignedUrlPostDto>;
