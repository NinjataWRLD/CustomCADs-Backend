using CustomCADs.Shared.Application.Requests.Queries;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;
using CadDto = (
    string Key,
    string ContentType,
    CustomCADs.Shared.Core.Dtos.CoordinatesDto CamCoordinates,
    CustomCADs.Shared.Core.Dtos.CoordinatesDto PanCoordinates
);

namespace CustomCADs.Shared.Queries.Cads;

public record GetCadByIdQuery(CadId Id) : IQuery<CadDto>;
