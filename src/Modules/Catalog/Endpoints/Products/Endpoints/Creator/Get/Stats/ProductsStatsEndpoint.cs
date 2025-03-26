using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.Count;
using CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator;

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
        ProductsCountQuery query = new(
            CreatorId: User.GetAccountId()
        );
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
