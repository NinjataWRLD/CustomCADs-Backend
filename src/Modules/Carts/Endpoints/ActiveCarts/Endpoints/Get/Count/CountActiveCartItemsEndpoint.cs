using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.Count;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Get.Count;

public sealed class CountActiveCartItemsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<int>
{
	public override void Configure()
	{
		Get("count");
		Group<ActiveCartsGroup>();
		Description(d => d
			.WithSummary("Count")
			.WithDescription("Count your Cart Items")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		int count = await sender.SendQueryAsync(
			new CountActiveCartItemsQuery(
				BuyerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		await SendOkAsync(count).ConfigureAwait(false);
	}
}
