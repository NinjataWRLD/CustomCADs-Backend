using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.Count;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.Stats;

public sealed class GetCustomsStatsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<GetCustomsStatsResponse>
{
    public override void Configure()
    {
        Get("stats");
        Group<CustomerGroup>();
        Description(d => d
            .WithSummary("Stats")
            .WithDescription("See your Custom' stats")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var counts = await sender.SendQueryAsync(
            new CountCustomsQuery(
                BuyerId: User.GetAccountId()
            ),
            ct
        ).ConfigureAwait(false);

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
