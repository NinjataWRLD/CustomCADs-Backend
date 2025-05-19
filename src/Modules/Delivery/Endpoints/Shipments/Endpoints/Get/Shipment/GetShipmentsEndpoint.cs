using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetAll;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Delivery.Endpoints.Shipments.Endpoints.Get.Shipment;

public class GetShipmentsEndpoint(IRequestSender sender)
	: Endpoint<GetShipmentsRequest, Result<GetShipmentsResponse>>
{
	public override void Configure()
	{
		Get("");
		Group<ShipmentsGroup>();
		Description(d => d
			.WithSummary("All")
			.WithDescription("See all your Shipments with Filter, Search, Sorting and Pagination options")
		);
	}

	public override async Task HandleAsync(GetShipmentsRequest req, CancellationToken ct)
	{
		Result<GetAllShipmentsDto> result = await sender.SendQueryAsync(
			new GetAllShipmentsQuery(
				CustomerId: User.GetAccountId(),
				Sorting: new(req.SortingType, req.SortingDirection),
				Pagination: new(req.Page, req.Limit)
			),
			ct
		).ConfigureAwait(false);

		Result<GetShipmentsResponse> response = new(
			Count: result.Count,
			Items: [.. result.Items.Select(i => i.ToResponse())]
		);
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
