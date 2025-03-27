using CustomCADs.Delivery.Domain.Shipments.Enums;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetSortings;

public class GetShipmentSortingsHandler
    : IQueryHandler<GetShipmentSortingsQuery, string[]>
{
    public Task<string[]> Handle(GetShipmentSortingsQuery req, CancellationToken ct)
        => Task.FromResult(
            Enum.GetNames<ShipmentSortingType>()
        );
}
