using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Delete;

public sealed record DeleteOngoingOrderCommand(
    OngoingOrderId Id,
    AccountId BuyerId
) : ICommand;
