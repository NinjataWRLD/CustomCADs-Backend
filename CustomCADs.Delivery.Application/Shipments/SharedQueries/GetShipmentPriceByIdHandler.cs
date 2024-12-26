using CustomCADs.Delivery.Application.Common.Exceptions;
using CustomCADs.Delivery.Domain.Shipments.Reads;
using CustomCADs.Shared.UseCases.Shipments.Queries;

namespace CustomCADs.Delivery.Application.Shipments.SharedQueries;

public class GetShipmentPriceByIdHandler(IShipmentReads reads)
    : IQueryHandler<GetShipmentPriceByIdQuery, decimal>
{
    public async Task<decimal> Handle(GetShipmentPriceByIdQuery req, CancellationToken ct)
    {
        Shipment shipment = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ShipmentNotFoundException.ById(req.Id);

        return shipment.Price;
    }
}
