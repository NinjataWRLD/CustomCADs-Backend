using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.Files.Application.Cads.Queries.Shared;

public class GetCadCoordsByIdHandler(ICadReads reads, BaseCachingService<CadId, Cad> cache)
	: IQueryHandler<GetCadCoordsByIdQuery, GetCadCoordsByIdDto>
{
	public async Task<GetCadCoordsByIdDto> Handle(GetCadCoordsByIdQuery req, CancellationToken ct)
	{
		Cad cad = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Cad>.ById(req.Id)
		).ConfigureAwait(false);

		return new(
			Cam: cad.CamCoordinates.ToDto(),
			Pan: cad.PanCoordinates.ToDto()
		);
	}
}
