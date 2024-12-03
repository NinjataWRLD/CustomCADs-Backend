using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.GetCadUrlGet;

public sealed record GetProductCadPresignedUrlGetQuery(
    ProductId Id,
    AccountId CreatorId
) : IQuery<GetProductCadPresignedUrlGetDto>;
