using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Printing.Application.Materials.Queries.Internal.GetTextureUrl.Post;

public sealed class GetMaterialTexturePresignedUrlPostHandler(IRequestSender sender)
	: IQueryHandler<GetMaterialTexturePresignedUrlPostQuery, UploadFileResponse>
{
	public async Task<UploadFileResponse> Handle(GetMaterialTexturePresignedUrlPostQuery req, CancellationToken ct)
	{
		return await sender.SendQueryAsync(
			new GetImagePresignedUrlPostByIdQuery(
				Name: req.MaterialName,
				File: req.Image
			),
			ct
		).ConfigureAwait(false);
	}
}
