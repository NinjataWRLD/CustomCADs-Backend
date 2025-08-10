using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Endpoints.Attributes;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Post.PresignedUrls;

public sealed class GetProductPostPresignedUrlsEndpoint(IRequestSender sender)
	: Endpoint<GetProductPostPresignedUrlsRequest, GetProductPostPresignedUrlsResponse>
{
	public override void Configure()
	{
		Post("presignedUrls/upload");
		Group<CreatorGroup>();
		Description(d => d
			.WithSummary("Upload Image & Cad")
			.WithDescription("Upload the Image and Cad for a Product")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(GetProductPostPresignedUrlsRequest req, CancellationToken ct)
	{
		UploadFileResponse image = await sender.SendQueryAsync(
			new CreatorGetProductImagePresignedUrlPostQuery(
				ProductName: req.ProductName,
				Image: req.Image
			),
			ct
		).ConfigureAwait(false);

		UploadFileResponse cad = await sender.SendQueryAsync(
			new CreatorGetProductCadPresignedUrlPostQuery(
				ProductName: req.ProductName,
				Cad: req.Cad
			),
			ct
		).ConfigureAwait(false);

		GetProductPostPresignedUrlsResponse response = new(
			Image: image,
			Cad: cad
		);
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
