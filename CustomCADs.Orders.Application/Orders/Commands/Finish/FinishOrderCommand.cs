using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.Orders.Commands.Finish;

public record FinishOrderCommand(
    OrderId Id,
    UserId FinisherId,
    (string Key, string ContentType)? Cad
) : ICommand;
