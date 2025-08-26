using CustomCADs.Files.Application.Cads.Storage;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.Files.Application.Cads.Queries.Shared;

public class GetCadPresignedUrlPutByIdHandler(ICadReads reads, ICadStorageService storage, BaseCachingService<CadId, Cad> cache)
	: IQueryHandler<GetCadPresignedUrlPutByIdQuery, string>
{
	public async Task<string> Handle(GetCadPresignedUrlPutByIdQuery req, CancellationToken ct)
	{
		Cad cad = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Cad>.ById(req.Id)
		).ConfigureAwait(false);

		string url = await storage.GetPresignedPutUrlAsync(
			key: cad.Key,
			file: req.NewFile
		).ConfigureAwait(false);

		return url;
	}
}
