using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;
using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.Recent;

public sealed class RecentCustomsEndpoint(IRequestSender sender)
	: Endpoint<RecentCustomsRequest, RecentCustomsResponse[]>
{
	public override void Configure()
	{
		Get("recent");
		Group<CustomerGroup>();
		Description(d => d
			.WithSummary("Recent")
			.WithDescription("See your most recent Customs")
		);
	}

	public override async Task HandleAsync(RecentCustomsRequest req, CancellationToken ct)
	{
		Result<GetAllCustomsDto> result = await sender.SendQueryAsync(
			new GetAllCustomsQuery(
				BuyerId: User.GetAccountId(),
				Sorting: new(CustomSortingType.OrderedAt, SortingDirection.Descending),
				Pagination: new(Limit: req.Limit)
			),
			ct
		).ConfigureAwait(false);

		RecentCustomsResponse[] response =
			[.. result.Items.Select(o => o.ToRecentResponse())];
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
