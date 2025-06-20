﻿using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetAll;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Get.All;

public sealed class GetActiveCartItemsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<ICollection<ActiveCartItemResponse>>
{
	public override void Configure()
	{
		Get("");
		Group<ActiveCartsGroup>();
		Description(d => d
			.WithSummary("All")
			.WithDescription("See all your Cart Items")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		ActiveCartItemDto[] cart = await sender.SendQueryAsync(
			new GetActiveCartItemsQuery(
				BuyerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		ICollection<ActiveCartItemResponse> response = [.. cart.Select(x => x.ToResponse())];
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
