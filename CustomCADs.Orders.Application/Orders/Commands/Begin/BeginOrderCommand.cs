using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.Begin;

public sealed record BeginOrderCommand(
    OrderId Id,
    AccountId DesignerId
) : ICommand;
