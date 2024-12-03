using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Inventory.Application.Products.Queries.GetImageUrlGet;

public sealed record GetProductImagePresignedUrlGetQuery(
    ProductId Id,
    AccountId CreatorId
) : IQuery<GetProductImagePresignedUrlGetDto>;
