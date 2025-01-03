using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Queries.GetCadUrlGet;

public sealed record GetOrderCadPresignedUrlGetQuery(
    OrderId Id,
    AccountId BuyerId
) : IQuery<GetOrderCadPresignedUrlGetDto>;
