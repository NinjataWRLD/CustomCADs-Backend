using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.All;

public sealed class GetCustomsEndpoint(IRequestSender sender)
	: Endpoint<GetCustomsRequest, Result<GetCustomsResponse>>
{
	public override void Configure()
	{
		Get("");
		Group<CustomerGroup>();
		Description(d => d
			.WithSummary("All")
			.WithDescription("See all your Customs with Filter, Search, Sorting and Pagination options")
		);
	}

	public override async Task HandleAsync(GetCustomsRequest req, CancellationToken ct)
	{
		Result<GetAllCustomsDto> result = await sender.SendQueryAsync(
			new GetAllCustomsQuery(
				ForDelivery: req.ForDelivery,
				CustomStatus: req.Status,
				BuyerId: User.GetAccountId(),
				Name: req.Name,
				Sorting: new(req.SortingType, req.SortingDirection),
				Pagination: new(req.Page, req.Limit)
			),
			ct
		).ConfigureAwait(false);

		Result<GetCustomsResponse> response = new(
			Count: result.Count,
			Items: [.. result.Items.Select(o => o.ToGetResponse())]
		);
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
