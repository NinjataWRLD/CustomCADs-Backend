using CustomCADs.Orders.Application.CompletedOrders.Queries.Internal.Count;
using CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Client;

namespace CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Client.Get.Stats;

public sealed class GetCompletedOrdersStatsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<int>
{
    public override void Configure()
    {
        Get("stats");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("Stats")
            .WithDescription("See your Completed Orders' stats")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        CountCompletedOrdersQuery query = new(
            BuyerId: User.GetAccountId()
        );
        int count = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        await SendOkAsync(count).ConfigureAwait(false);
    }
}
