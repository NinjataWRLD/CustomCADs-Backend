using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.Cancel;

public record CancelOrderCommand(
    OrderId Id,
    AccountId DesignerId
) : ICommand;
