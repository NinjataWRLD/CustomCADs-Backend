namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;

public record CreatorGetProductCadPresignedUrlPostDto(
    string GeneratedKey,
    string PresignedUrl
);
