using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Finish;

public sealed record FinishOngoingOrderCommand(
    OngoingOrderId Id,
    (string Key, string ContentType) Cad,
    AccountId DesignerId
) : ICommand;
