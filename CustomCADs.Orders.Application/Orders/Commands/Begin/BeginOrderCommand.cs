using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Orders.Application.Orders.Commands.Begin;

public record BeginOrderCommand(
    OrderId Id,
    AccountId BeginnerId
) : ICommand;
