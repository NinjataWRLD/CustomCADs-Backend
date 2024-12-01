using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.Remove;

public record RemoveOrderCommand(
    OrderId Id,
    AccountId RemoverId
) : ICommand;
