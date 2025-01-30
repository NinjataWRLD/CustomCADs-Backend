using CustomCADs.Carts.Application.PurchasedCarts.Queries.GetSortings;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Get.Sortings;

public sealed class GetPurchasedCartSortingsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<string[]>
{
    public override void Configure()
    {
        Get("sortings");
        Group<PurchasedCartsGroup>();
        Description(d => d
            .WithSummary("05. Sortings")
            .WithDescription("See all Purchased Cart Sorting types")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        GetPurchasedCartSortingsQuery query = new();
        string[] result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        await SendOkAsync(result).ConfigureAwait(false);
    }
}
