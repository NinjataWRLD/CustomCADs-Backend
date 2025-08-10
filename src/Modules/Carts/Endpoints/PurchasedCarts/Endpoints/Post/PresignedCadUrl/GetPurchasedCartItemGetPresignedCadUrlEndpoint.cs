using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetCadUrlGet;
using CustomCADs.Shared.Domain.TypedIds.Carts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Endpoints.Attributes;
using CustomCADs.Shared.Endpoints.Extensions;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints.Post.PresignedCadUrl;

public sealed class GetPurchasedCartItemGetPresignedCadUrlEndpoint(IRequestSender sender)
	: Endpoint<GetPurchasedCartItemGetPresignedCadUrlRequest, GetPurchasedCartItemGetPresignedCadUrlResponse>
{
	public override void Configure()
	{
		Post("presignedUrls/download/response");
		Group<PurchasedCartsGroup>();
		Description(d => d
			.WithSummary("Download Cad")
			.WithDescription("Download your Cart Item's Cad")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(GetPurchasedCartItemGetPresignedCadUrlRequest req, CancellationToken ct)
	{
		var (PresignedUrl, ContentType, Cam, Pan) = await sender.SendQueryAsync(
			new GetPurchasedCartItemCadPresignedUrlGetQuery(
				Id: PurchasedCartId.New(req.Id),
				ProductId: ProductId.New(req.ProductId),
				BuyerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		GetPurchasedCartItemGetPresignedCadUrlResponse response = new(
			PresignedUrl: PresignedUrl,
			ContentType: ContentType,
			CamCoordinates: Cam,
			PanCoordinates: Pan
		);
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
