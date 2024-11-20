using CustomCADs.Shared.Application.Requests.Commands;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;

namespace CustomCADs.Shared.Commands.Cads;

public record CreateCadCommand(string Key) : ICommand<CadId>;
