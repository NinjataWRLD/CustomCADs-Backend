using CustomCADs.Orders.Domain.OngoingOrders.Enums;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.GetSortings;

public class GetOngoingOrderSortingsHandler
    : IQueryHandler<GetOngoingOrderSortingsQuery, string[]>
{
    public Task<string[]> Handle(GetOngoingOrderSortingsQuery req, CancellationToken ct)
        => Task.FromResult(
            Enum.GetNames<OngoingOrderSortingType>()
        );
}
