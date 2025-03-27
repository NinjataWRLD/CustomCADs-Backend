using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.GetSortings;

namespace CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Client.Get.Sortings;

public sealed class GetCompletedOrderSortingsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<string[]>
{
    public override void Configure()
    {
        Get("sortings");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("Sortings")
            .WithDescription("See all Completed Order Sorting types")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        GetOngoingOrderSortingsQuery query = new();
        string[] result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        await SendOkAsync(result).ConfigureAwait(false);
    }
}
