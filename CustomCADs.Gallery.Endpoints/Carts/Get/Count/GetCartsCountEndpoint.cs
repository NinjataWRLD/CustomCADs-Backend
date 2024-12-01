using CustomCADs.Gallery.Application.Carts.Queries.Count;
using CustomCADs.Gallery.Application.Carts.Queries.CountItems;

namespace CustomCADs.Gallery.Endpoints.Carts.Get.Count;

public class GetCartsCountEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<GetCartsCountResponse>
{
    public override void Configure()
    {
        Get("count");
        Group<CartsGroup>();
        Description(d => d
            .WithSummary("Count")
            .WithDescription("See the total count of Carts and each one's Items count")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        CountCartQuery cartQuery = new(User.GetAccountId());
        int totalCartCount = await sender.SendQueryAsync(cartQuery, ct).ConfigureAwait(false);

        CountCartItemsQuery itemsQuery = new(User.GetAccountId());
        var counts = await sender.SendQueryAsync(itemsQuery, ct).ConfigureAwait(false);

        GetCartsCountResponse response = new(
            TotalCount: totalCartCount,
            Counts: counts.ToDictionary(kv => kv.Key.Value, kv => kv.Value)
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
