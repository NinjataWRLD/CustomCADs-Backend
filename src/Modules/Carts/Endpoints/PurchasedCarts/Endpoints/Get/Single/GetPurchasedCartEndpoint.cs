using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetById;
using CustomCADs.Shared.Domain.TypedIds.Carts;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints.Get.Single;

public sealed class GetPurchasedCartEndpoint(IRequestSender sender)
	: Endpoint<GetPurchasedCartRequest, GetPurchasedCartResponse>
{
	public override void Configure()
	{
		Get("{id}");
		Group<PurchasedCartsGroup>();
		Description(d => d
			.WithSummary("Single")
			.WithDescription("See your Cart in detail")
		);
	}

	public override async Task HandleAsync(GetPurchasedCartRequest req, CancellationToken ct)
	{
		GetPurchasedCartByIdDto cart = await sender.SendQueryAsync(
			new GetPurchasedCartByIdQuery(
				Id: PurchasedCartId.New(req.Id),
				BuyerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		GetPurchasedCartResponse response = cart.ToResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
