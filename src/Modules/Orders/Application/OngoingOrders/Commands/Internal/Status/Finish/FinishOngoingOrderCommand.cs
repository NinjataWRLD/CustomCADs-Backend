using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Status.Finish;

public sealed record FinishOngoingOrderCommand(
    OngoingOrderId Id,
    (string Key, string ContentType, decimal Volume) Cad,
    decimal Price,
    AccountId DesignerId
) : ICommand;
