﻿using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.Count.Carts;
using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.Count.Items;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints.Get.Stats;

public sealed class PurchasedCartsStatsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<PurchasedCartsStatsResponse>
{
	public override void Configure()
	{
		Get("stats");
		Group<PurchasedCartsGroup>();
		Description(d => d
			.WithSummary("Stats")
			.WithDescription("See your Carts' Stats")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		int totalCartCount = await sender.SendQueryAsync(
			new CountPurchasedCartsQuery(
				BuyerId: User.GetAccountId()
			),
			ct
	).ConfigureAwait(false);

		var counts = await sender.SendQueryAsync(
			new CountPurchasedCartItemsQuery(
				BuyerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		PurchasedCartsStatsResponse response = new(
			Total: totalCartCount,
			Counts: counts.ToDictionary(kv => kv.Key.Value, kv => kv.Value)
		);
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
