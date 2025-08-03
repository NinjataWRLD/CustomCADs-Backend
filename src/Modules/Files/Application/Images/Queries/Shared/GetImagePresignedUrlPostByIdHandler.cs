using CustomCADs.Files.Application.Images.Storage;
using CustomCADs.Shared.Abstractions.Requests.Queries;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Images.Queries;

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
