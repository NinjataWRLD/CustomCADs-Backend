using CustomCADs.Orders.Domain.CompletedOrders.Enums;

namespace CustomCADs.Orders.Application.CompletedOrders.Queries.Internal.GetSortings;

public class GetCompletedOrderSortingsHandler
    : IQueryHandler<GetCompletedOrderSortingsQuery, string[]>
{
    public Task<string[]> Handle(GetCompletedOrderSortingsQuery req, CancellationToken ct)
        => Task.FromResult(
            Enum.GetNames<CompletedOrderSortingType>()
        );
}
