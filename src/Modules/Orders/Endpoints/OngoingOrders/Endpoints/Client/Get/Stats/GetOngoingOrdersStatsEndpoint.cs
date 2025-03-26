using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.Count;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Get.Stats;

public sealed class GetOngoingOrdersStatsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<GetOngoingOrdersStatsResponse>
{
    public override void Configure()
    {
        Get("stats");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("Stats")
            .WithDescription("See your Ongoing Orders' stats")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        CountOngoingOrdersQuery query = new(
            BuyerId: User.GetAccountId()
        );
        CountOngoingOrdersDto counts = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetOngoingOrdersStatsResponse response = new(
            PendingCount: counts.Pending,
            AcceptedCount: counts.Accepted,
            BegunCount: counts.Begun,
            FinishedCount: counts.Finished,
            ReportedCount: counts.Reported,
            RemovedCount: counts.Removed
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
