using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Get;

public sealed record CreatorGetProductImagePresignedUrlGetQuery(
    ProductId Id,
    AccountId CreatorId
) : IQuery<CreatorGetProductImagePresignedUrlGetDto>;
