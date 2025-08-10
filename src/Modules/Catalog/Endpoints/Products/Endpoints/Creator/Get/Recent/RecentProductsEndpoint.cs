using CustomCADs.Catalog.Application.Products.Enums;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetAll;
using CustomCADs.Shared.Domain.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.Recent;

public sealed class RecentProductsEndpoint(IRequestSender sender)
	: Endpoint<RecentProductsRequest, RecentProductsResponse[]>
{
	public override void Configure()
	{
		Get("recent");
		Group<CreatorGroup>();
		Description(d => d
			.WithSummary("Recent")
			.WithDescription("See your most recent Products")
		);
	}

	public override async Task HandleAsync(RecentProductsRequest req, CancellationToken ct)
	{
		Result<CreatorGetAllProductsDto> result = await sender.SendQueryAsync(
			new CreatorGetAllProductsQuery(
				CreatorId: User.GetAccountId(),
				Sorting: new(
					ProductCreatorSortingType.UploadedAt.ToBase(),
					SortingDirection.Descending
				),
				Pagination: new(Limit: req.Limit)
			),
			ct
		).ConfigureAwait(false);

		RecentProductsResponse[] response = [.. result.Items.Select(p => p.ToRecentResponse())];
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
