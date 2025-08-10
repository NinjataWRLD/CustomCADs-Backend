using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put;
using CustomCADs.Shared.Endpoints.Attributes;
using CustomCADs.Shared.Endpoints.Extensions;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Put.PresignedUrl.Image;

public sealed class GetProductPutPresignedUrlEndpoint(IRequestSender sender)
	: Endpoint<GetProductPutPresignedUrlRequest, GetProductPutPresignedUrlResponse>
{
	public override void Configure()
	{
		Post("presignedUrls/replace/image");
		Group<CreatorGroup>();
		Description(d => d
			.WithSummary("Change Image")
			.WithDescription("Change your Product's Image")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(GetProductPutPresignedUrlRequest req, CancellationToken ct)
	{
		var imageDto = await sender.SendQueryAsync(
			new CreatorGetProductImagePresignedUrlPutQuery(
				Id: ProductId.New(req.Id),
				NewImage: req.File,
				CreatorId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		GetProductPutPresignedUrlResponse response = new(imageDto.PresignedUrl);
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
