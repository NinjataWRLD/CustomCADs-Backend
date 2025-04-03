using CustomCADs.Customs.Application.Customs.Queries.Internal.Client.Count;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Get.Stats;

public sealed class GetCustomsStatsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<GetCustomsStatsResponse>
{
    public override void Configure()
    {
        Get("stats");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("Stats")
            .WithDescription("See your Custom' stats")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        CountCustomsQuery query = new(
            BuyerId: User.GetAccountId()
        );
        var counts = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetCustomsStatsResponse response = new(
            PendingCount: counts.Pending,
            AcceptedCount: counts.Accepted,
            BegunCount: counts.Begun,
            FinishedCount: counts.Finished,
            CompletedCount: counts.Completed,
            ReportedCount: counts.Reported
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
