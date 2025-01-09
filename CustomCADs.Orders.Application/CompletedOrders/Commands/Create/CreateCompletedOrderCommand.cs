using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.CompletedOrders.Commands.Create;

public record CreateCompletedOrderCommand(
    string Name,
    string Description,
    AccountId BuyerId
) : ICommand<CompletedOrderId>;
