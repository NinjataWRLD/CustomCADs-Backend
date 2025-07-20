using CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetUrlGet.Cad;
using CustomCADs.Shared.Core.Common.Dtos;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Get.PresignedUrls.Cad;

public sealed class GetProductGetPresignedUrlsEndpoint(IRequestSender sender)
	: Endpoint<GetProductGetPresignedUrlsRequest, DownloadFileResponse>
{
	public override void Configure()
	{
		Post("presignedUrls/download/cad");
		Group<GalleryGroup>();
		Description(d => d
			.WithSummary("Download Cad")
			.WithDescription("Download the Cad for a Product")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(GetProductGetPresignedUrlsRequest req, CancellationToken ct)
	{
		DownloadFileResponse response = await sender.SendQueryAsync(
			new GalleryGetProductCadPresignedUrlGetQuery(
				Id: ProductId.New(req.Id)
			),
			ct
		).ConfigureAwait(false);

		await SendOkAsync(response).ConfigureAwait(false);
	}
}
