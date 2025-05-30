using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Queries;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Files.Application.Images.Queries.Shared;

public class GetImagePresignedUrlPutByIdHandler(IImageReads reads, IStorageService storage)
	: IQueryHandler<GetImagePresignedUrlPutByIdQuery, string>
{
	public async Task<string> Handle(GetImagePresignedUrlPutByIdQuery req, CancellationToken ct)
	{
		Image image = await reads.SingleByIdAsync(req.Id, track: false, ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Image>.ById(req.Id);

		string url = await storage.GetPresignedPutUrlAsync(
			key: image.Key,
			file: req.NewFile
		).ConfigureAwait(false);

		return url;
	}
}
