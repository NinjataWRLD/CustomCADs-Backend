using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;
using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Get.All;

public sealed class GetCustomsEndpoint(IRequestSender sender)
	: Endpoint<GetCustomsRequest, Result<GetCustomsResponse>>
{
	public override void Configure()
	{
		Get("");
		Group<DesignerGroup>();
		Description(d => d
			.WithSummary("All")
			.WithDescription("See all Customs with Filter, Search, Sort and Options options")
		);
	}

	public override async Task HandleAsync(GetCustomsRequest req, CancellationToken ct)
	{
		var customs = await sender.SendQueryAsync(
			new GetAllCustomsQuery(
				CustomStatus: CustomStatus.Finished,
				ForDelivery: req.ForDelivery,
				DesignerId: User.GetAccountId(),
				Name: req.Name,
				Sorting: new(req.SortingType, req.SortingDirection),
				Pagination: new(req.Page, req.Limit)
			),
			ct
		).ConfigureAwait(false);

		Result<GetCustomsResponse> response = new(
			Count: customs.Count,
			Items: [.. customs.Items.Select(o => o.ToResponse())]
		);
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
