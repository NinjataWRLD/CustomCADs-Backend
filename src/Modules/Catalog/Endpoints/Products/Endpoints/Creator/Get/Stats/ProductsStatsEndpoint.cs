using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.Count;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.Stats;

public sealed class ProductsStatsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<ProductsStatsResponse>
{
	public override void Configure()
	{
		Get("stats");
		Group<CreatorGroup>();
		Description(d => d
			.WithSummary("Stats")
			.WithDescription("See your Products' stats")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		ProductsCountDto counts = await sender.SendQueryAsync(
			new ProductsCountQuery(
				CreatorId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		ProductsStatsResponse response = new(
			UncheckedCount: counts.Unchecked,
			ValidatedCount: counts.Validated,
			ReportedCount: counts.Reported,
			BannedCount: counts.Banned
		);
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
