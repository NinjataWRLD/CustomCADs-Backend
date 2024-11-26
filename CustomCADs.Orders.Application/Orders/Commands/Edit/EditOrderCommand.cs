using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Orders.Application.Orders.Commands.Edit;

public record EditOrderCommand(
    OrderId Id,
    string Name,
    string Description,
    UserId BuyerId
) : ICommand;
