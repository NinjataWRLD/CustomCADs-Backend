using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.GetImageUrlGet;

public sealed record GetProductImagePresignedUrlGetQuery(
    ProductId Id,
    AccountId CreatorId
) : IQuery<GetProductImagePresignedUrlGetDto>;
