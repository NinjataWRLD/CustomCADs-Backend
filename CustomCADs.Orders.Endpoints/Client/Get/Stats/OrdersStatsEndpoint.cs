using CustomCADs.Orders.Application.Orders.Queries.Count;

namespace CustomCADs.Orders.Endpoints.Client.Get.Stats;

public class OrdersStatsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<OrdersStatsResponse>
{
    public override void Configure()
    {
        Get("stats");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("02. Stats")
            .WithDescription("See your Orders' stats")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        CountOrdersQuery query = new(
            BuyerId: User.GetAccountId()
        );
        CountOrdersDto counts = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        OrdersStatsResponse response = new(
            PendingCount: counts.Pending,
            AcceptedCount: counts.Accepted,
            BegunCount: counts.Begun,
            FinishedCount: counts.Finished,
            CompletedCount: counts.Completed,
            ReportedCount: counts.Reported,
            RemovedCount: counts.Removed
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
