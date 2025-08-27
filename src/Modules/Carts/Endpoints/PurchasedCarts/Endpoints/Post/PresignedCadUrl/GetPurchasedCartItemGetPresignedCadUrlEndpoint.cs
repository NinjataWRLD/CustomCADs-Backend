using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetCadUrlGet;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Domain.TypedIds.Carts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Endpoints.Attributes;
using CustomCADs.Shared.Endpoints.Extensions;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints.Post.PresignedCadUrl;

public sealed class GetPurchasedCartItemGetPresignedCadUrlEndpoint(IRequestSender sender)
	: Endpoint<GetPurchasedCartItemGetPresignedCadUrlRequest, GetPurchasedCartItemCadPresignedUrlGetDto>
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
		GetPurchasedCartItemCadPresignedUrlGetDto response = await sender.SendQueryAsync(
			new GetPurchasedCartItemCadPresignedUrlGetQuery(
				Id: PurchasedCartId.New(req.Id),
				ProductId: ProductId.New(req.ProductId),
				BuyerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
