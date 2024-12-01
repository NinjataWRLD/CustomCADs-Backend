using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Inventory.Application.Products.Queries.GetCadUrlGet;

public record GetProductCadPresignedUrlGetQuery(
    ProductId Id,
    AccountId CreatorId
) : IQuery<GetProductCadPresignedUrlGetDto>;
