using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.CompletedOrders.Queries.ClientGetById;

public sealed record ClientGetCompletedOrderByIdQuery(
    CompletedOrderId Id,
    AccountId BuyerId
) : IQuery<ClientGetCompletedOrderByIdDto>;
