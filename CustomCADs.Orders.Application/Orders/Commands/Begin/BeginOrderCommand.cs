using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.Begin;

public record BeginOrderCommand(
    OrderId Id,
    AccountId DesignerId
) : ICommand;
