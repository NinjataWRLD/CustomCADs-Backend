using CustomCADs.Shared.Application.Delivery;

namespace CustomCADs.Delivery.Application.Shipments.Queries.GetWaybill;

public class GetShipmentWaybillHandler(IDeliveryService delivery)
    : IQueryHandler<GetShipmentWaybillQuery, byte[]>
{
    public async Task<byte[]> Handle(GetShipmentWaybillQuery req, CancellationToken ct)
    {
        return await delivery.PrintAsync(req.ShipmentId, ct: ct);
    }
}
