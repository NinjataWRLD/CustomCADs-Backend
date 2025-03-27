using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.CompletedOrders.Queries.Internal.GetCadUrlGet;

public sealed record GetCompletedOrderCadPresignedUrlGetQuery(
    CompletedOrderId Id,
    AccountId BuyerId
) : IQuery<GetCompletedOrderCadPresignedUrlGetDto>;
