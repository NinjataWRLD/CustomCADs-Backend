using CustomCADs.Shared.Core.Common.TypedIds.Cads;
using CadDto = (
    string Key,
    string ContentType,
    CustomCADs.Shared.Core.Common.Dtos.CoordinatesDto CamCoordinates,
    CustomCADs.Shared.Core.Common.Dtos.CoordinatesDto PanCoordinates
);

namespace CustomCADs.Shared.UseCases.Cads.Queries;

public record GetCadByIdQuery(CadId Id) : IQuery<CadDto>;
