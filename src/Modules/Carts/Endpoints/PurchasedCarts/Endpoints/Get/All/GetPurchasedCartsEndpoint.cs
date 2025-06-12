using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetAll;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints.Get.All;

public sealed class GetPurchasedCartsEndpoint(IRequestSender sender)
	: Endpoint<GetPurchasedCartsRequest, Result<GetPurchasedCartsResponse>>
{
	public override void Configure()
	{
		Get("");
		Group<PurchasedCartsGroup>();
		Description(d => d
			.WithSummary("All")
			.WithDescription("See all your Carts with Sorting and Pagination options")
		);
	}

	public override async Task HandleAsync(GetPurchasedCartsRequest req, CancellationToken ct)
	{
		Result<GetAllPurchasedCartsDto> carts = await sender.SendQueryAsync(
			new GetAllPurchasedCartsQuery(
				BuyerId: User.GetAccountId(),
				PaymentStatus: req.PaymentStatus,
				Sorting: new(req.SortingType, req.SortingDirection),
				Pagination: new(req.Page, req.Limit)
			),
			ct
		).ConfigureAwait(false);

		Result<GetPurchasedCartsResponse> response = new(
			Count: carts.Count,
			Items: [.. carts.Items.Select(c => c.ToResponse())]
		);
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
