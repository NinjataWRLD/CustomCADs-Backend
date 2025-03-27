using CustomCADs.Shared.Abstractions.Delivery;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Shipments.Queries;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Shared;

public class CalculateShipmentHandler(IDeliveryService delivery)
    : IQueryHandler<CalculateShipmentQuery, CalculateShipmentDto[]>
{
    public async Task<CalculateShipmentDto[]> Handle(CalculateShipmentQuery req, CancellationToken ct)
    {
        CalculationDto[] calculations = await delivery.CalculateAsync(new(
            ParcelCount: req.ParcelCount,
            TotalWeight: req.TotalWeight,
            Country: req.Address.Country,
            City: req.Address.City
        ), ct).ConfigureAwait(false);

        return [.. calculations.Select(x => x.ToDto(req.TimeZone))];
    }
}
