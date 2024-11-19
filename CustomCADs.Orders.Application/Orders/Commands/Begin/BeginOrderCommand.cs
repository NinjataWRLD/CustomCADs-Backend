using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.Orders.Commands.Begin;

public record BeginOrderCommand(
    OrderId Id,
    UserId BeginnerId
) : ICommand;
