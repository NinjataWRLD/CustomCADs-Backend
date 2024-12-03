using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.Complete;

public sealed record CompleteOrderCommand(
    OrderId Id,
    AccountId BuyerId
) : ICommand;
