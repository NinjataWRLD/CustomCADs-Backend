using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Get;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Endpoints.Attributes;
using CustomCADs.Shared.Endpoints.Extensions;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.PresignedUrls.Image;

public sealed class GetProductGetPresignedImageUrlEndpoint(IRequestSender sender)
	: Endpoint<GetProductGetPresignedImageUrlRequest, DownloadFileResponse>
{
	public override void Configure()
	{
		Post("presignedUrls/download/image");
		Group<CreatorGroup>();
		Description(d => d
			.WithSummary("Download Image")
			.WithDescription("Download an Product's Image")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(GetProductGetPresignedImageUrlRequest req, CancellationToken ct)
	{
		DownloadFileResponse response = await sender.SendQueryAsync(
			new CreatorGetProductImagePresignedUrlGetQuery(
				Id: ProductId.New(req.Id),
				CreatorId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
