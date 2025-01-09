using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Remove;

public sealed record RemoveOngoingOrderCommand(
    OngoingOrderId Id,
    AccountId RemoverId
) : ICommand;
