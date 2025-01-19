using CustomCADs.Orders.Application.CompletedOrders.Queries.Count;

namespace CustomCADs.Orders.Endpoints.CompletedOrders.Client.Get.Stats;

public sealed class GetCompletedOrdersStatsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<int>
{
    public override void Configure()
    {
        Get("stats");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("03. Stats")
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
