using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.Files.Application.Cads.Queries.Shared;

public class GetCadVolumeByIdHandler(ICadReads reads)
	: IQueryHandler<GetCadVolumeByIdQuery, decimal>
{
	public async Task<decimal> Handle(GetCadVolumeByIdQuery req, CancellationToken ct)
	{
		Cad cad = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Cad>.ById(req.Id);

		return cad.Volume;
	}
}
