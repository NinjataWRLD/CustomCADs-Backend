using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.CompletedOrders.Queries.GetCadUrlGet;

public sealed record GetCompletedOrderCadPresignedUrlGetQuery(
    CompletedOrderId Id,
    AccountId BuyerId
) : IQuery<GetCompletedOrderCadPresignedUrlGetDto>;
