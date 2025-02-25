namespace CustomCADs.Catalog.Application.Products.Queries.Shared.GetCadUrl.Post;

public record GetProductCadPresignedUrlPostDto(
    string GeneratedKey,
    string PresignedUrl
);
