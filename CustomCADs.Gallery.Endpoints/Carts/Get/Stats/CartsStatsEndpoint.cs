using CustomCADs.Gallery.Application.Carts.Queries.Count;
using CustomCADs.Gallery.Application.Carts.Queries.CountItems;

namespace CustomCADs.Gallery.Endpoints.Carts.Get.Stats;

public class CartsStatsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<CartsStatsResponse>
{
    public override void Configure()
    {
        Get("stats");
        Group<CartsGroup>();
        Description(d => d
            .WithSummary("03. Stats")
            .WithDescription("See your Carts' Stats")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        CountCartQuery cartQuery = new(User.GetAccountId());
        int totalCartCount = await sender.SendQueryAsync(cartQuery, ct).ConfigureAwait(false);

        CountCartItemsQuery itemsQuery = new(User.GetAccountId());
        var counts = await sender.SendQueryAsync(itemsQuery, ct).ConfigureAwait(false);

        CartsStatsResponse response = new(
            TotalCount: totalCartCount,
            Counts: counts.ToDictionary(kv => kv.Key.Value, kv => kv.Value)
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
