using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;

public sealed class CreatorGetProductCadPresignedUrlPostHandler(IRequestSender sender)
	: IQueryHandler<CreatorGetProductCadPresignedUrlPostQuery, UploadFileResponse>
{
	public async Task<UploadFileResponse> Handle(CreatorGetProductCadPresignedUrlPostQuery req, CancellationToken ct)
	{
		return await sender.SendQueryAsync(
			new GetCadPresignedUrlPostByIdQuery(
				Name: req.ProductName,
				File: req.Cad
			),
			ct
		).ConfigureAwait(false);
	}
}
