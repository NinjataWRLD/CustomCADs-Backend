using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Domain.Orders.Reads;

namespace CustomCADs.Orders.Application.Orders.Queries.Count;

public class CountOrdersHandler(IOrderReads reads)
    : IQueryHandler<CountOrdersQuery, CountOrdersDto>
{
    public async Task<CountOrdersDto> Handle(CountOrdersQuery req, CancellationToken ct)
    {
        Dictionary<OrderStatus, int> counts = await reads
            .CountByStatusAsync(req.BuyerId, ct: ct)
            .ConfigureAwait(false);

        return new(
            Pending: counts.TryGetValue(OrderStatus.Pending, out int pendingCount)
                ? pendingCount : 0,

            Accepted: counts.TryGetValue(OrderStatus.Accepted, out int acceptedCount)
                ? acceptedCount : 0,

            Begun: counts.TryGetValue(OrderStatus.Begun, out int begunCount)
                ? begunCount : 0,

            Finished: counts.TryGetValue(OrderStatus.Finished, out int finishedCount)
                ? finishedCount : 0,

            Completed: counts.TryGetValue(OrderStatus.Completed, out int completedCount)
                ? completedCount : 0,

            Reported: counts.TryGetValue(OrderStatus.Reported, out int reportedCount)
                ? reportedCount : 0,

            Removed: counts.TryGetValue(OrderStatus.Removed, out int removedCount)
                ? removedCount : 0
        );
    }
}
