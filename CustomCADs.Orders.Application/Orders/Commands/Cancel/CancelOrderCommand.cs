using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Orders.Application.Orders.Commands.Cancel;

public record CancelOrderCommand(
    OrderId Id,
    AccountId CancellerId
) : ICommand;
