using CustomCADs.Shared.Application.Delivery;
using CustomCADs.Shared.Application.Delivery.Dtos;
using CustomCADs.Shared.Speedy.Services.Calculation;
using CustomCADs.Shared.Speedy.Services.Shipment;
using Microsoft.Extensions.Options;

namespace CustomCADs.Shared.Infrastructure.Delivery;

public sealed class SpeedyService(
    IOptions<DeliverySettings> settings,
    ShipmentService shipmentService,
    CalculationService calculationService
) : IDeliveryService
{
    public async Task<CalculationDto[]> CalculateAsync(string shipmentId, CancellationToken ct = default)
    {
        var response = await calculationService.CalculateAsync(
            account: new(settings.Value.Username, settings.Value.Password),
            shipmentId: shipmentId,
            ct: ct
        ).ConfigureAwait(false);

        return [.. response.Select(c => new CalculationDto(
            ServiceId: c.ServiceId,
            Price: new(
                Amount: c.Price.Amount,
                Vat: c.Price.Vat,
                Total: c.Price.Total,
                Currency: c.Price.Currency
            ),
            PickupDate: c.PickupDate,
            DeliveryDeadline: c.DeliveryDeadline
        ))];
    }

    public async Task<ShipmentDto> ShipAsync(string package, string contents, int parcelCount, double totalWeight, CancellationToken ct = default)
    {
        var response = await shipmentService.CreateShipmentAsync(
            account: new(settings.Value.Username, settings.Value.Password),
            package: package,
            contents: contents,
            parcelCount: parcelCount,
            totalWeight: totalWeight,
            ct: ct
        ).ConfigureAwait(false);

        return new(
            Id: response.Id,
            ParcelIds: [.. response.Parcels.Select(p => p.Id)],
            Price: Convert.ToDecimal(response.Price.Amount),
            PickupDate: response.PickupDate,
            DeliveryDeadline: response.DeliveryDeadline
        );
    }
}
