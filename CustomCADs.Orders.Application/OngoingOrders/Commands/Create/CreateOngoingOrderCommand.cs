using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Create;

public sealed record CreateOngoingOrderCommand(
    string Name,
    string Description,
    bool Delivery,
    AccountId BuyerId
) : ICommand<OngoingOrderId>;
