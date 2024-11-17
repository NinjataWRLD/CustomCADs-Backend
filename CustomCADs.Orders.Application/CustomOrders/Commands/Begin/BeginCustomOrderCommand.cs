using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.CustomOrders.Commands.Begin;

public record BeginCustomOrderCommand(
    CustomOrderId Id,
    UserId BeginnerId
) : ICommand;
