using CustomCADs.Files.Application.Cads.Storage;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.Files.Application.Cads.Queries.Shared;

public class GetCadPresignedUrlPostByIdHandler(ICadStorageService storage)
	: IQueryHandler<GetCadPresignedUrlPostByIdQuery, UploadFileResponse>
{
	public async Task<UploadFileResponse> Handle(GetCadPresignedUrlPostByIdQuery req, CancellationToken ct)
	{
		return await storage.GetPresignedPostUrlAsync(
			name: req.Name,
			file: req.File
		).ConfigureAwait(false);
	}
}
