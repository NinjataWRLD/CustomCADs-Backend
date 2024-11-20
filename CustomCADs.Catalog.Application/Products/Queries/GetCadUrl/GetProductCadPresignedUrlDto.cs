namespace CustomCADs.Catalog.Application.Products.Queries.GetCadUrl;

public record GetProductCadPresignedUrlDto(
    string CadKey,
    string CadUrl
);
