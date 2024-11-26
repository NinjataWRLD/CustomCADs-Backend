using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Inventory.Application.Products.Queries.GetCadUrlGet;

public record GetProductCadPresignedUrlGetQuery(
    ProductId Id,
    UserId CreatorId
) : IQuery<GetProductCadPresignedUrlGetDto>;
