using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Images.Queries;

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
