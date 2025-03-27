using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.ClientGetById;

public sealed record ClientGetOngoingOrderByIdQuery(
    OngoingOrderId Id,
    AccountId BuyerId
) : IQuery<ClientGetOngoingOrderByIdDto>;
