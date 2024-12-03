using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.Edit;

public sealed record EditOrderCommand(
    OrderId Id,
    string Name,
    string Description,
    AccountId BuyerId
) : ICommand;
