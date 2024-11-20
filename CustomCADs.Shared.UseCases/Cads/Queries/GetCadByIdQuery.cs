using CadDto = (
    string Key,
    string ContentType,
    CustomCADs.Shared.Core.Dtos.CoordinatesDto CamCoordinates,
    CustomCADs.Shared.Core.Dtos.CoordinatesDto PanCoordinates
);

namespace CustomCADs.Shared.UseCases.Cads.Queries;

public record GetCadByIdQuery(CadId Id) : IQuery<CadDto>;
