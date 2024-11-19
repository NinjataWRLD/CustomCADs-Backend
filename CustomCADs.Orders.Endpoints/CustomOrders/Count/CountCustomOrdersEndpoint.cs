using CustomCADs.Orders.Application.CustomOrders.Queries.Count;
using CustomCADs.Orders.Domain.CustomOrders.Enums;

namespace CustomCADs.Orders.Endpoints.CustomOrders.Count;

public class CountCustomOrdersEndpoint(IRequestSender sender)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("count");
        Group<CustomOrdersGroup>();
        Description(d => d.WithSummary("3. I want to see the count of my Orders by status"));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        CountCustomOrdersQuery query = new(User.GetAccountId(), default);
        
        query = query with { Status = CustomOrderStatus.Pending };
        int pendingOrdersCount = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
        
        query = query with { Status = CustomOrderStatus.Accepted };
        int acceptedOrdersCount = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
        
        query = query with { Status = CustomOrderStatus.Begun };
        int begunOrdersCount = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
        
        query = query with { Status = CustomOrderStatus.Finished };
        int finishedOrdersCount = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
        
        query = query with { Status = CustomOrderStatus.Reported };
        int reportedOrdersCount = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
        
        query = query with { Status = CustomOrderStatus.Removed };
        int removedOrdersCount = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        CountCustomOrdersResponse response = new(pendingOrdersCount, acceptedOrdersCount, begunOrdersCount, finishedOrdersCount, reportedOrdersCount, removedOrdersCount);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
