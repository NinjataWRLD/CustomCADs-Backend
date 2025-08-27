using CustomCADs.Files.Application.Images.Storage;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.UseCases.Images.Queries;

namespace CustomCADs.Files.Application.Images.Queries.Shared;

public class GetImagePresignedUrlPutByIdHandler(IImageReads reads, IImageStorageService storage, BaseCachingService<ImageId, Image> cache)
	: IQueryHandler<GetImagePresignedUrlPutByIdQuery, string>
{
	public async Task<string> Handle(GetImagePresignedUrlPutByIdQuery req, CancellationToken ct)
	{
		Image image = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Image>.ById(req.Id)
		).ConfigureAwait(false);

		string url = await storage.GetPresignedPutUrlAsync(
			key: image.Key,
			file: req.NewFile
		).ConfigureAwait(false);

		return url;
	}
}
