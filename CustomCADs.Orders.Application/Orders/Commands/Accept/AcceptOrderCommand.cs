using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.Accept;

public sealed record AcceptOrderCommand(
    OrderId Id,
    AccountId DesignerId
) : ICommand;
