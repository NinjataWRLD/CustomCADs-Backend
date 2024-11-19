using CustomCADs.Orders.Application.Orders.Queries.Count;
using CustomCADs.Orders.Domain.Orders.Enums;

namespace CustomCADs.Orders.Endpoints.Orders.Count;

public class CountOrdersEndpoint(IRequestSender sender)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("count");
        Group<OrdersGroup>();
        Description(d => d.WithSummary("3. I want to see the count of my Orders by status"));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        CountOrdersQuery query = new(User.GetAccountId(), default);
        
        query = query with { Status = OrderStatus.Pending };
        int pendingOrdersCount = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
        
        query = query with { Status = OrderStatus.Accepted };
        int acceptedOrdersCount = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
        
        query = query with { Status = OrderStatus.Begun };
        int begunOrdersCount = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
        
        query = query with { Status = OrderStatus.Finished };
        int finishedOrdersCount = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
        
        query = query with { Status = OrderStatus.Reported };
        int reportedOrdersCount = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
        
        query = query with { Status = OrderStatus.Removed };
        int removedOrdersCount = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        CountOrdersResponse response = new(pendingOrdersCount, acceptedOrdersCount, begunOrdersCount, finishedOrdersCount, reportedOrdersCount, removedOrdersCount);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
