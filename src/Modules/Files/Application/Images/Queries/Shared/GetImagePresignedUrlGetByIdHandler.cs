using CustomCADs.Files.Application.Images.Storage;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Images.Queries;

namespace CustomCADs.Files.Application.Images.Queries.Shared;

public class GetImagePresignedUrlGetByIdHandler(IImageReads reads, IImageStorageService storage, BaseCachingService<ImageId, Image> cache)
	: IQueryHandler<GetImagePresignedUrlGetByIdQuery, DownloadFileResponse>
{
	public async Task<DownloadFileResponse> Handle(GetImagePresignedUrlGetByIdQuery req, CancellationToken ct)
	{
		Image image = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Image>.ById(req.Id)
		).ConfigureAwait(false);

		string url = await storage.GetPresignedGetUrlAsync(image.Key).ConfigureAwait(false);

		return new(
			PresignedUrl: url,
			ContentType: image.ContentType
		);
	}
}
