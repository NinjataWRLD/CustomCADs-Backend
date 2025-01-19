using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.SetDelivery;

public record SetOngoingOrderDeliveryCommand(
    OngoingOrderId Id,
    bool Value,
    AccountId BuyerId
) : ICommand;
