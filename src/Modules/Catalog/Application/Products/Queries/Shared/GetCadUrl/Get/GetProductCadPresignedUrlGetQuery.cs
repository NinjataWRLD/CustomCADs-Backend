namespace CustomCADs.Catalog.Application.Products.Queries.Shared.GetCadUrl.Get;

public sealed record GetProductCadPresignedUrlGetQuery(
    ProductId Id
) : IQuery<GetProductCadPresignedUrlGetDto>;
