namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Shared.GetCadUrl.Get;

public record GetProductCadPresignedUrlGetDto(
    string PresignedUrl,
    string ContentType
);
