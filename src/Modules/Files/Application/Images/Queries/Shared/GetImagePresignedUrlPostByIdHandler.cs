using CustomCADs.Files.Application.Images.Storage;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Images.Queries;

namespace CustomCADs.Files.Application.Images.Queries.Shared;

public class GetImagePresignedUrlPostByIdHandler(IImageStorageService storage)
	: IQueryHandler<GetImagePresignedUrlPostByIdQuery, UploadFileResponse>
{
	public async Task<UploadFileResponse> Handle(GetImagePresignedUrlPostByIdQuery req, CancellationToken ct)
	{
		return await storage.GetPresignedPostUrlAsync(
			name: req.Name,
			file: req.File
		).ConfigureAwait(false);
	}
}
