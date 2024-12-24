using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.CreateWithDelivery;

public sealed record CreateOrderWithDeliveryCommand(
    string Name,
    string Description,
    AccountId BuyerId
) : ICommand<OrderId>;
