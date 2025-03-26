using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Edit;

public sealed record EditOngoingOrderCommand(
    OngoingOrderId Id,
    string Name,
    string Description,
    AccountId BuyerId
) : ICommand;
