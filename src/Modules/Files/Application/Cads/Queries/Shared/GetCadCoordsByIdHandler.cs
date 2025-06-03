using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Queries;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Files.Application.Cads.Queries.Shared;

public class GetCadCoordsByIdHandler(ICadReads reads)
	: IQueryHandler<GetCadCoordsByIdQuery, GetCadCoordsByIdDto>
{
	public async Task<GetCadCoordsByIdDto> Handle(GetCadCoordsByIdQuery req, CancellationToken ct)
	{
		Cad cad = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Cad>.ById(req.Id);

		return new(
			Cam: cad.CamCoordinates.ToDto(),
			Pan: cad.PanCoordinates.ToDto()
		);
	}
}
