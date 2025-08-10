using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Shared.Application.UseCases.Cads.Queries;

public record GetCadCoordsByIdQuery(
	CadId Id
) : IQuery<GetCadCoordsByIdDto>;

public record GetCadCoordsByIdDto(
	CoordinatesDto Cam,
	CoordinatesDto Pan
);
