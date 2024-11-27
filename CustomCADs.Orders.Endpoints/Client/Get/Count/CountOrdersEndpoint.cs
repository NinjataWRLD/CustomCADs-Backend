using CustomCADs.Orders.Application.Orders.Queries.Count;

namespace CustomCADs.Orders.Endpoints.Client.Get.Count;

public class CountOrdersEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<CountOrdersResponse>
{
    public override void Configure()
    {
        Get("count");
        Group<ClientGroup>();
        Description(d => d.WithSummary("4. I want to see the count of my Orders by status"));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        CountOrdersQuery query = new(User.GetAccountId());
        CountOrdersDto counts = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        CountOrdersResponse response = new(
            Pending: counts.Pending,
            Accepted: counts.Accepted,
            Begun: counts.Begun,
            Finished: counts.Finished,
            Reported: counts.Reported,
            Removed: counts.Removed
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
