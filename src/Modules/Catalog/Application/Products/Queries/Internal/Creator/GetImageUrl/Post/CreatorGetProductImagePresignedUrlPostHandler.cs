using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Images.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;

public sealed class CreatorGetProductImagePresignedUrlPostHandler(IRequestSender sender)
	: IQueryHandler<CreatorGetProductImagePresignedUrlPostQuery, UploadFileResponse>
{
	public async Task<UploadFileResponse> Handle(CreatorGetProductImagePresignedUrlPostQuery req, CancellationToken ct)
	{
		return await sender.SendQueryAsync(
			new GetImagePresignedUrlPostByIdQuery(
				Name: req.ProductName,
				File: req.Image
			),
			ct
		).ConfigureAwait(false);
	}
}
