using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories.Reads;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.Count;

public sealed class CountCustomsHandler(ICustomReads reads)
    : IQueryHandler<CountCustomsQuery, CountCustomsDto>
{
    public async Task<CountCustomsDto> Handle(CountCustomsQuery req, CancellationToken ct)
    {
        Dictionary<CustomStatus, int> counts = await reads
            .CountAsync(req.BuyerId, ct: ct)
            .ConfigureAwait(false);

        return new(
            Pending: TryGetCount(counts, CustomStatus.Pending),
            Accepted: TryGetCount(counts, CustomStatus.Accepted),
            Begun: TryGetCount(counts, CustomStatus.Begun),
            Finished: TryGetCount(counts, CustomStatus.Finished),
            Completed: TryGetCount(counts, CustomStatus.Completed),
            Reported: TryGetCount(counts, CustomStatus.Reported)
        );
    }

    private static int TryGetCount(Dictionary<CustomStatus, int> counts, CustomStatus status)
    {
        if (counts.TryGetValue(status, out int count))
            return count;

        return 0;
    }
}
