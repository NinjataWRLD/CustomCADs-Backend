using CustomCADs.Files.Application.Cads.Storage;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.Files.Application.Cads.Queries.Shared;

public class GetCadPresignedUrlGetByIdHandler(ICadReads reads, ICadStorageService storage, BaseCachingService<CadId, Cad> cache)
	: IQueryHandler<GetCadPresignedUrlGetByIdQuery, DownloadFileResponse>
{
	public async Task<DownloadFileResponse> Handle(GetCadPresignedUrlGetByIdQuery req, CancellationToken ct)
	{
		Cad cad = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Cad>.ById(req.Id)
		).ConfigureAwait(false);

		string url = await storage.GetPresignedGetUrlAsync(
			key: cad.Key,
			contentType: cad.ContentType
		).ConfigureAwait(false);

		return new(
			PresignedUrl: url,
			ContentType: cad.ContentType
		);
	}
}
