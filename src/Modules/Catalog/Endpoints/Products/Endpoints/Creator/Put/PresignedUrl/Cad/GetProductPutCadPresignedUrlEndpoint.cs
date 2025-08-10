using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;
using CustomCADs.Shared.Endpoints.Attributes;
using CustomCADs.Shared.Endpoints.Extensions;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Put.PresignedUrl.Cad;

public sealed class GetProductPutCadPresignedUrlEndpoint(IRequestSender sender)
	: Endpoint<GetProductPutCadPresignedUrlRequest, GetProductPutCadPresignedUrlResponse>
{
	public override void Configure()
	{
		Post("presignedUrls/replace/cad");
		Group<CreatorGroup>();
		Description(d => d
			.WithSummary("Change Cad")
			.WithDescription("Change your Product's Cad")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(GetProductPutCadPresignedUrlRequest req, CancellationToken ct)
	{
		var cadDto = await sender.SendQueryAsync(
			new CreatorGetProductCadPresignedUrlPutQuery(
				Id: ProductId.New(req.Id),
				NewCad: req.File,
				CreatorId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		GetProductPutCadPresignedUrlResponse response = new(cadDto.PresignedUrl);
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
