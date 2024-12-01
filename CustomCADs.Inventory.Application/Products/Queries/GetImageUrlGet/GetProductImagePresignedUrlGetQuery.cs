using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Inventory.Application.Products.Queries.GetImageUrlGet;

public record GetProductImagePresignedUrlGetQuery(
    ProductId Id,
    AccountId CreatorId
) : IQuery<GetProductImagePresignedUrlGetDto>;
