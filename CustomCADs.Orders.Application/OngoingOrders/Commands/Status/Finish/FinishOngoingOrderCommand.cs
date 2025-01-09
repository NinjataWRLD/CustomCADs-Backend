using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Finish;

public sealed record FinishOngoingOrderCommand(
    OngoingOrderId Id,
    AccountId DesignerId,
    (string Key, string ContentType) Cad
) : ICommand<FinishOngoingOrderDto>;
