namespace CustomCADs.Catalog.Application.Products.Queries.GetCadUrlGet;

public record GetProductCadPresignedUrlGetDto(
    string PresignedUrl,
    string ContentType
);
