namespace CustomCADs.Catalog.Application.Products.Queries.GetCadUrlGet;

public sealed record GetProductCadPresignedUrlGetQuery(
    ProductId Id
) : IQuery<GetProductCadPresignedUrlGetDto>;
