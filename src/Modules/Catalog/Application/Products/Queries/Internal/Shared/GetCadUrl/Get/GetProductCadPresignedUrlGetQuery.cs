namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Shared.GetCadUrl.Get;

public sealed record GetProductCadPresignedUrlGetQuery(
    ProductId Id
) : IQuery<GetProductCadPresignedUrlGetDto>;
