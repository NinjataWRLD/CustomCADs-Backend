using CustomCADs.Shared.Application.Requests.Commands;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;

namespace CustomCADs.Shared.Commands.Cads;

public record SetCadKeyCommand(
    CadId Id,
    string Key
) : ICommand;
