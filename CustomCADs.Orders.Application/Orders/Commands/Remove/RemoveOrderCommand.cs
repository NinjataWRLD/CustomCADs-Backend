using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.Remove;

public sealed record RemoveOrderCommand(
    OrderId Id,
    AccountId RemoverId
) : ICommand;
