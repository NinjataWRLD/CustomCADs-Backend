using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.Create;

public sealed record CreateOrderCommand(
    bool Delivery,
    string Name,
    string Description,
    AccountId BuyerId
) : ICommand<OrderId>;
