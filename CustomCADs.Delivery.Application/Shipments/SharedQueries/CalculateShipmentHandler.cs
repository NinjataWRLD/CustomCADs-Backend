using CustomCADs.Shared.Abstractions.Delivery;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;
using CustomCADs.Shared.UseCases.Shipments.Queries;

namespace CustomCADs.Delivery.Application.Shipments.SharedQueries;

public class CalculateShipmentHandler(IDeliveryService delivery)
    : IQueryHandler<CalculateShipmentQuery, CalculationDto[]>
{
    public async Task<CalculationDto[]> Handle(CalculateShipmentQuery req, CancellationToken ct)
    {
        CalculationDto[] calculations = await delivery.CalculateAsync(new(
            ParcelCount: req.ParcelCount,
            TotalWeight: req.TotalWeight,
            Country: req.Address.Country,
            City: req.Address.City
        ), ct).ConfigureAwait(false);

        return calculations;
    }
}
