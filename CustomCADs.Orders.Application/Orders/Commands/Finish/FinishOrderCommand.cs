using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Orders.Application.Orders.Commands.Finish;

public record FinishOrderCommand(
    OrderId Id,
    AccountId FinisherId,
    (string Key, string ContentType)? Cad
) : ICommand;
