using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Shared.UseCases.Cads.Queries;

public record GetCadByCoordsIdQuery(
    CadId Id
) : IQuery<(CoordinatesDto Cam, CoordinatesDto Pan)>;
