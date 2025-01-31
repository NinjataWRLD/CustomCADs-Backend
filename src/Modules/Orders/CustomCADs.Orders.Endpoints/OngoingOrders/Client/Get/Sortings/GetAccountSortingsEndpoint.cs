using CustomCADs.Orders.Application.OngoingOrders.Queries.GetSortings;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Client.Get.Sortings;

public sealed class GetOngoingOrderSortingsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<string[]>
{
    public override void Configure()
    {
        Get("sortings");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("05. Sortings")
            .WithDescription("See all Ongoing Order Sorting types")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        GetOngoingOrderSortingsQuery query = new();
        string[] result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        await SendOkAsync(result).ConfigureAwait(false);
    }
}
