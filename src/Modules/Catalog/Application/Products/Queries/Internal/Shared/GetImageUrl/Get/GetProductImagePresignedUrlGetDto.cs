namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Shared.GetImageUrl.Get;

public record GetProductImagePresignedUrlGetDto(
    string PresignedUrl,
    string ContentType
);
