using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.Finish;

public sealed record FinishOrderCommand(
    OrderId Id,
    AccountId DesignerId,
    (string Key, string ContentType)? Cad
) : ICommand;
