using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Orders.Application.Orders.Commands.Delete;

public record DeleteOrderCommand(
    OrderId Id,
    UserId BuyerId
) : ICommand;
