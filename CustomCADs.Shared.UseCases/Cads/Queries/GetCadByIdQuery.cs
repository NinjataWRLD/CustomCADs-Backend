using CadDto = (
    string Key,
    string ContentType,
    CustomCADs.Shared.Core.Common.Dtos.CoordinatesDto CamCoordinates,
    CustomCADs.Shared.Core.Common.Dtos.CoordinatesDto PanCoordinates
);

namespace CustomCADs.Shared.UseCases.Cads.Queries;

public sealed record GetCadByIdQuery(
    CadId Id
) : IQuery<CadDto>;
