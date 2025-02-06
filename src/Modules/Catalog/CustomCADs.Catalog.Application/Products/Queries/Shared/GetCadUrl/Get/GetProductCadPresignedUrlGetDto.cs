namespace CustomCADs.Catalog.Application.Products.Queries.Shared.GetCadUrl.Get;

public record GetProductCadPresignedUrlGetDto(
    string PresignedUrl,
    string ContentType
);
