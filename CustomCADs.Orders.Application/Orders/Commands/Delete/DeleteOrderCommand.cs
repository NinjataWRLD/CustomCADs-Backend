using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.Delete;

public sealed record DeleteOrderCommand(
    OrderId Id,
    AccountId BuyerId
) : ICommand;
