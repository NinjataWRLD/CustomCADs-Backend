namespace CustomCADs.Catalog.Application.Products.Queries.GetCadUrlPost;

public record GetProductCadPresignedUrlPostDto(
    string GeneratedKey,
    string PresignedUrl
);
