using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.SetDelivery;

public record SetOngoingOrderDeliveryCommand(
    OngoingOrderId Id,
    bool Value,
    AccountId BuyerId
) : ICommand;
