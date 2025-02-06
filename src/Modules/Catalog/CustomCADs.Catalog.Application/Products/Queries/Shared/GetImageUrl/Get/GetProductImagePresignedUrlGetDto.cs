namespace CustomCADs.Catalog.Application.Products.Queries.Shared.GetImageUrl.Get;

public record GetProductImagePresignedUrlGetDto(
    string PresignedUrl,
    string ContentType
);
