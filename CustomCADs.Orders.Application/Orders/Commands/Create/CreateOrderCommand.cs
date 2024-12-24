using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.Create;

public sealed record CreateOrderCommand(
    string Name,
    string Description,
    AccountId BuyerId
) : ICommand<OrderId>;
