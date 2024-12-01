using CustomCADs.Inventory.Application.Products.Queries.Count;

namespace CustomCADs.Inventory.Endpoints.Products.Get.Stats;

public class ProductsStatsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<ProductsStatsResponse>
{
    public override void Configure()
    {
        Get("stats");
        Group<ProductsGroup>();
        Description(d => d
            .WithSummary("04. Stats")
            .WithDescription("See your Products' stats")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        ProductsCountQuery query = new(CreatorId: User.GetAccountId());
        ProductsCountDto counts = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        ProductsStatsResponse response = new(
            UncheckedCount: counts.Unchecked,
            ValidatedCount: counts.Validated,
            ReportedCount: counts.Reported,
            BannedCount: counts.Banned
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
