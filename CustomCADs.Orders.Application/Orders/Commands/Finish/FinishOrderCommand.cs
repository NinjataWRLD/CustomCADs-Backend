using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;

namespace CustomCADs.Orders.Application.Orders.Commands.Finish;

public record FinishOrderCommand(
    OrderId Id,
    UserId FinisherId,
    CadId? CadId
) : ICommand;
