namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Shared.GetCadUrl.Post;

public record GetProductCadPresignedUrlPostDto(
    string GeneratedKey,
    string PresignedUrl
);
