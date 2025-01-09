using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Orders.Domain.OngoingOrders.Reads;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.Count;

public sealed class CountOngoingOrdersHandler(IOngoingOrderReads reads)
    : IQueryHandler<CountOngoingOrdersQuery, CountOngoingOrdersDto>
{
    public async Task<CountOngoingOrdersDto> Handle(CountOngoingOrdersQuery req, CancellationToken ct)
    {
        Dictionary<OngoingOrderStatus, int> counts = await reads
            .CountByStatusAsync(req.BuyerId, ct: ct)
            .ConfigureAwait(false);

        return new(
            Pending: counts.TryGetValue(OngoingOrderStatus.Pending, out int pendingCount)
                ? pendingCount : 0,

            Accepted: counts.TryGetValue(OngoingOrderStatus.Accepted, out int acceptedCount)
                ? acceptedCount : 0,

            Begun: counts.TryGetValue(OngoingOrderStatus.Begun, out int begunCount)
                ? begunCount : 0,

            Finished: counts.TryGetValue(OngoingOrderStatus.Finished, out int finishedCount)
                ? finishedCount : 0,

            Reported: counts.TryGetValue(OngoingOrderStatus.Reported, out int reportedCount)
                ? reportedCount : 0,

            Removed: counts.TryGetValue(OngoingOrderStatus.Removed, out int removedCount)
                ? removedCount : 0
        );
    }
}
