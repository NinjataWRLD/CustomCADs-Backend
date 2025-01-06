using CustomCADs.Carts.Application.PurchasedCarts.Queries.Count;
using CustomCADs.Carts.Application.PurchasedCarts.Queries.CountItems;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Get.Stats;

public sealed class PurchasedCartsStatsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<PurchasedCartsStatsResponse>
{
    public override void Configure()
    {
        Get("stats");
        Group<PurchasedCartsGroup>();
        Description(d => d
            .WithSummary("01. Stats")
            .WithDescription("See your Carts' Stats")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        CountPurchasedCartsQuery cartQuery = new(
            BuyerId: User.GetAccountId()
        );
        int totalCartCount = await sender.SendQueryAsync(cartQuery, ct).ConfigureAwait(false);

        CountPurchasedCartItemsQuery itemsQuery = new(
            BuyerId: User.GetAccountId()
        );
        var counts = await sender.SendQueryAsync(itemsQuery, ct).ConfigureAwait(false);

        PurchasedCartsStatsResponse response = new(
            TotalCount: totalCartCount,
            Counts: counts.ToDictionary(kv => kv.Key.Value, kv => kv.Value)
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
